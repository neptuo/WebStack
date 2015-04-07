using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Templates.UI
{
    public interface IControlObserverContext
    {
        /// <summary>
        /// Observer target control.
        /// </summary>
        IControl Target { get; }

        /// <summary>
        /// Current component manager.
        /// </summary>
        IComponentManager ComponentManager { get; }

        /// <summary>
        /// Flag to indicate whether <see cref="Target"/> can be processed.
        /// </summary>
        bool Cancel { get; set; }
    }
}
