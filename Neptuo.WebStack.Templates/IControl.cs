using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Templates
{
    public interface IControl
    {
        /// <summary>
        /// Method invoked in init phase.
        /// Place any inicialization code here.
        /// </summary>
        void OnInit(IComponentManager componentManager);

        /// <summary>
        /// Renders output to <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">Output writer.</param>
        void Render(IHtmlWriter writer);
    }
}
