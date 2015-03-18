using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Exceptions
{
    /// <summary>
    /// Handler for handling exceptions raised during request processing.
    /// </summary>
    public interface IExceptionHandler
    {
        /// <summary>
        /// Processes <paramref name="exception"/> raised during processing request described in <paramref name="httpContext"/>.
        /// </summary>
        /// <param name="exception">Raised exception.</param>
        /// <param name="httpContext">Current HTTP context.</param>
        /// <returns><c>true</c> if request was handled; <c>false</c> to process request by next handler.</returns>
        Task<bool> TryHandleAsync(Exception exception, IHttpContext httpContext);
    }
}
