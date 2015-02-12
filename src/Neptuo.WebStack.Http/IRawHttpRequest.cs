using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Base HTTP request description.
    /// </summary>
    public interface IRawHttpRequest
    {
        /// <summary>
        /// HTTP method used in request.
        /// </summary>
        string Method { get; }

        /// <summary>
        /// Raw URL (url + query string as client requested).
        /// May contain also schema and domain.
        /// </summary>
        string Url { get; }

        /// <summary>
        /// HTTP protocol used in request.
        /// Eg. HTTP/1.1
        /// </summary>
        string Protocol { get; }

        /// <summary>
        /// Key-value collection to request headers.
        /// </summary>
        IDictionary<string, string> Headers { get; }

        /// <summary>
        /// Raw request body.
        /// </summary>
        Stream InputStream { get; }
    }
}
