using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Templates
{
    /// <summary>
    /// Context of view rendering.
    /// </summary>
    public interface IRenderContext
    {
        /// <summary>
        /// Output HTML writer.
        /// </summary>
        IHtmlWriter Writer { get; }


    }
}
