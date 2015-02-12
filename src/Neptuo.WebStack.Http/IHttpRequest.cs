using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Http request.
    /// </summary>
    public interface IHttpRequest
    {
        /// <summary>
        /// Raw HTTP request values.
        /// </summary>
        IRawHttpRequest RawValues { get; }

        /// <summary>
        /// Collection of supported values.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        IKeyValueCollection CustomValues { get; }
    }
}
