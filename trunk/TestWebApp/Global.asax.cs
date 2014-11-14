using Neptuo;
using Neptuo.WebStack;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace TestWebApp
{
    public class Global : HttpApplication, IRequestHandler
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Engine.Environment.UseRootRequestHandler(this);
        }

        public async Task<bool> TryHandleAsync(IHttpContext httpContext)
        {
            await httpContext.Response().OutputWriter().WriteLineAsync("Hello, World!");
            httpContext.Response().OutputWriter().Flush();
            return true;
        }
    }
}