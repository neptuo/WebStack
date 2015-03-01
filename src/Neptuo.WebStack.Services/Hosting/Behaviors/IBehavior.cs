using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Services.Hosting.Behaviors
{
    /// <summary>
    /// Integrates logic into execution pipeline.
    /// </summary>
    /// <typeparam name="T">Type of behavior interface.</typeparam>
    public interface IBehavior<in T>
    {
        /// <summary>
        /// Invoked when processing pipeline.
        /// </summary>
        /// <param name="handler">Behavior interface.</param>
        /// <param name="context">Current HTTP context.</param>
        /// <param name="pipeline">Processing pipeline.</param>
        /// <returns><c>true</c> if request was handled; <c>false</c> to process request by next handler.</returns>
        Task<bool> ExecuteAsync(T handler, IHttpContext httpContext, IBehaviorContext pipeline);
    }
}
