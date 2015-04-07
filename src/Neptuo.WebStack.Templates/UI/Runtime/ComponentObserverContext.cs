using Neptuo.WebStack.Templates.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Templates.UI.Runtime
{
    /// <summary>
    /// EventArgs used in <see cref="IControlObserver"/>.
    /// </summary>
    internal class ComponentObserverContext : EventArgs, IControlObserverContext
    {
        public IControl Target { get; private set; }
        public IComponentManager ComponentManager { get; private set; }
        public bool Cancel { get; set; }

        public ComponentObserverContext(IControl target, IComponentManager componentManager)
        {
            Ensure.NotNull(target, "target");
            Ensure.NotNull(componentManager, "componentManager");
            Target = target;
            ComponentManager = componentManager;
        }
    }
}
