using Neptuo.Collections.Specialized;
using Neptuo.WebStack.Http.Collections.Specialized;
using Neptuo.WebStack.Http.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    public sealed class HttpRequest
    {
        private readonly IHttpContext httpContext;
        private readonly IKeyValueCollection customValues;

        internal HttpRequest(IHttpContext httpContext)
        {
            Ensure.NotNull(httpContext, "httpContext");
            this.httpContext = httpContext;
            this.customValues = new PrefixKeyValueCollection("Request.", httpContext.CustomValues());
        }

        public IHttpContext Context()
        {
            return httpContext;
        }

        public IKeyValueCollection CustomValues()
        {
            return customValues;
        }

        public IHttpRequestMessage RawMessage()
        {
            return httpContext.RequestMessage();
        }
    }
}
