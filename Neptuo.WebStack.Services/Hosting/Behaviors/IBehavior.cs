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
        /// <param name="context">Current Http context.</param>
        /// <param name="pipeline">Processing pipeline.</param>
        Task ExecuteAsync(T handler, IHttpContext context, IBehaviorContext pipeline);
    }
}
