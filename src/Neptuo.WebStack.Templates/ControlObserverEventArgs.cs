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
        public IControl Target { get; set; }

        /// <summary>
        /// Flag to indicate whether <see cref="Target"/> can be processed.
        /// </summary>
        public bool Cancel { get; set; }

        public ControlObserverEventArgs(IControl target)
        {
            Guard.NotNull(target, "target");
            Target = target;
        }
    }
}
