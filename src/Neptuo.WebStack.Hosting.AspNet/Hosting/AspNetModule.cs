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
using HttpWebContext = System.Web.HttpContext;

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
            HttpApplication application = (HttpApplication)sender;
            HttpContext webContext = application.Context;

            IRequestHandler requestHandler = Engine.Environment.WithRootRequestHandler();
            AspNetContext httpContext = new AspNetContext(webContext);

            bool isHandled = await requestHandler.TryHandleAsync(httpContext);
            if (isHandled)
            {
                httpContext.FlushOutput();
                application.CompleteRequest();
            }
            else
            {
                //TODO: Throw or let run underlaying ASP.NET.
            }
        }

        public void Dispose()
        { }
    }

    //public static class StreamExtensions
    //{
    //    public static void CopyStream(this Stream input, Stream output)
    //    {
    //        byte[] buffer = new byte[32768];
    //        int read;
    //        while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
    //        {
    //            output.Write(buffer, 0, read);
    //        }
    //    }
    //}
}
