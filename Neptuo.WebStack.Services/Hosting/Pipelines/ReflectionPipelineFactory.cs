using Neptuo.WebStack.Hosting.Pipelines;
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
    public class ReflectionPipelineFactory<T> : IPipelineFactory
        where T : new()
    {
        public IPipeline Create()
        {
            return new ReflectionPipeline<T>(Engine.Environment.WithBehaviors());
        }
    }
}
