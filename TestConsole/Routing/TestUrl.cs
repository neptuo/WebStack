using Neptuo.WebStack.Hosting.Routing;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.TestConsole.Routing
{
    class TestUrl
    {
        public static void Test()
        {
            Url pattern = new Url("http://www.neptuo.com/test");
            Console.WriteLine(pattern);

            pattern = new Url("x//www.neptuo.com/test");
            Console.WriteLine(pattern);

            pattern = new Url("~/test");
            Console.WriteLine(pattern);
        }
    }
}
