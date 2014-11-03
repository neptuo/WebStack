using Neptuo.Diagnostics;
using Neptuo.TestConsole.Routing.Services;
using Neptuo.WebStack.Http;
using Neptuo.WebStack.Hosting;
using Neptuo.WebStack.Hosting.Routing;
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
    class TestRouting : DebugHelper
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
                .UseParameterCollection(collection =>
                {
                    collection
                        .Add("lang", new TestRouteParameter())
                        .Add("product", new TestRouteParameter())
                        .Add("destination", new TestRouteParameter());
                })
                .UseRouteTable(routeTable =>
                {
                    routeTable
                        .MapServices(Assembly.GetExecutingAssembly());
                });

            IRequestHandler requestHandler = new CodeDomPipelineFactory(typeof(GetHelloHandler));

            //PathRouteSegment rootSegment = new PathRouteSegment();
            //DebugIteration("Build route table", 1, () =>
            //{
            //    rootSegment.Include(new StaticRouteSegment("~/cs/contacts/send"));
            //    rootSegment.Include(new StaticRouteSegment("~/cs/home"));
            //    rootSegment.Include(new StaticRouteSegment("~/cs/hosting"));
            //    rootSegment.Include(new StaticRouteSegment("~/cs/contacts"));
            //    rootSegment.Include(new StaticRouteSegment("~/cs"));
            //});

            Engine.Environment.WithRouteTable()
                .Map("~/cs/home", requestHandler)
                .Map("~/cs/about", requestHandler)
                .Map("~/cs/{destination}", requestHandler)
                .Map("~/cs/{destination}/products", requestHandler)
                .Map("~/cs/{destination}/photo", requestHandler)
                .Map("~/cs/{destination}/{product}", requestHandler)
                .Map("~/cs/{destination}/{product}/order", requestHandler)
                .Map("~/cs/{destination}/{product}/photo", requestHandler);

            Engine.Environment.WithRouteTable()
                .Map("~/cs/about/company", requestHandler)
                .Map("~/cs/about/people", requestHandler);

            //requestHandler = ((RouteTable)Engine.Environment.WithRouteTable())
            //    .GetrequestHandler("~/cs/about/people");

            PrintSegment(((RouteRequestHandler)Engine.Environment.WithRouteTable()).PathTree, 0);
        }

        private static void PrintSegment(RouteSegment routeSegment, int indent)
        {
            PrintLine(routeSegment.ToString(), indent);

            foreach (RouteSegment childRouteSegment in routeSegment.EnumerateChildren())
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
