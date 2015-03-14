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
    /// Executes <see cref="IGet"/> handler.
    /// </summary>
    public class GetBehavior : IBehavior<IGet>
    {
        /// <summary>
        /// Executes <see cref="IGet.ExecuteAsync"/> method on <paramref name="handler"/> if current request is GET request.
        /// </summary>
        /// <param name="handler">Behavior interface.</param>
        /// <param name="context">Behavior pipeline context.</param>
        public async Task ExecuteAsync(IGet handler, IBehaviorContext context)
        {
            if (context.HttpContext().Request().IsMethodGet())
            {
                if (!await handler.ExecuteAsync())
                    context.MarkAsNotHandled();
            }

            await context.NextAsync();
        }
    }
}
