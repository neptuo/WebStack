using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Wraps <see cref="HttpContext"/>.
    /// </summary>
    public class AspNetHttpContext : IHttpContext
    {
        /// <summary>
        /// Original http context.
        /// </summary>
        private readonly HttpContext httpContext;


        /// <summary>
        /// Cached wrapper for <see cref="HttpRequest"/>.
        /// </summary>
        private IHttpRequest request;

        /// <summary>
        /// Cached wrapper for <see cref="HttpResponse"/>.
        /// </summary>
        private IHttpResponse response;

        /// <summary>
        /// Inner collection for <see cref="IHttpContext.Values"/>.
        /// </summary>
        private IDictionary<string, object> values;

        public IHttpRequest Request
        {
            get
            {
                if (request == null)
                    request = new AspNetHttpRequest(httpContext.Request);

                return request;
            }
        }

        public IHttpResponse Response
        {
            get
            {
                if (response == null)
                    response = new AspNetHttpResponse(httpContext.Response, Request);

                return response;
            }
        }

        public IDictionary<string, object> Values
        {
            get
            {
                if (values == null)
                    values = new Dictionary<string, object>();

                return values;
            }
        }

        public AspNetHttpContext(HttpContext httpContext)
        {
            Guard.NotNull(httpContext, "httpContext");
            this.httpContext = httpContext;
        }

        public string ResolveUrl(string appRelativeUrl)
        {
            Guard.NotNullOrEmpty(appRelativeUrl, "appRelativeUrl");
            return VirtualPathUtility.ToAbsolute(appRelativeUrl);
        }
    }
}
