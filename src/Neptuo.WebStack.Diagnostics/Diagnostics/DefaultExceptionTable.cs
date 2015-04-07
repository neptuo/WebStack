using Neptuo.ComponentModel;
using Neptuo.WebStack.Diagnostics.Internals;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Diagnostics
{
    /// <summary>
    /// Default implementation of <see cref="IExceptionTable"/>.
    /// </summary>
    public class DefaultExceptionTable : IExceptionTable
    {
        private readonly OutFuncCollection<Type, IExceptionHandler, bool> onSearchExceptionHandler = new OutFuncCollection<Type, IExceptionHandler, bool>();
        private readonly OutFuncCollection<Type, IRequestHandler, bool> onSearchRequestHandler = new OutFuncCollection<Type, IRequestHandler, bool>();
        private readonly Dictionary<Type, IExceptionHandler> storage = new Dictionary<Type, IExceptionHandler>();
        private readonly object storageLock = new object();

        public IExceptionTable AddSearchHandler(OutFunc<Type, IExceptionHandler, bool> searchHandler)
        {
            Ensure.NotNull(searchHandler, "searchHandler");
            onSearchExceptionHandler.Add(searchHandler);
            return this;
        }

        public IExceptionTable AddSearchHandler(OutFunc<Type, IRequestHandler, bool> searchHandler)
        {
            Ensure.NotNull(searchHandler, "searchHandler");
            onSearchRequestHandler.Add(searchHandler);
            return this;
        }

        public IExceptionTable MapException(Type exceptionType, IExceptionHandler exceptionHandler)
        {
            Ensure.NotNull(exceptionType, "exceptionType");
            Ensure.NotNull(exceptionHandler, "exceptionHandler");
            storage[exceptionType] = exceptionHandler;
            return this;
        }

        public IExceptionTable MapException(Type exceptionType, IRequestHandler requestHandler)
        {
            return MapException(exceptionType, new RequestExceptionHandler(requestHandler));
        }

        public bool TryGet(Type exceptionType, out IExceptionHandler exceptionHandler)
        {
            // Try to look in storage.
            if (TryGetFromStorage(exceptionType, out exceptionHandler))
                return true;

            // Try to execute exception handler searching...
            if (onSearchExceptionHandler.TryExecute(exceptionType, out exceptionHandler))
            {
                lock (storageLock)
                {
                    IExceptionHandler currenHandler = exceptionHandler;
                    if (TryGetFromStorage(exceptionType, out exceptionHandler))
                        return true;

                    storage[exceptionType] = exceptionHandler = currenHandler;
                    return true;
                }
            }

            // Try to execute request handler searching...
            IRequestHandler requestHandler;
            if (onSearchRequestHandler.TryExecute(exceptionType, out requestHandler))
            {
                lock (storageLock)
                {
                    if (TryGetFromStorage(exceptionType, out exceptionHandler))
                        return true;

                    storage[exceptionType] = exceptionHandler = new RequestExceptionHandler(requestHandler);
                    return true;
                }
            }

            // Exception handler is not available.
            exceptionHandler = null;
            return false;
        }

        private bool TryGetFromStorage(Type exceptionType, out IExceptionHandler exceptionHandler)
        {
            while (exceptionType != typeof(object))
            {
                if (storage.TryGetValue(exceptionType, out exceptionHandler))
                    return true;

                exceptionType = exceptionType.BaseType;
            }

            exceptionHandler = null;
            return false;
        }
    }
}
