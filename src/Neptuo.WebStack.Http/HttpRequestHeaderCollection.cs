using Neptuo.Collections.Specialized;
using Neptuo.WebStack.Http.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    public class HttpRequestHeaderCollection : IReadOnlyKeyValueCollection
    {
        private readonly IHttpRequestMessage httpRequest;
        private readonly KeyValueCollection storage;
        
        internal HttpRequestHeaderCollection(IHttpRequestMessage httpRequest)
        {
            Guard.NotNull(httpRequest, "httpRequest");
            this.httpRequest = httpRequest;
            this.storage = new KeyValueCollection();

            foreach (KeyValuePair<string, string> header in httpRequest.Headers)
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

        public T GetOrDefault<T>(string key, T defaultValue)
        {
            T value;
            if (TryGet(key, out value))
                return value;

            return defaultValue;
        }
    }
}
