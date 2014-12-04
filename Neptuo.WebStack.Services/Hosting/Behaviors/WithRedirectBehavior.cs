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
        protected override Task<IHttpResponse> ExecuteAsync(IWithRedirect handler, IHttpRequest httpRequest, IHttpResponse httpResponse)
        {
            if (!String.IsNullOrEmpty(handler.Location))
                httpRequest.Header("Location", handler.Location); //TODO: Resolve URL or use IReadOnlyUrl?

            return Task.FromResult(httpResponse);
        }
    }
}
