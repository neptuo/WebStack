﻿using Microsoft.Practices.Unity;
using Neptuo;
using Neptuo.FileSystems;
using Neptuo.Lifetimes.Mapping;
using Neptuo.Unity;
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
using Neptuo.WebStack.Services.Hosting.Behaviors.Providers;
using Neptuo.WebStack.Services.Hosting.Pipelines.Compilation;
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

namespace TestWebApp
{
    public class Global : HttpApplication, IRequestHandler
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Converts.Repository
                .Add(typeof(int), typeof(HttpStatus), new HttpStatusConverter())
                .Add(typeof(string), typeof(HttpMethod), new HttpMethodConverter())
                .Add(typeof(string), typeof(HttpMediaType), new HttpMediaTypeConverter())
                .Add(typeof(string), typeof(IEnumerable<HttpMediaType>), new HttpMediaTypeConverter());

            IUnityContainer container = new UnityContainer()
                .RegisterType<IUrlBuilder, UrlBuilder>(new GetterLifetimeManager(() => new UrlBuilder(HttpContext.Current.Request.ApplicationPath)));

            Engine.Environment.Use<IDependencyContainer>(new UnityDependencyContainer(container, new LifetimeMapping<LifetimeManager>()));
            Engine.Environment.UseParameterCollection(c => c.Add("FileName", new FileNameParameter(LocalFileSystem.FromDirectoryPath(@"E:\Pictures\Camera Roll"))));
            Engine.Environment.UseBehaviors(provider =>
                provider
                    .AddMapping<IWithRedirect, WithRedirectBehavior>()
                    .AddMapping<IWithStatus, WithStatusBehavior>()
                    .AddMapping(typeof(IForInput<>), typeof(ForInputBehavior<>))
                    .AddMapping(typeof(IWithOutput<>), typeof(WithOutputBehavior<>))
            );
            Engine.Environment.UseCodeDomConfiguration(@"C:\Temp\Services", @"D:\Projects\Neptuo.WebStack\TestWebApp\bin");
            Engine.Environment.UseSerialization((serializers, deserializers) =>
            {
                serializers
                    .Map(HttpMediaType.Xml, new XmlSerializer())
                    .Map(HttpMediaType.Html, new XmlSerializer());

                deserializers
                    .Map(HttpMediaType.Xml, new XmlSerializer());
            });

            RouteRequestHandler routeTable = new RouteRequestHandler(Engine.Environment.WithParameterCollection());
            routeTable
                .MapService(typeof(HelloHandler))
                .MapService(typeof(PersonJohnDoeHandler));


            IUrlBuilder builder = routeTable.UrlBuilder();
            routeTable.Map(
                builder.VirtualPath("~/photos/{FileName}"), 
                new FileSystemRequestHandler(
                    LocalFileSystem.FromDirectoryPath(@"E:\Pictures\Camera Roll"),
                    new UrlPathProvider()
                )
            );

            Engine.Environment.UseRootRequestHandler(
                new ExceptionRequestHandler(
                    new DelegatingRequestHandler(
                        //new FileSystemRequestHandler(
                        //    LocalFileSystem.FromDirectoryPath(@"E:\Pictures"),
                        //    new UrlPathProvider()
                        //),
                        routeTable,
                        this
                    )
                )
            );
        }

        public async Task<IHttpResponse> TryHandleAsync(IHttpRequest httpRequest)
        {
            IHttpResponse httpResponse = new DefaultHttpResponse();
            await httpResponse.OutputWriter().WriteLineAsync("Hello, World!");
            return httpResponse;
        }
    }

    public class FileNameParameter : IRouteParameter
    {
        private readonly IReadOnlyDirectory rootDirectory;

        public FileNameParameter(IReadOnlyDirectory rootDirectory)
        {
            Guard.NotNull(rootDirectory, "rootDirectory");
            this.rootDirectory = rootDirectory;
        }

        public bool MatchUrl(IRouteParameterMatchContext context)
        {
            string remainingUrl = context.RemainingUrl;
            if(rootDirectory.FindFiles(remainingUrl, true).Any())
            {
                context.HttpRequest.CustomValues().Set("FileSystemRequestHandler:FileName", remainingUrl);
                context.RemainingUrl = null;
                return true;
            }

            return false;
        }
    }
}