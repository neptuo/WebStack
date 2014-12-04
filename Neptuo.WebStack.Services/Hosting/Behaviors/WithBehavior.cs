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
        /// <param name="httpRequest">Current HTTP request.</param>
        /// <param name="pipeline">Processing pipeline.</param>
        public async Task<IHttpResponse> ExecuteAsync(T handler, IHttpRequest httpRequest, IBehaviorContext pipeline)
        {
            IHttpResponse httpResponse = await pipeline.NextAsync(httpRequest);
            if (httpResponse == null)
                return null;

            return await ExecuteAsync(handler, httpRequest, httpResponse);
        }

        /// <summary>
        /// Invoked when processing 'Response' pipeline.
        /// </summary>
        /// <param name="handler">Behavior interface.</param>
        /// <param name="httpRequest">Current HTTP request.</param>
        /// <param name="httpResponse">Response for the current HTTP request.</param>
        protected abstract Task<IHttpResponse> ExecuteAsync(T handler, IHttpRequest httpRequest, IHttpResponse httpResponse);
    }
}
