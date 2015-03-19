using Neptuo.Collections.Specialized;
using Neptuo.FileSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Templates.Hosting
{
    public static class _KeyValueCollectionExtensions
    {
        public static IKeyValueCollection TemplateFile(this IKeyValueCollection collection, IReadOnlyFile templateFile)
        {
            Ensure.NotNull(collection, "collection");
            return collection.Set("TemplateFile", templateFile);
        }

        public static bool TryGetTemplateFile(this IReadOnlyKeyValueCollection collection, out IReadOnlyFile templateFile)
        {
            Ensure.NotNull(collection, "collection");
            return collection.TryGet("TemplateFile", out templateFile);
        }
    }
}
