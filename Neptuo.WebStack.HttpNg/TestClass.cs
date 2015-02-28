using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    class TestClass
    {
        public static void Test()
        {
            IHttpContext httpContext = null;

            //httpContext.Request().IsMethodGet();
            httpContext.Request().Files();




            IUrlBuilder builder = null;
            builder.Path("/test").ToUrl();
            builder.Schema("http").Host("www.google.com").Parameter("q", "John").ToUrl();
        }
    }
}
