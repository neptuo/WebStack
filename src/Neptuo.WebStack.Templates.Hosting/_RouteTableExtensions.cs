using Neptuo.FileSystems;
using Neptuo.WebStack.Http;
using Neptuo.WebStack.Routing;
using Neptuo.WebStack.Templates.Hosting.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Templates.Hosting
{
    /// <summary>
    /// Templates extensions for <see cref="IRouteTable"/>.
    /// </summary>
    public static class _RouteTableExtensions
    {
        public static IRouteTable MapUiTemplate(this IRouteTable routeTable, IReadOnlyUrl routePattern, IReadOnlyFile templateFile)
        {
            Ensure.NotNull(routeTable, "routeTable");
            return routeTable.Map(routePattern, new TemplateRequestHandler(templateFile));
        }
    }
}
