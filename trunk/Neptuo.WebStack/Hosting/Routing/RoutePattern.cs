using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Hosting.Routing
{
    /// <summary>
    /// Contains single route URL and Http method pattern.
    /// Support protocols, domains, app-relative URLs etc.
    /// </summary>
    public struct RoutePattern
    {
        public HttpMethod Method { get; set; }

        public string Protocol { get; set; }
        public string Domain { get; set; }
        public string Path { get; set; }
    }
}
