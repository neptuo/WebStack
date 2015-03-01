using Neptuo.Collections.Specialized;
using Neptuo.WebStack.Http.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    public class HttpResponseHeaderCollection : IKeyValueCollection
    {
        private readonly IHttpResponseMessage httpResponse;
        private readonly KeyValueCollection storage;

        internal HttpResponseHeaderCollection(IHttpResponseMessage httpResponse)
        {
            Guard.NotNull(httpResponse, "httpResponse");
            this.httpResponse = httpResponse;
            this.storage = new KeyValueCollection();

            foreach (KeyValuePair<string, string> header in httpResponse.Headers)
                storage.Set(header.Key, header.Value);
        }

        public IEnumerable<string> Keys
        {
            get { return storage.Keys; }
        }

        public bool TryGet<T>(string key, out T value)
        {
            return storage.TryGet(key, out value);
        }

        IKeyValueCollection IKeyValueCollection.Set(string key, object value)
        {
            return Set(key, value);
        }

        public HttpResponseHeaderCollection Set(string key, object value)
        {
            storage.Set(key, value);
            httpResponse.Headers[key] = Converts.To<object, string>(value);
            return this;
        }
    }
}
