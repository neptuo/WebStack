using Neptuo.Collections.Specialized;
using Neptuo.WebStack.Http.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Neptuo.WebStack.Http
{
    public class AspNetHttpContext : IHttpContext, IHttpRequest, IHttpResponse
    {
        private readonly HttpContext httpContext;
        private readonly ProviderKeyValueCollection values;

        public IKeyValueCollection Values
        {
            get { return values; }
        }

        public AspNetHttpContext(HttpContext httpContext)
        {
            Guard.NotNull(httpContext, "httpContext");
            this.values = new ProviderKeyValueCollection();
            this.values.AddProvider(TryGetValue);
            this.httpContext = httpContext;
        }

        private bool TryGetValue(string key, out object value)
        {
            if (TryGetContextValue(key, out value))
                return true;

            if (TryGetRequestValue(key, out value))
                return true;

            if (TryGetResponseValue(key, out value))
                return true;

            value = null;
            return false;
        }

        private bool TryGetContextValue(string key, out object value)
        {
            if(key == ContextKey.Request)
            {
                value = this;
                return true;
            }

            if (key == ContextKey.Response)
            {
                value = this;
                return true;
            }

            value = null;
            return false;
        }

        private bool TryGetRequestValue(string key, out object value)
        {
            if (key == RequestKey.CancellationToken)
            {
                value = httpContext.Response.ClientDisconnectedToken;
                return true;
            }

            if (key == RequestKey.Files)
            {
                value = GetPostedFiles(httpContext.Request.Files);
                return true;
            }

            if (key == RequestKey.Form)
            {
                value = new KeyValueCollection(httpContext.Request.QueryString);
                return true;
            }

            if (key == RequestKey.Headers)
            {
                value = new KeyValueCollection(httpContext.Request.Headers);
                return true;
            }

            if (key == RequestKey.InputStream)
            {
                value = httpContext.Request.InputStream;
                return true;
            }

            if (key == RequestKey.Method)
            {
                value = HttpMethod.KnownMethods[httpContext.Request.HttpMethod];
                return true;
            }

            if (key == RequestKey.QueryString)
            {
                value = new KeyValueCollection(httpContext.Request.QueryString);
                return true;
            }

            if (key == RequestKey.Url)
                throw Guard.Exception.NotImplemented();

            value = null;
            return false;
        }

        private bool TryGetResponseValue(string key, out object value)
        {
            if (key == ResponseKey.Headers)
            {
                value = new KeyValueCollection(httpContext.Response.Headers);
                return true;
            }

            if (key == ResponseKey.OutputStream)
            {
                value = httpContext.Response.OutputStream;
                return true;
            }

            value = null;
            return false;
        }

        private IEnumerable<IHttpFile> GetPostedFiles(HttpFileCollection files)
        {
            List<IHttpFile> result = new List<IHttpFile>();
            foreach (HttpPostedFile file in files)
                result.Add(new AspNetHttpFile(file));

            return result;
        }
    }
}
