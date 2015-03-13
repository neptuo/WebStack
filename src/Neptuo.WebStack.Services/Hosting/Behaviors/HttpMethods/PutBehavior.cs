using Neptuo.ComponentModel.Behaviors;
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
        /// <param name="context">Behavior pipeline context.</param>
        public async Task ExecuteAsync(IPut handler, IBehaviorContext context)
        {
            if (context.HttpContext().Request().IsMethodPut())
            {
                if (!await handler.ExecuteAsync())
                    context.MarkAsNotHandled();
            }

            await context.NextAsync();
        }
    }
}
