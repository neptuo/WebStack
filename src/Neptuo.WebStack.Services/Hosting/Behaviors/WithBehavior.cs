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
        /// Invokes abstract <see cref="ExecuteAsync"/> after promoting to next behavior in pipeline.
        /// </summary>
        /// <param name="handler">Behavior interface.</param>
        /// <param name="httpContext">Current HTTP context.</param>
        /// <param name="pipeline">Processing pipeline.</param>
        public async Task<bool> ExecuteAsync(T handler, IHttpContext httpContext, IBehaviorContext pipeline)
        {
            bool result = await pipeline.NextAsync(httpContext);
            if(!result)
                return false;

            return await ExecuteAsync(handler, httpContext);
        }

        /// <summary>
        /// Invoked when processing 'Response' pipeline.
        /// </summary>
        /// <param name="handler">Behavior interface.</param>
        /// <param name="httpContext">Current HTTP context.</param>
        protected abstract Task<bool> ExecuteAsync(T handler, IHttpContext httpContext);
    }
}
