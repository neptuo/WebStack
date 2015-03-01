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
    /// Implementation of <see cref="IWithRedirect"/> contract.
    /// </summary>
    public class WithRedirectBehavior : WithBehavior<IWithRedirect>
    {
        protected override Task<bool> ExecuteAsync(IWithRedirect handler, IHttpContext httpContext)
        {
            if (!String.IsNullOrEmpty(handler.Location))
                httpContext.Response().Headers().Set("Location", handler.Location); //TODO: Resolve URL or use IReadOnlyUrl?

            return Task.FromResult(true);
        }
    }
}
