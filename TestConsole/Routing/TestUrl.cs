using Neptuo.WebStack.Routing;
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
            IUrlBuilder urlBuilder = new UrlBuilder("/test");

            IReadOnlyUrl url = urlBuilder.FromUrl("http://www.neptuo.com/test/abc");
            Console.WriteLine(url);

            url = urlBuilder.FromUrl("http://www.neptuo.com/");
            Console.WriteLine(url);

            url = urlBuilder.FromUrl("//www.neptuo.com/test");
            Console.WriteLine(url);

            url = urlBuilder.FromUrl("~/abc");
            Console.WriteLine(url);
        }
    }
}
