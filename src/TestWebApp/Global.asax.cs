using Microsoft.Practices.Unity;
using Neptuo;
using Neptuo.Activators;
using Neptuo.Compilers;
using Neptuo.ComponentModel.Behaviors.Providers;
using Neptuo.FileSystems;
using Neptuo.WebStack;
using Neptuo.WebStack.Diagnostics;
using Neptuo.WebStack.Diagnostics.Hosting;
using Neptuo.WebStack.Formatters;
using Neptuo.WebStack.Http;
using Neptuo.WebStack.Http.Converters;
using Neptuo.WebStack.Routing;
using Neptuo.WebStack.Routing.Hosting;
using Neptuo.WebStack.Services.Behaviors;
using Neptuo.WebStack.Services.Hosting;
using Neptuo.WebStack.Services.Hosting.Behaviors;
using Neptuo.WebStack.Services.Hosting.Processing;
using Neptuo.WebStack.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using TestWebApp.Exceptions;
using TestWebApp.Services;

namespace TestWebApp
{
    public class UrlBuilderActivator : IActivator<IUrlBuilder>
    {
        public IUrlBuilder Create()
        {
            return new UrlBuilder(HttpContext.Current.Request.ApplicationPath);
        }
    }

    public class Global : HttpApplication, IRequestHandler
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //string binDirectory = @"C:\Development\Neptuo\WebStack\src\TestWebApp\bin";
            string binDirectory = @"C:\Users\marek.fisera\Projects\Neptuo\WebStack\src\TestWebApp\bin";
            string tempDirectory = @"C:\Temp\WebStack";
            //string wwwRootDirectory = @"E:\Pictures\Camera Roll";
            string wwwRootDirectory = @"C:\Users\marek.fisera\Pictures\Screenshots";

            Converts.Repository
                .Add(typeof(int), typeof(HttpStatus), new HttpStatusConverter())
                .Add(typeof(string), typeof(HttpMethod), new HttpMethodConverter())
                .Add(typeof(string), typeof(HttpMediaType), new HttpMediaTypeConverter())
                .Add(typeof(HttpMediaType), typeof(string), new HttpMediaTypeConverter())
                .Add(typeof(string), typeof(IEnumerable<HttpMediaType>), new HttpMediaTypeConverter());

            Engine.Environment.Use<IDependencyContainer>(
                new UnityDependencyContainer()
                    .Map<IUrlBuilder>().InTransient().ToActivator(new UrlBuilderActivator())
            );
            Engine.Environment.UseParameterCollection(c => c.Add("FileName", new FileNameParameter(LocalFileSystem.FromDirectoryPath(wwwRootDirectory))));

            Engine.Environment.UseWebServices()
                .UseBehaviors(provider => provider
                    .AddMapping<IWithRedirect, WithRedirectBehavior>()
                    .AddMapping<IWithStatus, WithStatusBehavior>()
                    .AddMapping(typeof(IForInput<>), typeof(ForInputBehavior<>))
                    .AddMapping(typeof(IWithOutput<>), typeof(WithOutputBehavior<>))
                )
                .UseCodeDomConfiguration(typeof(RequestPipelineBase<>), tempDirectory, binDirectory);

            Engine.Environment.WithWebServices().WithCodeDomConfiguration().IsDebugMode(true);

            Engine.Environment.UseFormatters((serializers, deserializers) =>
            {
                serializers
                    .Map(HttpMediaType.Xml, new XmlFormatter())
                    .Map(HttpMediaType.Html, new XmlFormatter())
                    .Map(HttpMediaType.Json, new JsonFormatter());

                deserializers
                    .Map(HttpMediaType.Xml, new XmlFormatter())
                    .Map(HttpMediaType.Json, new JsonFormatter());
            });

            Engine.Environment.UseTreeRouteTable(routeTable =>
                routeTable
                    .MapService(typeof(HelloHandler))
                    .MapService(typeof(PersonJohnDoeHandler))
                    .Map(
                        routeTable.UrlBuilder().VirtualPath("~/photos/{FileName}").ToUrl(),
                        new FileSystemRequestHandler(
                            LocalFileSystem.FromDirectoryPath(wwwRootDirectory),
                            new UrlPathProvider()
                        )
                    )
            );

            Engine.Environment.UseExceptionTable(
                new DefaultExceptionTable()
                    .MapException<NullReferenceException>(new NullReferenceExceptionHandler())
            );

            Engine.Environment.UseRootRequestHandler(
                new ExceptionRequestHandler(
                    new DelegatingRequestHandler(
                        //new FileSystemRequestHandler(
                        //    LocalFileSystem.FromDirectoryPath(@"E:\Pictures"),
                        //    new UrlPathProvider()
                        //),
                        new RouteRequestHandler(),
                        this
                    )
                )
            );
        }

        public async Task<bool> TryHandleAsync(IHttpContext httpContext)
        {
            throw new NullReferenceException("x");
            await httpContext.Response().OutputWriter().WriteLineAsync("Request handler was not found!");
            return true;
        }
    }

    public class FileNameParameter : IRouteParameter
    {
        private readonly IReadOnlyDirectory rootDirectory;

        public FileNameParameter(IReadOnlyDirectory rootDirectory)
        {
            Ensure.NotNull(rootDirectory, "rootDirectory");
            this.rootDirectory = rootDirectory;
        }

        public bool MatchUrl(IRouteParameterMatchContext context)
        {
            string remainingUrl = context.RemainingUrl;
            if(rootDirectory.FindFiles(remainingUrl, true).Any())
            {
                context.HttpContext.CustomValues().Set("FileSystemRequestHandler:FileName", remainingUrl);
                context.RemainingUrl = null;
                return true;
            }

            return false;
        }
    }
}