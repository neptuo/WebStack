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
        /// Invokes abstract <see cref="ExecuteAsync"/> before promoting to next behavior in pipeline.
        /// </summary>
        /// <param name="handler">Behavior interface.</param>
        /// <param name="httpContext">Current HTTP context.</param>
        /// <param name="pipeline">Processing pipeline.</param>
        public async Task<bool> ExecuteAsync(T handler, IHttpContext httpContext, IBehaviorContext pipeline)
        {
            if (await ExecuteAsync(handler, httpContext))
                return true;

            return await pipeline.NextAsync(httpContext);
        }

        /// <summary>
        /// Invoked when processing 'Request' pipeline.
        /// If return value is not <c>null</c>, than this value is used as HTTP response and pipeline processing is stopped.
        /// Otherwise processing is promoted to the next behavior.
        /// </summary>
        /// <param name="handler">Behavior interface.</param>
        /// <param name="httpContext">Current HTTP context.</param>
        /// <returns><c>true</c> if request was handled; <c>false</c> to process request by next handler.</returns>
        protected abstract Task<bool> ExecuteAsync(T handler, IHttpContext httpContext);
    }
}
