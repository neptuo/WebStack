using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Neptuo.WebStack.Hosting
{
    public class AspNetHttpModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += OnBeginRequest;
        }

        private void OnBeginRequest(object sender, EventArgs e)
        {
            bool handlerExecuted = false;
            HttpApplication httpApplication = (HttpApplication)sender;
            HttpContext httpContext = httpApplication.Context;
            using (IHttpContext context = new AspNetHttpContext(httpContext))
            {
                IRequestHandler requestHandler = Engine.Environment.WithRootRequestHandler();
                if (requestHandler.TryHandleAsync(context).Result)
                    handlerExecuted = true;
            }

            if (handlerExecuted)
                httpApplication.CompleteRequest();
        }

        public void Dispose()
        { }
    }
}
