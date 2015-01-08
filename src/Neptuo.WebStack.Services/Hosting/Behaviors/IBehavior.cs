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
        /// <param name="context">Current HTTP request.</param>
        /// <param name="pipeline">Processing pipeline.</param>
        /// <returns>HTTP response for current request.</returns>
        Task<IHttpResponse> ExecuteAsync(T handler, IHttpRequest httpRequest, IBehaviorContext pipeline);
    }
}
