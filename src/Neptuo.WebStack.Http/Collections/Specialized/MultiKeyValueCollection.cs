using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http.Collections.Specialized
{
    public class MultiKeyValueCollection : IReadOnlyKeyValueCollection
    {
        private readonly IReadOnlyKeyValueCollection[] collections;

        public MultiKeyValueCollection(params IReadOnlyKeyValueCollection[] collections)
        {
            Ensure.NotNull(collections, "collections");
            this.collections = collections;
        }

        public IEnumerable<string> Keys
        {
            get { return collections.SelectMany(c => c.Keys); }
        }

        public bool TryGet<T>(string key, out T value)
        {
            foreach (IReadOnlyKeyValueCollection collection in collections)
            {
                if (collection.TryGet(key, out value))
                    return true;
            }

            value = default(T);
            return false;
        }
    }
}
