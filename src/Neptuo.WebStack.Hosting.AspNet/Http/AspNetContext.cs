using Neptuo.Activators;
using Neptuo.Collections.Specialized;
using Neptuo.ComponentModel;
using Neptuo.FeatureModels;
using Neptuo.WebStack.Http.Keys;
using Neptuo.WebStack.Http.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HttpWebContext = System.Web.HttpContext;

namespace Neptuo.WebStack.Http
{
    public class AspNetContext : DisposableBase, IHttpContext
    {
        private readonly HttpWebContext webContext;
        private readonly MappingFeatureModel features;
        private readonly IKeyValueCollection customValues;
        private readonly AspNetRequestMessage httpRequestMessage;
        private readonly AspNetResponseMessage httpResponseMessage;
        private readonly AspNetContextNotification notification;

        public AspNetContext(HttpWebContext webContext, IDependencyProvider dependencyProvider)
        {
            Ensure.NotNull(webContext, "webContext");
            this.webContext = webContext;
            this.customValues = new KeyValueCollection();
            this.httpRequestMessage = new AspNetRequestMessage(webContext.Request);
            this.httpResponseMessage = new AspNetResponseMessage(webContext.Response);
            this.notification = new AspNetContextNotification(webContext);
            this.features = new MappingFeatureModel(true, new Dictionary<Type, object>
            {
                { typeof(IHttpContextNotification), notification },
                { typeof(IUrlBuilder), new UrlBuilder(webContext.Request.ApplicationPath) },
                { typeof(IKeyValueCollection), customValues },
                { typeof(IHttpRequestMessage), httpRequestMessage },
                { typeof(IHttpResponseMessage), httpResponseMessage },
                { typeof(IDependencyProvider), dependencyProvider }
            });

            PrepareCustomValues();
        }

        private void PrepareCustomValues()
        {
            customValues.Set(RequestKey.QueryString, new HttpRequestParamCollection(new NameValueDictionary(webContext.Request.QueryString)));
            customValues.Set(RequestKey.Form, new HttpRequestParamCollection(new NameValueDictionary(webContext.Request.Form)));
            customValues.Set(RequestKey.Files, webContext.Request.Files.OfType<HttpPostedFile>().Select(f => new AspNetFile(f)));

            customValues.Set(ResponseKey.BodyWriter, webContext.Response.Output);
        }

        bool IFeatureModel.TryWith<TFeature>(out TFeature feature)
        {
            return features.TryWith(out feature);
        }

        public void FlushOutput()
        {
            using (notification.OnOutputFlush())
                webContext.Response.OutputStream.Flush();
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();

            notification
                .OnDispose()
                .Dispose();
        }
    }
}
