using Neptuo.WebStack.Services.Hosting.Behaviors;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Services.Hosting.Pipelines
{
    /// <summary>
    /// Pipeline for handlers with parameterless constructor.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DefaultPipelineBase<T> : PipelineBase<T>
        where T: new()
    {
        protected override IHandlerFactory<T> GetHandlerFactory(IHttpRequest httpRequest)
        {
            return new DefaultHandlerFactory<T>();
        }
    }
}
