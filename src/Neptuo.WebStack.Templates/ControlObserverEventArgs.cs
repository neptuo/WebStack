using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Templates
{
    /// <summary>
    /// EventArgs used in <see cref="IControlObserver"/>.
    /// </summary>
    public class ControlObserverEventArgs : EventArgs
    {
        /// <summary>
        /// Observer target control.
        /// </summary>
        public IControl Target { get; private set; }

        /// <summary>
        /// Current component manager.
        /// </summary>
        public IComponentManager ComponentManager { get; private set; }

        /// <summary>
        /// Flag to indicate whether <see cref="Target"/> can be processed.
        /// </summary>
        public bool Cancel { get; set; }

        /// <summary>
        /// Creates new instance with parameters <paramref name="target"/> and <paramref name="componentManager"/>.
        /// </summary>
        /// <param name="target">Observer target control.</param>
        /// <param name="componentManager">Current component manager.</param>
        public ControlObserverEventArgs(IControl target, IComponentManager componentManager)
        {
            Guard.NotNull(target, "target");
            Guard.NotNull(componentManager, "componentManager");
            Target = target;
            ComponentManager = componentManager;
        }
    }
}
