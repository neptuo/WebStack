using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http.Collections.Specialized
{
    public class PrefixKeyValueCollection : IKeyValueCollection
    {
        private readonly string requiredPrefix;
        private readonly IKeyValueCollection collection;

        public PrefixKeyValueCollection(string requiredPrefix, IKeyValueCollection collection)
        {
            Guard.NotNullOrEmpty(requiredPrefix, "requiredPrefix");
            Guard.NotNull(collection, "collection");
            this.requiredPrefix = requiredPrefix;
            this.collection = collection;
        }

        public IKeyValueCollection Set(string key, object value)
        {
            key = PrepareKey(key);
            collection.Set(key, value);
            return this;
        }

        public IEnumerable<string> Keys
        {
            get { return collection.Keys; }
        }

        public bool TryGet<T>(string key, out T value)
        {
            key = PrepareKey(key);
            return collection.TryGet(key, out value);
        }

        private string PrepareKey(string key)
        {
            Guard.NotNull(key, "key");

            if (!key.StartsWith(requiredPrefix))
                key = requiredPrefix + key;

            return key;
        }
    }
}
