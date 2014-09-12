using Neptuo.TestConsole.Routing.Services;
using Neptuo.WebStack.Hosting;
using Neptuo.WebStack.Hosting.Pipelines;
using Neptuo.WebStack.Hosting.Routing.Segments;
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
                });
                //.UseRouteTable(routeTable =>
                //{
                //    routeTable
                //        .MapServices(Assembly.GetExecutingAssembly());
                //});

            IPipelineFactory pipelineFactory = new CodeDomPipelineFactory(typeof(GetHelloHandler));

            RootRouteSegment rootSegment = new RootRouteSegment();
            rootSegment.IncludeUrl("~/cs/home", pipelineFactory);
            rootSegment.IncludeUrl("~/cs", pipelineFactory);
            rootSegment.IncludeUrl("~/cs/contacts", pipelineFactory);
            rootSegment.IncludeUrl("~/cs/contacts/send", pipelineFactory);
            rootSegment.IncludeUrl("~/cs/hosting", pipelineFactory);

            PrintSegment(rootSegment, 0);
        }

        private static void PrintSegment(RouteSegment routeSegment, int indent)
        {
            PrintLine(routeSegment.UrlPart, indent);

            foreach (RouteSegment childRouteSegment in routeSegment.Children)
                PrintSegment(childRouteSegment, indent + 1);
        }

        private static void PrintLine(string message, int indent)
        {
            for (int i = 0; i < indent; i++)
                Console.Write("  ");

            Console.WriteLine(message);
        }
    }
}
