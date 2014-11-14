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
            HttpContext httpContext = ((HttpApplication)sender).Context;
            IHttpContext context = new AspNetHttpContext(httpContext);

            IRequestHandler requestHandler = Engine.Environment.WithRootRequestHandler();
            if (requestHandler.TryHandleAsync(context).Result)
                httpContext.Response.End();
        }

        public void Dispose()
        { }
    }
}
