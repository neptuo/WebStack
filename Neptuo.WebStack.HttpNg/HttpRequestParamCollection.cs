using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// The collection of request parameters (query string or form) for extending.
    /// </summary>
    public class HttpRequestParamCollection : IReadOnlyKeyValueCollection
    {
        private readonly IReadOnlyKeyValueCollection storage;

        internal HttpRequestParamCollection(IReadOnlyKeyValueCollection storage)
        {
            Guard.NotNull(storage, "storage");
            this.storage = storage;
        }

        internal HttpRequestParamCollection(IDictionary<string, string> parameters)
        {
            Guard.NotNull(parameters, "parameters");
            KeyValueCollection storage = new KeyValueCollection();

            foreach (KeyValuePair<string, string> parameter in parameters)
                storage.Set(parameter.Key, parameter.Value);

            this.storage = storage;
        }

        public IEnumerable<string> Keys
        {
            get { return storage.Keys; }
        }

        public bool TryGet<T>(string key, out T value)
        {
            return storage.TryGet(key, out value);
        }
    }
}
