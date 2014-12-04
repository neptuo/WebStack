using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack
{
    /// <summary>
    /// Base class with some util methods.
    /// </summary>
    public abstract class RequestHandlerAsync : IRequestHandler
    {
        /// <summary>
        /// Process <paramref name="httpRequest"/>.
        /// </summary>
        /// <param name="httpRequest">Current Http context.</param>
        /// <returns>
        /// If returns <c>null</c>, request processing should be delegated to the next handler.
        /// If returns any non-null response, this handler handled the request described in <paramref name="httpRequest"/>;
        /// </returns>
        protected abstract Task<IHttpResponse> TryHandleAsync(IHttpRequest httpRequest);

        /// <summary>
        /// Returns empty HTTP response for manual setting values.
        /// </summary>
        /// <returns>Empty HTTP response.</returns>
        protected IHttpResponse BuildResponse()
        {
            return new DefaultHttpResponse();
        }

        /// <summary>
        /// Builds response from content in <paramref name="content"/> with Content-type set to <paramref name="contentType"/>.
        /// </summary>
        /// <param name="content">Raw response bytes.</param>
        /// <param name="contentType">Type of content in <paramref name="content"/>.</param>
        /// <returns>HTTP response with content from <paramref name="content"/>.</returns>
        protected IHttpResponse BuildStreamResponse(Stream content, HttpMediaType contentType)
        {
            return new DefaultHttpResponse(HttpStatus.Ok, content).HeaderContentType(contentType);
        }

        Task<IHttpResponse> IRequestHandler.TryHandleAsync(IHttpRequest httpRequest)
        {
            return TryHandleAsync(httpRequest);
        }
    }
}
