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
        /// <param name="context">Current Http context.</param>
        /// <param name="pipeline">Processing pipeline.</param>
        public Task ExecuteAsync(IPut handler, IHttpContext context, IBehaviorContext pipeline)
        {
            if (context.Request().IsMethodPut())
                return handler.ExecuteAsync();
            else
                return pipeline.NextAsync();
        }
    }
}
