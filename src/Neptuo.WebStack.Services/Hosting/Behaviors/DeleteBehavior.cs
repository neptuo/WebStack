using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Services.Hosting.Behaviors
{
    /// <summary>
    /// Executes <see cref="IDelete"/> handler.
    /// </summary>
    public class DeleteBehavior : IBehavior<IDelete>
    {
        /// <summary>
        /// Executes <see cref="IDelete.ExecuteAsync"/> method on <paramref name="handler"/> if current request is DELETE request.
        /// </summary>
        /// <param name="handler">Behavior interface.</param>
        /// <param name="context">Current HTTP context.</param>
        /// <param name="pipeline">Processing pipeline.</param>
        public async Task<bool> ExecuteAsync(IDelete handler, IHttpContext httpContext, IBehaviorContext pipeline)
        {
            if (httpContext.Request().IsMethodDelete())
                return await handler.ExecuteAsync();

            return await pipeline.NextAsync(httpContext);
        }
    }
}
