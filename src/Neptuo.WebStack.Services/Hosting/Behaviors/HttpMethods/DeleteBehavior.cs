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
    /// Executes <see cref="IDelete"/> handler.
    /// </summary>
    public class DeleteBehavior : IBehavior<IDelete>
    {
        /// <summary>
        /// Executes <see cref="IDelete.ExecuteAsync"/> method on <paramref name="handler"/> if current request is DELETE request.
        /// </summary>
        /// <param name="handler">Behavior interface.</param>
        /// <param name="context">Behavior pipeline context.</param>
        public async Task ExecuteAsync(IDelete handler, IBehaviorContext context)
        {
            if (context.HttpContext().Request().IsMethodDelete())
            {
                if (!await handler.ExecuteAsync())
                    context.MarkAsNotHandled();
            }

            await context.NextAsync();
        }
    }
}
