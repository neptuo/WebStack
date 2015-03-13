using Neptuo.Diagnostics;
using Neptuo.TestConsole.Routing.Services;
using Neptuo.WebStack.Http;
using Neptuo.WebStack.Routing;
using Neptuo.WebStack.Routing.Segments;
using Neptuo.WebStack.Services.Behaviors;
using Neptuo.WebStack.Services.Hosting;
using Neptuo.WebStack.Services.Hosting.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Neptuo.WebStack;
using Neptuo.WebStack.Services.Hosting.Processing;

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
                });
                //.UseRouteTable(routeTable =>
                //{
                //    routeTable
                //        .MapServices(Assembly.GetExecutingAssembly());
                //});

            IRequestHandler requestHandler = new CodeDomServiceHandlerFactory(typeof(GetHelloHandler));

            //PathRouteSegment rootSegment = new PathRouteSegment();
            //DebugIteration("Build route table", 1, () =>
            //{
            //    rootSegment.Include(new StaticRouteSegment("~/cs/contacts/send"));
            //    rootSegment.Include(new StaticRouteSegment("~/cs/home"));
            //    rootSegment.Include(new StaticRouteSegment("~/cs/hosting"));
            //    rootSegment.Include(new StaticRouteSegment("~/cs/contacts"));
            //    rootSegment.Include(new StaticRouteSegment("~/cs"));
            //});

            RouteRequestHandler routeTable = new RouteRequestHandler(Engine.Environment.WithParameterCollection());
            IUrlBuilder builder = routeTable.UrlBuilder();
            routeTable
                .Map(builder.VirtualPath("~/cs/home").ToUrl(), requestHandler)
                .Map(builder.VirtualPath("~/cs/about").ToUrl(), requestHandler)
                .Map(builder.VirtualPath("~/cs/{destination}").ToUrl(), requestHandler)
                .Map(builder.VirtualPath("~/cs/{destination}/products").ToUrl(), requestHandler)
                .Map(builder.VirtualPath("~/cs/{destination}/photo").ToUrl(), requestHandler)
                .Map(builder.VirtualPath("~/cs/{destination}/{product}").ToUrl(), requestHandler)
                .Map(builder.VirtualPath("~/cs/{destination}/{product}/order").ToUrl(), requestHandler)
                .Map(builder.VirtualPath("~/cs/{destination}/{product}/photo").ToUrl(), requestHandler);

            routeTable
                .Map(builder.VirtualPath("~/cs/about/company").ToUrl(), requestHandler)
                .Map(builder.VirtualPath("~/cs/about/people").ToUrl(), requestHandler);

            Engine.Environment.UseRootRequestHandler(routeTable);

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
