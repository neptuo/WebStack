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
    /// Executes <see cref="IPost"/> handler.
    /// </summary>
    public class PostBehavior : IBehavior<IPost>
    {
        /// <summary>
        /// Executes <see cref="IPost.ExecuteAsync"/> method on <paramref name="handler"/> if current request is POST request.
        /// </summary>
        /// <param name="handler">Behavior interface.</param>
        /// <param name="context">Behavior pipeline context.</param>
        public async Task ExecuteAsync(IPost handler, IBehaviorContext context)
        {
            if (context.HttpContext().Request().IsMethodPost())
            {
                if (!await handler.ExecuteAsync())
                    context.MarkAsNotHandled();
            }

            await context.NextAsync();
        }
    }
}
