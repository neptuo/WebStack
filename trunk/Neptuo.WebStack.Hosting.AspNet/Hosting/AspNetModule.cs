using Neptuo.Collections.Specialized;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Neptuo.WebStack.Hosting
{
    public class AspNetModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            EventHandlerTaskAsyncHelper asyncHelper = new EventHandlerTaskAsyncHelper(OnBeginRequest);
            context.AddOnBeginRequestAsync(asyncHelper.BeginEventHandler, asyncHelper.EndEventHandler);
            //context.BeginRequest += OnBeginRequest;
        }

        private async Task OnBeginRequest(object sender, EventArgs e)
        {
            bool handlerExecuted = false;
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;

            DefaultHttpRequest httpRequest = CreateHttpRequest(context.Request, context.Response);
            IRequestHandler requestHandler = Engine.Environment.WithRootRequestHandler();
            IHttpResponse httpResponse = await requestHandler.TryHandleAsync(httpRequest);
            httpRequest.RaiseOnDisposing();

            if (httpResponse != null)
            {
                ProcessResponse(context.Response, httpResponse);
                httpResponse.Dispose();
                handlerExecuted = true;
            }

            if (handlerExecuted)
                application.CompleteRequest();
        }

        private DefaultHttpRequest CreateHttpRequest(HttpRequest request, HttpResponse response)
        {
            IDependencyProvider dependencyProvider = Engine.RootContainer.CreateChildContainer();
            HttpMethod method = HttpMethod.KnownMethods[request.HttpMethod];
            IReadOnlyUrl url = dependencyProvider.Resolve<IUrlBuilder>().FromUrl(request.Url.AbsoluteUri);
            IReadOnlyKeyValueCollection headers = new KeyValueCollection(request.Headers);
            IHttpParamCollection queryString = new AspNetParamCollection(request.QueryString);
            Stream inputStream = request.InputStream;
            IHttpParamCollection form = new AspNetParamCollection(request.Form);
            IEnumerable<IHttpFile> files = GetPostedFiles(request.Files);
            CancellationToken cancellationToken = response.ClientDisconnectedToken;
            IKeyValueCollection customValues = new KeyValueCollection();

            return new DefaultHttpRequest(
                dependencyProvider,
                method,
                url,
                headers,
                queryString,
                inputStream,
                form,
                files,
                cancellationToken,
                customValues
            );
        }

        private IEnumerable<IHttpFile> GetPostedFiles(HttpFileCollection files)
        {
            List<IHttpFile> result = new List<IHttpFile>();
            foreach (HttpPostedFile file in files)
                result.Add(new AspNetFile(file));

            return result;
        }

        private void ProcessResponse(HttpResponse response, IHttpResponse httpResponse)
        {
            // Status.
            response.StatusCode = httpResponse.Status().Code;

            // Headers.
            //response.Headers.Clear();
            response.Headers.Remove("Server");
            foreach (string headerKey in httpResponse.Headers().Keys)
            {
                string headerValue;
                if(httpResponse.Headers().TryGet(headerKey, out headerValue))
                    response.Headers.Add(headerKey, headerValue);
            }

            // Content.
            //Console.WriteLine(new StreamReader(httpResponse.OutputStream()).ReadToEnd());
            //httpResponse.OutputStream().CopyStream(response.OutputStream);
            if (httpResponse.OutputStream().CanSeek)
                httpResponse.OutputStream().Seek(0, SeekOrigin.Begin);

            httpResponse.OutputStream().CopyTo(response.OutputStream);
        }

        public void Dispose()
        { }
    }

    public static class StreamExtensions
    {
        public static void CopyStream(this Stream input, Stream output)
        {
            byte[] buffer = new byte[32768];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }
    }
}
