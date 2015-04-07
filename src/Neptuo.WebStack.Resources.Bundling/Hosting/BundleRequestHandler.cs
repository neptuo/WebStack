using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace Neptuo.WebStack.Resources.Bundling.Hosting
{
    /// <summary>
    /// Request handler for downloading bundles.
    /// </summary>
    public class BundleRequestHandler : IRequestHandler
    {
        public Task<bool> TryHandleAsync(IHttpContext httpContext)
        {
            Bundle bundle;
            if(httpContext.CustomValues().TryGetBundle(out bundle))
            {
                //TODO: Create bundle context, so BundleResponse can be created.
                BundleResponse response = null; throw Ensure.Exception.NotImplemented(); // bundle.GenerateBundleResponse(new BundleContext());
                httpContext.Response().OutputWriter().Write(response.Content);
                httpContext.Response().Headers().ContentType(new HttpMediaType(response.ContentType));

                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
