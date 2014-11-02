using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Hosting.Exceptions
{
    /// <summary>
    /// Handler for handling exceptions raised during request processing.
    /// </summary>
    public interface IExceptionRequestHandler
    {
        /// <summary>
        /// Processes <paramref name="exception"/> raised during processing request described in <paramref name="httpContext"/>.
        /// </summary>
        /// <param name="exception">Raised exception.</param>
        /// <param name="httpContext">Current HTTP context.</param>
        /// <returns>Whether handler was able to handler the exception.</returns>
        Task<bool> HandleAsync(Exception exception, IHttpContext httpContext);
    }
}
