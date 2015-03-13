using Microsoft.Practices.Unity;
using Neptuo;
using Neptuo.Activators;
using Neptuo.FileSystems;
using Neptuo.ComponentModel.Behaviors.Providers;
using Neptuo.WebStack;
using Neptuo.WebStack.Exceptions;
using Neptuo.WebStack.Http;
using Neptuo.WebStack.Http.Converters;
using Neptuo.WebStack.Routing;
using Neptuo.WebStack.Serialization;
using Neptuo.WebStack.Serialization.Xml;
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
using TestWebApp.Services;
using Neptuo.WebStack.Routing.Hosting;

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
            string binDirectory = @"D:\Development\Neptuo\WebStack\src\TestWebApp\bin";
            string tempDirectory = @"C:\Temp\Services";
            string wwwRootDirectory = @"E:\Pictures\Camera Roll";

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

            Engine.Environment.UseSerialization((serializers, deserializers) =>
            {
                serializers
                    .Map(HttpMediaType.Xml, new XmlSerializer())
                    .Map(HttpMediaType.Html, new XmlSerializer());

                deserializers
                    .Map(HttpMediaType.Xml, new XmlSerializer());
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