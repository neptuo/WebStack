using Neptuo.WebStack.Http.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Common HTTP method extensions for <see cref="HttpRequest"/>.
    /// </summary>
    public static class _HttpRequestExtensions_Method
    {
        /// <summary>
        /// Returns HTTP method of <paramref name="request"/>.
        /// </summary>
        public static HttpMethod Method(this HttpRequest request)
        {
            Ensure.NotNull(request, "request");

            HttpMethod method;
            if (!request.CustomValues().TryGet(RequestKey.Method, out method))
            {
                method = Converts.To<string, HttpMethod>(request.RawMessage().Method);
                request.CustomValues().Set(RequestKey.Method, method);
            }

            return method;
        }

        /// <summary>
        /// Returns <c>true</c> if Http method equals to Get; returns <c>false</c> otherwise.
        /// </summary>
        public static bool IsMethodGet(this HttpRequest request)
        {
            Ensure.NotNull(request, "request");
            return request.Method() == HttpMethod.Get;
        }

        /// <summary>
        /// Returns <c>true</c> if Http method equals to Post; returns <c>false</c> otherwise.
        /// </summary>
        public static bool IsMethodPost(this HttpRequest request)
        {
            Ensure.NotNull(request, "request");
            return request.Method() == HttpMethod.Post;
        }

        /// <summary>
        /// Returns <c>true</c> if Http method equals to Put; returns <c>false</c> otherwise.
        /// </summary>
        public static bool IsMethodPut(this HttpRequest request)
        {
            Ensure.NotNull(request, "request");
            return request.Method() == HttpMethod.Put;
        }

        /// <summary>
        /// Returns <c>true</c> if Http method equals to Delete; returns <c>false</c> otherwise.
        /// </summary>
        public static bool IsMethodDelete(this HttpRequest request)
        {
            Ensure.NotNull(request, "request");
            return request.Method() == HttpMethod.Delete;
        }

    }
}
