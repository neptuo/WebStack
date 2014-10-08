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
        protected override void Execute(IWithRedirect handler, IHttpContext context)
        {
            if (!String.IsNullOrEmpty(handler.Location))
                context.Response().Headers.Add("Location", context.ResolveUrl(handler.Location));
        }
    }
}
