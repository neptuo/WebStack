using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Diagnostics
{
    /// <summary>
    /// Generic base class for handling exceptions.
    /// </summary>
    /// <typeparam name="T">Type of exception to handle.</typeparam>
    public abstract class ExceptionHandlerBase<T> : IExceptionHandler
        where T : Exception
    {
        /// <summary>
        /// Processes <paramref name="exception"/> raised during processing request described in <paramref name="httpContext"/>.
        /// </summary>
        /// <param name="exception">Raised exception.</param>
        /// <param name="httpContext">Current HTTP context.</param>
        /// <returns><c>true</c> if request was handled; <c>false</c> to process request by next handler.</returns>
        protected abstract Task<bool> TryHandleAsync(T exception, IHttpContext httpContext);

        public Task<bool> TryHandleAsync(Exception exception, IHttpContext httpContext)
        {
            return TryHandleAsync((T)exception, httpContext);
        }
    }
}
