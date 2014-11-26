using Neptuo.WebStack.Services;
using Neptuo.WebStack.Services.Behaviors;
using Neptuo.WebStack.Services.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.TestConsole.Routing.Services
{
    [Route("~/hello")]
    public class GetHelloHandler : IGet, IWithOutput<string>
    {
        public string Output { get; private set; }

        public Task<bool> ExecuteAsync()
        {
            Output = "Hello, World!";
            return Task.FromResult(true);
        }
    }
}
