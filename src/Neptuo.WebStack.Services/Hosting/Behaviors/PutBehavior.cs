using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Services.Hosting.Behaviors
{
    /// <summary>
    /// Executes <see cref="IPut"/> handler.
    /// </summary>
    public class PutBehavior : IBehavior<IPut>
    {
        /// <summary>
        /// Executes <see cref="IPut.ExecuteAsync"/> method on <paramref name="handler"/> if current request is PUT request.
        /// </summary>
        /// <param name="handler">Behavior interface.</param>
        /// <param name="httpContext">Current HTTP context.</param>
        /// <param name="pipeline">Processing pipeline.</param>
        public async Task<bool> ExecuteAsync(IPut handler, IHttpContext httpContext, IBehaviorContext pipeline)
        {
            if (httpContext.Request().IsMethodPut())
                return await handler.ExecuteAsync();

            return await pipeline.NextAsync(httpContext);
        }
    }
}
