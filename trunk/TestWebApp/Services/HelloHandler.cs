using Neptuo.WebStack.Services;
using Neptuo.WebStack.Services.Behaviors;
using Neptuo.WebStack.Services.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TestWebApp.Services
{
    [Route("~/hello")]
    public class HelloHandler : IGet, IPost, IForInput<string>, IWithOutput<string>
    {
        public string Input { get; set; }
        public string Output { get; private set; }

        public Task ExecuteAsync()
        {
            Output = String.Format("Hello, {0}!", Input);
            return Task.FromResult(true);
        }
    }
}