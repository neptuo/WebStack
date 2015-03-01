using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpWebResponse = System.Web.HttpResponse;

namespace Neptuo.WebStack.Http.Messages
{
    public class AspNetResponseMessage : IHttpResponseMessage
    {
        private readonly HttpWebResponse webResponse;
        private IDictionary<string, string> webHeaders;

        public string Protocol
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public int StatusCode
        {
            get { return webResponse.StatusCode; }
            set { webResponse.StatusCode = value; }
        }

        public string StatusText
        {
            get { return webResponse.StatusDescription; }
            set { webResponse.StatusDescription = value; }
        }

        public IDictionary<string, string> Headers
        {
            get
            {
                if (webHeaders == null)
                    webHeaders = new NameValueDictionary(webResponse.Headers);

                return webHeaders;
            }
        }

        public Stream BodyStream
        {
            get { return webResponse.OutputStream; }
        }

        public AspNetResponseMessage(HttpWebResponse webResponse)
        {
            Guard.NotNull(webResponse, "webResponse");
            this.webResponse = webResponse;
        }
    }
}
