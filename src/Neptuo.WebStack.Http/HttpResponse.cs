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
    public class HttpResponse
    {
        private readonly IHttpContext httpContext;
        private readonly IKeyValueCollection customValues;

        internal HttpResponse(IHttpContext httpContext)
        {
            Ensure.NotNull(httpContext, "httpContext");
            this.httpContext = httpContext;
            this.customValues = new PrefixKeyValueCollection("Response.", httpContext.CustomValues());
        }

        public IHttpContext Context()
        {
            return httpContext;
        }

        public IKeyValueCollection CustomValues()
        {
            return customValues;
        }

        public IHttpResponseMessage RawMessage()
        {
            return httpContext.ResponseMessage();
        }
    }
}
