using Neptuo;
using Neptuo.FileSystems;
using Neptuo.WebStack;
using Neptuo.WebStack.Http;
using Neptuo.WebStack.Routing;
using Neptuo.WebStack.Services.Behaviors;
using Neptuo.WebStack.Services.Hosting;
using Neptuo.WebStack.Services.Hosting.Behaviors;
using Neptuo.WebStack.Services.Hosting.Behaviors.Providers;
using Neptuo.WebStack.Services.Hosting.Pipelines.Compilation;
using Neptuo.WebStack.StaticFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using TestWebApp.Services;

namespace TestWebApp
{
    public class Global : HttpApplication, IRequestHandler
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Engine.Environment.UseBehaviors(new InterfaceBehaviorProvider().AddMapping(typeof(IForInput<>), typeof(ForInputBehavior<>)).AddMapping(typeof(IWithOutput<>), typeof(WithOutputBehavior<>)));
            Engine.Environment.UseCodeDomConfiguration(new CodeDomPipelineConfiguration(@"C:\Temp", @"D:\Projects\Neptuo.WebStack\TestWebApp\bin"));

            RouteRequestHandler routeTable = new RouteRequestHandler(new RouteParameterCollection());
            routeTable.MapService(typeof(HelloHandler));

            Engine.Environment.UseRootRequestHandler(new DelegatingRequestHandler(
                new FileSystemRequestHandler(
                    LocalFileSystem.FromDirectoryPath(@"E:\Pictures"),
                    new UrlPathProvider()
                ),
                routeTable,
                this
            ));
        }

        public async Task<bool> TryHandleAsync(IHttpContext httpContext)
        {
            await httpContext.Response().OutputWriter().WriteLineAsync("Hello, World!");
            return true;
        }
    }
}