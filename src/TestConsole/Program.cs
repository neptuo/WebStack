using Neptuo.TestConsole.Routing;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TestUrl.Test();
            //TestRouting.Test();

            Console.ReadKey(true);
        }

        static void Test()
        {
            IHttpContext httpContext = null;

            httpContext.Request().IsMethodGet();
            httpContext.Request().Files();

            httpContext.Response().Headers().Set(null, null);
            httpContext.Response().OutputWriter().Write("Hello!");


            IUrlBuilder builder = null;
            builder.Path("/test").ToUrl();
            builder.Schema("http").Host("www.google.com").Parameter("q", "John").ToUrl();
        }
    }
}
