using Neptuo;
using Neptuo.WebStack;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.SessionState;

namespace AspOneDemo
{
    public class Global : HttpApplication, IRequestHandler
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Engine.Environment.UseRootRequestHandler(this);
        }

        public Task<bool> TryHandleAsync(IHttpContext httpContext)
        {
            httpContext.Response().Headers().ContentType(new HttpMediaType("text/plain"));

            CompilationSection compilation = (CompilationSection)ConfigurationManager.GetSection("system.web/compilation");
            string compilationDirectory = compilation.TempDirectory;
            httpContext.Response().OutputWriter().Write("CompilationDirectory: ");
            httpContext.Response().OutputWriter().WriteLine(compilationDirectory);
            httpContext.Response().OutputWriter().Write("Exist: ");
            httpContext.Response().OutputWriter().WriteLine(Directory.Exists(compilationDirectory));

            string currentDirectory = HttpContext.Current.Server.MapPath("~/");
            httpContext.Response().OutputWriter().Write("CurrentDirectory: ");
            httpContext.Response().OutputWriter().WriteLine(currentDirectory);
            httpContext.Response().OutputWriter().Write("Exist: ");
            httpContext.Response().OutputWriter().WriteLine(Directory.Exists(currentDirectory));

            return Task.FromResult(true);
        }
    }
}