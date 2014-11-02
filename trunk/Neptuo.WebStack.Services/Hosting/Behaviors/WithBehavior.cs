﻿using Neptuo.WebStack.Http;
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
        /// <param name="context">Current Http context.</param>
        /// <param name="pipeline">Processing pipeline.</param>
        public async Task ExecuteAsync(T handler, IHttpContext context, IBehaviorContext pipeline)
        {
            await pipeline.NextAsync();
            await ExecuteAsync(handler, context);
        }

        /// <summary>
        /// Invoked when processing 'Response' pipeline.
        /// </summary>
        /// <param name="handler">Behavior interface.</param>
        /// <param name="context">Current Http context.</param>
        protected abstract Task ExecuteAsync(T handler, IHttpContext context);
    }
}
