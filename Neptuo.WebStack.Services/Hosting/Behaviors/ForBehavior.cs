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
        /// <param name="httpRequest">Current HTTP request.</param>
        /// <param name="pipeline">Processing pipeline.</param>
        public async Task<IHttpResponse> ExecuteAsync(T handler, IHttpRequest httpRequest, IBehaviorContext pipeline)
        {
            IHttpResponse httpResponse = await ExecuteAsync(handler, httpRequest);
            if (httpResponse != null)
                return httpResponse;

            return await pipeline.NextAsync(httpRequest);
        }

        /// <summary>
        /// Invoked when processing 'Request' pipeline.
        /// If return value is not <c>null</c>, than this value is used as HTTP response and pipeline processing is stopped.
        /// Otherwise processing is promoted to the next behavior.
        /// </summary>
        /// <param name="handler">Behavior interface.</param>
        /// <param name="httpResponse">Current HTTP request.</param>
        /// <returns>Response for the current HTTP request.</returns>
        protected abstract Task<IHttpResponse> ExecuteAsync(T handler, IHttpRequest httpRequest);
    }
}
