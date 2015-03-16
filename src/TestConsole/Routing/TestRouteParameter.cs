using Neptuo.WebStack.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.TestConsole.Routing
{
    public class TestRouteParameter : IRouteParameter
    {
        public bool TryMatchUrl(IRouteParameterMatchContext context)
        {
            throw new NotImplementedException();
        }
    }
}
