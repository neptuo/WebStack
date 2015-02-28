
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Describes HTTP raw response values.
    /// </summary>
    public interface IHttpResponseMessage
    {
        /// <summary>
        /// HTTP protocol used in request.
        /// Eg. HTTP/1.1
        /// </summary>
        string Protocol { get; set; }

        /// <summary>
        /// Response status code.
        /// </summary>
        int StatusCode { get; set; }

        /// <summary>
        /// Response reason phrase (description of <see cref="IHttpResponseMessage.StatusCode"/>).
        /// </summary>
        string StatusText { get; set; }

        /// <summary>
        /// Key-value collection to response headers.
        /// </summary>
        IDictionary<string, string> Headers { get; }

        /// <summary>
        /// Raw response body.
        /// </summary>
        Stream BodyStream { get; }
    }
}
