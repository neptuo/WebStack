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
        /// Current Http request context that this pipeline operates on.
        /// </summary>
        private IHttpContext context;

        /// <summary>
        /// Instance of handler to execute.
        /// </summary>
        private T handler;

        /// <summary>
        /// Gets factory for handlers of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="context">Current Http request context.</param>
        /// <returns>Factory for handlers of type <typeparamref name="T"/>.</returns>
        protected abstract IHandlerFactory<T> GetHandlerFactory(IHttpContext context);

        /// <summary>
        /// Gets enumeration of behaviors for handler of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="context">Current Http request context.</param>
        /// <returns>Enumeration of behaviors for handler of type <typeparamref name="T"/>.</returns>
        protected abstract IEnumerable<IBehavior<T>> GetBehaviors(IHttpContext context);

        /// <summary>
        /// Creates instance of handler and using <see cref="IBehavior"/> executes action.
        /// </summary>
        /// <param name="context">Current Http request context.</param>
        public async Task<bool> TryHandleAsync(IHttpContext context)
        {
            IHandlerFactory<T> handlerFactory = GetHandlerFactory(context);
            this.handler = handlerFactory.Create(context);
            this.context = context;

            behaviorEnumerator = GetBehaviors(context).GetEnumerator();
            await NextAsync();

            return true;
        }

        /// <summary>
        /// Moves to next processing to next behavior.
        /// </summary>
        public Task NextAsync()
        {
            if (behaviorEnumerator.MoveNext())
                return behaviorEnumerator.Current.ExecuteAsync(handler, context, this);

            return Task.FromResult(true);
        }
    }
}
