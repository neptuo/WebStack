using Neptuo.Collections.Specialized;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Routing
{
    /// <summary>
    /// Holds routes and mapped pipelines.
    /// </summary>
    public interface IRouteTable
    {
        /// <summary>
        /// Gets instance of URL builder for mapping URLs.
        /// </summary>
        IUrlBuilder UrlBuilder();

        /// <summary>
        /// Maps <paramref name="pipelineFactory"/> to <paramref name="routePattern"/>.
        /// </summary>
        /// <param name="routePattern">Pattern to regiter <paramref name="requestHandler"/> on.</param>
        /// <param name="target">Route target.</param>
        /// <rereturns>Self (for fluency).</rereturns>
        IRouteTable Map(IReadOnlyUrl routePattern, object target);

        /// <summary>
        /// Tries to find target for <paramref name="url"/>.
        /// </summary>
        /// <param name="url">Url to find target for.</param>
        /// <param name="routeValues">Collection for custom route values.</param>
        /// <param name="target">Target for <paramref name="url"/>.</param>
        /// <returns></returns>
        bool TryGetTarget(IReadOnlyUrl url, IKeyValueCollection routeValues, out object target);
    }
}
