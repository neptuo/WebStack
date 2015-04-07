using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace Neptuo.WebStack.Resources.Bundling.Hosting
{
    /// <summary>
    /// Bundle extensions for <see cref="IKeyValueCollection"/>.
    /// </summary>
    public static class _KeyValueCollectionExtensions
    {
        public static IKeyValueCollection Bundle(this IKeyValueCollection collection, Bundle bundle)
        {
            Ensure.NotNull(collection, "collection");
            return collection.Set("Bundle", bundle);
        }

        public static bool TryGetBundle(this IReadOnlyKeyValueCollection collection, out Bundle bundle)
        {
            Ensure.NotNull(collection, "collection");
            return collection.TryGet("Bundle", out bundle);
        }
    }
}
