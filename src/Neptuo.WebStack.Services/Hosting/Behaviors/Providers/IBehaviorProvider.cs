using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Services.Hosting.Behaviors.Providers
{
    public interface IBehaviorProvider
    {
        IEnumerable<Type> GetBehaviors(Type handlerType);
    }
}
