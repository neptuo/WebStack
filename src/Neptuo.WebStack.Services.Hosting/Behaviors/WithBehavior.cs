using Neptuo.ComponentModel.Behaviors;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Services.Hosting.Behaviors
{
    /// <summary>
    /// Base class for behavior in 'Response' processing pipeline.
    /// </summary>
    /// <typeparam name="T">Type of behavior interface.</typeparam>
    public abstract class WithBehavior<T> : IBehavior<T>
    {
        /// <summary>
        /// Invoked when processing 'Response' pipeline.
        /// </summary>
        /// <param name="handler">Behavior interface.</param>
        /// <param name="httpContext">Current HTTP context.</param>
        protected abstract Task<bool> ExecuteAsync(T handler, IHttpContext httpContext);

        public async Task ExecuteAsync(T handler, IBehaviorContext context)
        {
            await context.NextAsync();

            if (!await ExecuteAsync(handler, context.HttpContext()))
                context.MarkAsNotHandled();
        }
    }
}
