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

            PathRouteSegment rootSegment = new PathRouteSegment();
            rootSegment.IncludeSegment("~/cs/contacts/send", pipelineFactory);
            rootSegment.IncludeSegment("~/cs/contacts", pipelineFactory);
            rootSegment.IncludeSegment("~/cs/home", pipelineFactory);
            rootSegment.IncludeSegment("~/cs/hosting", pipelineFactory);
            rootSegment.IncludeSegment("~/cs", pipelineFactory);

            PrintSegment(rootSegment, 0);
        }

        private static void PrintSegment(IRouteSegment routeSegment, int indent)
        {
            IStaticRouteSegment staticSegment = routeSegment as IStaticRouteSegment;
            if (staticSegment != null)
                PrintLine(staticSegment.UrlPart, indent);

            foreach (IRouteSegment childRouteSegment in routeSegment.Children)
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
