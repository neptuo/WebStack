using Neptuo.WebStack.Services.Behaviors;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Services.Hosting.Behaviors
{
    /// <summary>
    /// Implementation of <see cref="IWithStatus"/> contract.
    /// </summary>
    public class WithStatusBehavior : WithBehavior<IWithStatus>
    {
        protected override Task<bool> ExecuteAsync(IWithStatus handler, IHttpContext context)
        {
            if (handler.Status != null)
                context.Response().Status(handler.Status);

            return Task.FromResult(true);
        }
    }
}
