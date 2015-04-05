using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Resources.Hosting
{
    public class ResourceRequestHandler : IRequestHandler
    {
        private readonly IResourceCollection resources;

        public ResourceRequestHandler(IResourceCollection resources)
        {
            Ensure.NotNull(resources, "resources");
            this.resources = resources;
        }

        public Task<bool> TryHandleAsync(IHttpContext httpContext)
        {
            throw new NotImplementedException();
        }
    }
}
