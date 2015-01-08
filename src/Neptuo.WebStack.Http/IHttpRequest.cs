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
        /// Current service container.
        /// </summary>
        IDependencyProvider DependencyProvider();

        /// <summary>
        /// Http method.
        /// </summary>
        HttpMethod Method();

        /// <summary>
        /// Requested url.
        /// </summary>
        IReadOnlyUrl Url();

        /// <summary>
        /// Http request headers.
        /// </summary>
        IReadOnlyKeyValueCollection Headers();

        /// <summary>
        /// Input query string.
        /// </summary>
        IHttpParamCollection QueryString();

        /// <summary>
        /// Input stream.
        /// </summary>
        Stream InputStream();

        /// <summary>
        /// Data posted as form data.
        /// </summary>
        IHttpParamCollection Form();

        /// <summary>
        /// Posted files.
        /// </summary>
        IEnumerable<IHttpFile> Files();

        /// <summary>
        /// Cancellation token indicating whether the HTTP request was cancelled.
        /// </summary>
        CancellationToken CancellationToken();

        /// <summary>
        /// Event fired when disposing this HTTP request object.
        /// </summary>
        event Action OnDisposing;

        /// <summary>
        /// Collection of supported values.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        IKeyValueCollection CustomValues();
    }

}
