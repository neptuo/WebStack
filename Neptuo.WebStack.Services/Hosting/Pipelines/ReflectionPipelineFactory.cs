using Neptuo.WebStack;
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
        public async Task<IHttpResponse> TryHandleAsync(IHttpRequest httpRequest)
        {
            IRequestHandler pipeline = new ReflectionPipeline<T>(Engine.Environment.WithBehaviors());
            return await pipeline.TryHandleAsync(httpRequest);
        }
    }
}
