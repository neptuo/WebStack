using Neptuo.WebStack.Hosting.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.TestConsole.Routing
{
    class TestRoutePattern
    {
        public static void Test()
        {
            RoutePattern pattern = new RoutePattern("http://www.neptuo.com/test");
            Console.WriteLine(pattern);

            pattern = new RoutePattern("//www.neptuo.com/test");
            Console.WriteLine(pattern);

            pattern = new RoutePattern("~/test");
            Console.WriteLine(pattern);
        }
    }
}
