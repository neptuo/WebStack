using Neptuo.WebStack.Http;
using Neptuo.WebStack.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace Neptuo.WebStack.Resources.Bundling.Hosting.Parameters
{
    /// <summary>
    /// Parameter for finding bundle from URL.
    /// </summary>
    public class BundleParameter : IRouteParameter
    {
        private readonly BundleCollection bundleCollection;

        public BundleParameter()
            : this(BundleTable.Bundles)
        { }

        public BundleParameter(BundleCollection bundleCollection)
        {
            Ensure.NotNull(bundleCollection, "bundleCollection");
            this.bundleCollection = bundleCollection;
        }

        public bool MatchUrl(IRouteParameterMatchContext context)
        {
            string virtualUrl = (context.RemainingUrl.StartsWith("/") ? "~" : "~/") + context.RemainingUrl;
            Bundle bundle = bundleCollection.GetBundleFor(virtualUrl);
            if (bundle == null)
                return false;

            context.HttpContext.CustomValues().Bundle(bundle);
            context.RemainingUrl = null;
            return true;
        }
    }
}
