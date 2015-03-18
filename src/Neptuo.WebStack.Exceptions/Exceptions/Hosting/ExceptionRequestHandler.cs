using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Exceptions.Hosting
{
    /// <summary>
    /// Handler for processing exceptions raised when executing main handler.
    /// Execution of passed in request handler is wrapped in try-catch block and possible
    /// exception is processed by registered <see cref="IExceptionHandler"/>.
    /// </summary>
    public class ExceptionRequestHandler : IRequestHandler
    {
        private readonly IExceptionTable exceptionTable;
        private readonly IRequestHandler requestHandler;

        /// <summary>
        /// Creates new instance with default exception table <see cref="Engine.Environment.WithExceptionTable"/>.
        /// </summary>
        /// <param name="requestHandler">Inner request handler to wrap.</param>
        public ExceptionRequestHandler(IRequestHandler requestHandler)
            : this(Engine.Environment.WithExceptionTable(), requestHandler)
        { }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="exceptionTable">Exception mapping.</param>
        /// <param name="requestHandler">Inner request handler to wrap.</param>
        public ExceptionRequestHandler(IExceptionTable exceptionTable, IRequestHandler requestHandler)
        {
            Ensure.NotNull(exceptionTable, "exceptionTable");
            Ensure.NotNull(requestHandler, "requestHandler");
            this.requestHandler = requestHandler;
            this.exceptionTable = exceptionTable;
        }

        public async Task<bool> TryHandleAsync(IHttpContext httpContext)
        {
            Exception exception = null;

            try
            {
                // Try execute inner handler.
                return await requestHandler.TryHandleAsync(httpContext);
            }
            catch (Exception e)
            {
                // No awaiting in catch, so, store exception.
                exception = e;
            }

            // If catch block was visited, try handle raised exception.
            if (exception != null)
                return await HandleExceptionAsync(exception, httpContext);

            // So, this should be never reached...
            return false;
        }

        private async Task<bool> HandleExceptionAsync(Exception sourceException, IHttpContext httpContext)
        {
            ExceptionModel sourceModel = httpContext.Exceptions().Push(sourceException);

            IExceptionHandler exceptionHandler;
            if (!exceptionTable.TryGet(sourceException.GetType(), out exceptionHandler))
                throw sourceException;

            Exception exception = null;

            try
            {
                // Try execute inner handler.
                bool result = await exceptionHandler.TryHandleAsync(sourceException, httpContext);
                if (result)
                    httpContext.Exceptions().MarkAsHandled(sourceModel);

                return result;
            }
            catch (Exception e)
            {
                // No awaiting in catch, so, store exception.
                exception = e;
            }

            // If catch block was visited, try handle raised exception.
            if (exception != null)
                return await HandleExceptionAsync(exception, httpContext);

            // So, this should be never reached...
            return false;
        }
    }
}
