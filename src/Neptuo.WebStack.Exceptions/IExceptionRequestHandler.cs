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
    public interface IExceptionRequestHandler
    {
        /// <summary>
        /// Processes <paramref name="exception"/> raised during processing request described in <paramref name="httpRequest"/>.
        /// </summary>
        /// <param name="exception">Raised exception.</param>
        /// <param name="httpRequest">Current HTTP request.</param>
        /// <returns>Response for the current HTTP request.</returns>
        Task<IHttpResponse> HandleAsync(Exception exception, IHttpRequest httpRequest);
    }
}
