using Neptuo.Collections.Specialized;
using Neptuo.WebStack.Http.Keys;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Http request.
    /// </summary>
    public interface IHttpRequest
    {
        /// <summary>
        /// Collection of supported values.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        IKeyValueCollection Values { get; }

        /// <summary>
        /// Event fired when disposing HTTP request.
        /// </summary>
        event Action OnDisposing;
    }

    /// <summary>
    /// Common extensions for <see cref="IHttpRequest"/>.
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Http method.
        /// </summary>
        public static HttpMethod Method(this IHttpRequest request)
        {
            Guard.NotNull(request, "request");
            return request.Values.Get<HttpMethod>(RequestKey.Method);
        }

        /// <summary>
        /// Returns <c>true</c> if Http method equals to Get; returns <c>false</c> otherwise.
        /// </summary>
        public static bool IsMethodGet(this IHttpRequest request)
        {
            Guard.NotNull(request, "request");
            return request.Method() == HttpMethod.Get;
        }

        /// <summary>
        /// Returns <c>true</c> if Http method equals to Post; returns <c>false</c> otherwise.
        /// </summary>
        public static bool IsMethodPost(this IHttpRequest request)
        {
            Guard.NotNull(request, "request");
            return request.Method() == HttpMethod.Post;
        }

        /// <summary>
        /// Returns <c>true</c> if Http method equals to Put; returns <c>false</c> otherwise.
        /// </summary>
        public static bool IsMethodPut(this IHttpRequest request)
        {
            Guard.NotNull(request, "request");
            return request.Method() == HttpMethod.Put;
        }

        /// <summary>
        /// Returns <c>true</c> if Http method equals to Delete; returns <c>false</c> otherwise.
        /// </summary>
        public static bool IsMethodDelete(this IHttpRequest request)
        {
            Guard.NotNull(request, "request");
            return request.Method() == HttpMethod.Delete;
        }

        /// <summary>
        /// The base path on which is application hosted.
        /// </summary>
        public static string ApplicationPath(this IHttpRequest request)
        {
            Guard.NotNull(request, "request");
            return request.Values.Get<string>(RequestKey.ApplicationPath);
        }

        /// <summary>
        /// Requested url.
        /// </summary>
        public static IReadOnlyUrl Url(this IHttpRequest request)
        {
            Guard.NotNull(request, "request");
            return request.Values.Get<IReadOnlyUrl>(RequestKey.Url);
        }

        /// <summary>
        /// Http request headers.
        /// </summary>
        public static IReadOnlyKeyValueCollection Headers(this IHttpRequest request)
        {
            Guard.NotNull(request, "request");
            return request.Values.Get<IReadOnlyKeyValueCollection>(RequestKey.Headers);
        }

        /// <summary>
        /// Http request headers.
        /// </summary>
        public static T Header<T>(this IHttpRequest request, string headerName, T? defaltValue)
            where T : struct
        {
            Guard.NotNull(request, "request");
            Guard.NotNullOrEmpty(headerName, "headerName");
            return request.Headers().Get<T>(headerName, defaltValue);
        }

        /// <summary>
        /// Http request headers.
        /// </summary>
        public static T Header<T>(this IHttpRequest request, string headerName, T defaltValue)
            where T : class
        {
            Guard.NotNull(request, "request");
            Guard.NotNullOrEmpty(headerName, "headerName");
            return request.Headers().Get<T>(headerName, defaltValue);
        }

        /// <summary>
        /// Input stream.
        /// </summary>
        public static Stream InputStream(this IHttpRequest request)
        {
            Guard.NotNull(request, "request");
            return request.Values.Get<Stream>(RequestKey.InputStream);
        }

        /// <summary>
        /// Input query string.
        /// </summary>
        public static IHttpParamCollection QueryString(this IHttpRequest request)
        {
            Guard.NotNull(request, "request");
            return request.Values.Get<IHttpParamCollection>(RequestKey.QueryString);
        }

        /// <summary>
        /// Data posted as form data.
        /// </summary>
        public static IHttpParamCollection Form(this IHttpRequest request)
        {
            Guard.NotNull(request, "request");
            return request.Values.Get<IHttpParamCollection>(RequestKey.Form);
        }

        /// <summary>
        /// Posted files.
        /// </summary>
        public static IEnumerable<IHttpFile> Files(this IHttpRequest request)
        {
            Guard.NotNull(request, "request");
            return request.Values.Get<IEnumerable<IHttpFile>>(RequestKey.Files);
        }

        /// <summary>
        /// Cancellation token for calling HTTP request.
        /// </summary>
        public static CancellationToken CancellationToken(this IHttpRequest request)
        {
            Guard.NotNull(request, "request");
            return request.Values.Get<CancellationToken>(RequestKey.CancellationToken);
        }
    }
}
