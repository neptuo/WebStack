using Neptuo.WebStack;
using Neptuo.WebStack.Http;
using Neptuo.WebStack.Services.Hosting.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Services.Hosting.Pipelines
{
    /// <summary>
    /// Base implementation of pipeline that operates on handler of type <typeparamref name="T"/>.
    /// Integrates execution of behaviors during handler execution.
    /// </summary>
    /// <typeparam name="T">Type of handler.</typeparam>
    public abstract class PipelineBase<T> : IRequestHandler, IBehaviorContext
    {
        /// <summary>
        /// Enumerator for behaviors for type <typeparamref name="T" />
        /// </summary>
        private IEnumerator<IBehavior<T>> behaviorEnumerator;

        /// <summary>
        /// Instance of handler to execute.
        /// </summary>
        private T handler;

        /// <summary>
        /// Gets factory for handlers of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="httpRequest">Current Http request context.</param>
        /// <returns>Factory for handlers of type <typeparamref name="T"/>.</returns>
        protected abstract IHandlerFactory<T> GetHandlerFactory(IHttpRequest httpRequest);

        /// <summary>
        /// Gets enumeration of behaviors for handler of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="context">Current Http request context.</param>
        /// <returns>Enumeration of behaviors for handler of type <typeparamref name="T"/>.</returns>
        protected abstract IEnumerable<IBehavior<T>> GetBehaviors(IHttpRequest httpRequest);

        /// <summary>
        /// Creates instance of handler and using <see cref="IBehavior"/> executes action.
        /// </summary>
        /// <param name="httpRequest">Current HTTP request.</param>
        /// <returns>Response for the current HTTP request.</returns>
        public async Task<IHttpResponse> TryHandleAsync(IHttpRequest httpRequest)
        {
            IHandlerFactory<T> handlerFactory = GetHandlerFactory(httpRequest);
            this.handler = handlerFactory.Create(httpRequest);

            behaviorEnumerator = GetBehaviors(httpRequest).GetEnumerator();
            return await NextAsync(httpRequest);
        }

        /// <summary>
        /// Moves to next processing to next behavior.
        /// </summary>
        /// <param name="httpRequest">Current HTTP request.</param>
        public Task<IHttpResponse> NextAsync(IHttpRequest httpRequest)
        {
            // Try to call next behavior in pipeline.
            if (behaviorEnumerator.MoveNext())
                return behaviorEnumerator.Current.ExecuteAsync(handler, httpRequest, this);

            // No more behaviors equal to inability process request this way.
            return Task.FromResult<IHttpResponse>(null);
        }
    }
}
