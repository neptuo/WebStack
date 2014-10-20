using Neptuo.TestConsole.Routing;
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
    }
}
