using Neptuo.Activators;
using Neptuo.ComponentModel.Behaviors;
using Neptuo.ComponentModel.Behaviors.Processing;
using Neptuo.ComponentModel.Behaviors.Processing.Compilation;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Services.Hosting.Processing
{
    /// <summary>
    /// Base class for compiled pipeline.
    /// </summary>
    /// <typeparam name="T">Target handler type.</typeparam>
    public abstract class RequestPipelineBase<T> : PipelineBase<T>, IRequestHandler
        where T : new()
    {
        protected override IActivator<T> GetHandlerFactory()
        {
            return new DefaultActivator<T>();
        }

        protected override IBehaviorContext GetBehaviorContext(IEnumerable<IBehavior<T>> behaviors, T handler)
        {
            return new DefaultBehaviorContext<T>(behaviors, handler);
        }

        public async Task<bool> TryHandleAsync(IHttpContext httpContext)
        {
            IEnumerable<IBehavior<T>> behaviors = GetBehaviors();
            IActivator<T> handlerFactory = GetHandlerFactory();
            T handler = handlerFactory.Create();

            IBehaviorContext context = GetBehaviorContext(behaviors, handler);
            context.HttpContext(httpContext);
            
            await context.NextAsync();
            return context.IsHandled();
        }
    }
}
