using Neptuo.WebStack.Hosting;
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
    public class ReflectionPipelineFactory<T> : IRequestHandler
        where T : new()
    {
        public async Task<bool> TryHandleAsync(IHttpContext httpContext)
        {
            IRequestHandler pipeline = new ReflectionPipeline<T>(Engine.Environment.WithBehaviors());
            await pipeline.TryHandleAsync(httpContext);
            return true;
        }
    }
}
