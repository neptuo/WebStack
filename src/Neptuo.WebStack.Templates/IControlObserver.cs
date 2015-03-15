using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.WebStack.Templates
{
    /// <summary>
    /// Interface that enables usage of type as observer.
    /// </summary>
    public interface IControlObserver
    {
        /// <summary>
        /// Observes target init phase.
        /// </summary>
        /// <param name="e">Context that describes observer usage.</param>
        void OnInit(ControlObserverEventArgs e);

        /// <summary>
        /// Observes target render phase.
        /// </summary>
        /// <param name="e">Context that describes observer usage.</param>
        /// <param name="writer">Output rendering writer.</param>
        void Render(ControlObserverEventArgs e, IHtmlWriter writer);
    }
}
