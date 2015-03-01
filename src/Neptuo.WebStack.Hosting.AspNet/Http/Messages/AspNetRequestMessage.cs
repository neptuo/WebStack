﻿using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpWebRequest = System.Web.HttpRequest;

namespace Neptuo.WebStack.Http.Messages
{
    public class AspNetRequestMessage : IHttpRequestMessage
    {
        private readonly HttpWebRequest webRequest;
        private IDictionary<string, string> webHeaders;

        public string Method
        {
            get { return webRequest.HttpMethod; }
        }

        public string Url
        {
            get { return webRequest.RawUrl; }
        }

        public string Protocol
        {
            get { throw Guard.Exception.NotImplemented(); }
        }

        public IDictionary<string, string> Headers
        {
            get
            {
                if (webHeaders == null)
                    webHeaders = new NameValueDictionary(webRequest.Headers);

                return webHeaders;
            }
        }

        public Stream BodyStream
        {
            get { return webRequest.InputStream; }
        }

        public AspNetRequestMessage(HttpWebRequest webRequest)
        {
            Guard.NotNull(webRequest, "webRequest");
            this.webRequest = webRequest;
        }
    }
}
