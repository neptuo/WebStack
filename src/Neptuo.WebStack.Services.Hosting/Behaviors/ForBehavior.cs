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
    /// Base class for behavior in 'Request' processing pipeline.
    /// </summary>
    /// <typeparam name="T">Type of behavior interface.</typeparam>
    public abstract class ForBehavior<T> : IBehavior<T>
    {
        /// <summary>
        /// Invoked when processing 'Request' pipeline.
        /// If return value is not <c>null</c>, than this value is used as HTTP response and pipeline processing is stopped.
        /// Otherwise processing is promoted to the next behavior.
        /// </summary>
        /// <param name="handler">Behavior interface.</param>
        /// <param name="httpContext">Current HTTP context.</param>
        /// <returns><c>true</c> if request was handled; <c>false</c> to process request by next handler.</returns>
        protected abstract Task<bool> ExecuteAsync(T handler, IHttpContext httpContext);

        public async Task ExecuteAsync(T handler, IBehaviorContext context)
        {
            if (await ExecuteAsync(handler, context.HttpContext()))
                return;

            await context.NextAsync();
        }
    }
}
