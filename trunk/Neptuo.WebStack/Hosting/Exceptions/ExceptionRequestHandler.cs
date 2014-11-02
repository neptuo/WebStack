﻿using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Hosting.Exceptions
{
    /// <summary>
    /// Handler for processing exceptions raised when executing main handler.
    /// Execution of passed in request handler is wrapped in try-catch block and possible
    /// exception is processed by registered <see cref="IExceptionRequestHandler"/>.
    /// </summary>
    public class ExceptionRequestHandler : IRequestHandler
    {
        private readonly IRequestHandler innerHandler;
        private readonly Dictionary<Type, IExceptionRequestHandler> storage;

        public ExceptionRequestHandler(IRequestHandler innerHandler)
        {
            Guard.NotNull(innerHandler, "innerHandler");
            this.innerHandler = innerHandler;
            this.storage = new Dictionary<Type, IExceptionRequestHandler>();
        }

        public ExceptionRequestHandler MapException(Type exceptionType, IExceptionRequestHandler exceptionHandler)
        {
            Guard.NotNull(exceptionType, "exceptionType");
            Guard.NotNull(exceptionHandler, "exceptionHandler");
            storage[exceptionType] = exceptionHandler;
            return this;
        }

        public Task<bool> HandleAsync(IHttpContext httpContext)
        {
            return HandleOverrideAsync(innerHandler, httpContext);
        }

        private async Task<bool> HandleOverrideAsync(IRequestHandler requestHandler, IHttpContext httpContext)
        {
            try
            {
                return await innerHandler.HandleAsync(httpContext);
            }
            catch (Exception e)
            {
                return HandleExceptionAsync(e, httpContext).Result; //TODO: Fix await
            }
        }

        private async Task<bool> HandleExceptionAsync(Exception e, IHttpContext httpContext)
        {
            IExceptionRequestHandler exceptionHandler = FindExceptionHandler(e.GetType());
            if (exceptionHandler == null)
                return false;

            try
            {
                return await exceptionHandler.HandleAsync(e, httpContext);
            }
            catch (Exception next)
            {
                return HandleExceptionAsync(next, httpContext).Result; //TODO: Fix await
            }
        }

        private IExceptionRequestHandler FindExceptionHandler(Type exceptionType)
        {
            IExceptionRequestHandler requestHandler;
            if (storage.TryGetValue(exceptionType, out requestHandler))
                return requestHandler;

            if (exceptionType.BaseType != typeof(object))
                return FindExceptionHandler(exceptionType.BaseType);

            return null;
        }
    }
}
