using Neptuo.WebStack.Resources;
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

        /// <summary>
        /// Context for selecting which resources to use from collection.
        /// </summary>
        IResourceContext ResourceContext { get; }
    }
}
