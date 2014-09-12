using Neptuo.WebStack.Hosting;
using Neptuo.WebStack.Services.Behaviors;
using Neptuo.WebStack.Services.Hosting;
using Neptuo.WebStack.Services.Hosting.Behaviors;
using Neptuo.WebStack.Services.Hosting.Pipelines.Compilation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.TestConsole.Routing
{
    class TestRouting
    {
        public static void Test()
        {
            Engine.Environment
                .UseCodeDomConfiguration(new CodeDomPipelineConfiguration("c:\\temp", Environment.CurrentDirectory))
                .UseBehaviors(provider =>
                {
                    provider
                        .AddMapping(typeof(IForInput<>), typeof(ForInputBehavior<>))
                        .AddMapping(typeof(IWithOutput<>), typeof(WithOutputBehavior<>))
                        .AddMapping(typeof(IWithRedirect), typeof(WithRedirectBehavior))
                        .AddMapping(typeof(IWithStatus), typeof(WithStatusBehavior));
                })
                .UseRouteTable(routeTable =>
                {
                    routeTable
                        .MapServices(Assembly.GetExecutingAssembly());
                });
        }
    }
}
