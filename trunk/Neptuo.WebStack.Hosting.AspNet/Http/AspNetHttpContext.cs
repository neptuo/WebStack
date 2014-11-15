using Neptuo.Collections.Specialized;
using Neptuo.ComponentModel;
using Neptuo.WebStack.Http.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Neptuo.WebStack.Http
{
    public class AspNetHttpContext : DisposableBase, IHttpContext, IHttpRequest, IHttpResponse
    {
        private readonly HttpContext httpContext;
        private readonly ProviderKeyValueCollection values;

        public IKeyValueCollection Values
        {
            get { return values; }
        }

        public event Action OnDisposing;

        public AspNetHttpContext(HttpContext httpContext)
        {
            Guard.NotNull(httpContext, "httpContext");
            this.values = new ProviderKeyValueCollection();
            this.values.AddProvider(TryGetContextValue);
            this.values.AddProvider(TryGetRequestValue);
            this.values.AddProvider(TryGetResponseValue);
            this.httpContext = httpContext;
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

            if (key == ContextKey.UrlBuilder)
            {
                value = new UrlBuilder();
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
            {
                value = this.UrlBuilder().FromUrl(httpContext.Request.Url.AbsoluteUri);
                return true;
            }

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

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            RaiseOnDisposing();
        }

        /// <summary>
        /// Raises <see cref="OnDisposing"/> and sets it to <c>null</c>.
        /// </summary>
        private void RaiseOnDisposing()
        {
            if (OnDisposing != null)
                OnDisposing();

            OnDisposing = null;
        }
    }
}
