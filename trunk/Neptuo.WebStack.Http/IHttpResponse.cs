using Neptuo.Collections.Specialized;
using Neptuo.WebStack.Http.Keys;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Describes Http response.
    /// </summary>
    public interface IHttpResponse : IDisposable
    {
        /// <summary>
        /// HTTP response status.
        /// </summary>
        HttpStatus Status();

        /// <summary>
        /// HTTP response status.
        /// </summary>
        IHttpResponse Status(HttpStatus status);

        /// <summary>
        /// HTTP response headers.
        /// </summary>
        IKeyValueCollection Headers();

        /// <summary>
        /// HTTP response stream.
        /// </summary>
        Stream OutputStream();

        /// <summary>
        /// Event fired when disposing this HTTP response object.
        /// </summary>
        event Action OnDisposing;

        
        /// <summary>
        /// Collection of supported values.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        IKeyValueCollection CustomValues();
    }
}
