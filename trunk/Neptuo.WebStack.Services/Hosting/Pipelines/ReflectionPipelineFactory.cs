using Neptuo.WebStack.Hosting.Routing;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Services.Hosting.Pipelines
{
    /// <summary>
    /// Creates instances of <see cref="ReflectionPipeline"/>.
    /// </summary>
    public class ReflectionPipelineFactory<T> : IRouteHandler
        where T : new()
    {
        public async Task HandleAsync(IHttpContext httpContext)
        {
            IRouteHandler pipeline = new ReflectionPipeline<T>(Engine.Environment.WithBehaviors());
            await pipeline.HandleAsync(httpContext);
        }
    }
}
