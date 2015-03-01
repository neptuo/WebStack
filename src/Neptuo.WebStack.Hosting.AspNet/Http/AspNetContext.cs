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
    public class AspNetContext : IHttpContext
    {
        private readonly HttpWebContext webContext;
        private readonly MappingFeatureModel features;
        private readonly IKeyValueCollection customValues;
        private readonly AspNetRequestMessage httpRequestMessage;
        private readonly AspNetResponseMessage httpResponseMessage;
        private readonly AspNetContextNotification notification;

        public AspNetContext(HttpWebContext webContext)
        {
            Guard.NotNull(webContext, "webContext");
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
                { typeof(IHttpResponseMessage), httpResponseMessage }
            });
        }

        public bool TryWith<TFeature>(out TFeature feature)
        {
            return features.TryWith(out feature);
        }

        #region IHttpContextNotification

        public event Action OnHeadersSending;
        public event Action OnHeadersSent;

        public event Action OnOutputFlushing;
        public event Action OnOutputFlushed;

        public event Action OnDisposing;
        public event Action OnDisposed;

        #endregion
    }
}
