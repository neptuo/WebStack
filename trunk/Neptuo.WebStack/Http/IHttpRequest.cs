using Neptuo.Collections.Specialized;
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
    /// Http request.
    /// </summary>
    public interface IHttpRequest
    {
        /// <summary>
        /// Collection of supported values.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        IReadOnlyKeyValueCollection Values { get; }
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
            return request.Values.Get<HttpMethod>("Method");
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
        /// Requested url.
        /// </summary>
        public static Uri Url(this IHttpRequest request)
        {
            Guard.NotNull(request, "request");
            return request.Values.Get<Uri>("Url");
        }

        /// <summary>
        /// Http request headers.
        /// </summary>
        public static IReadOnlyKeyValueCollection Headers(this IHttpRequest request)
        {
            Guard.NotNull(request, "request");
            return request.Values.Get<IReadOnlyKeyValueCollection>("Headers");
        }

        /// <summary>
        /// Input stream.
        /// </summary>
        public static Stream InputStream(this IHttpRequest request)
        {
            Guard.NotNull(request, "request");
            return request.Values.Get<Stream>("InputStream");
        }

        /// <summary>
        /// Input query string.
        /// </summary>
        public static IReadOnlyKeyValueCollection QueryString(this IHttpRequest request)
        {
            Guard.NotNull(request, "request");
            return request.Values.Get<IReadOnlyKeyValueCollection>("QueryString");
        }

        /// <summary>
        /// Data posted as form data.
        /// </summary>
        public static IReadOnlyKeyValueCollection Form(this IHttpRequest request)
        {
            Guard.NotNull(request, "request");
            return request.Values.Get<IReadOnlyKeyValueCollection>("Form");
        }

        /// <summary>
        /// Posted files.
        /// </summary>
        public static IEnumerable<IHttpFile> Files(this IHttpRequest request)
        {
            Guard.NotNull(request, "request");
            return request.Values.Get<IEnumerable<IHttpFile>>("Files");
        }
    }
}
