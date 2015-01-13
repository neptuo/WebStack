using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Resources
{
    /// <summary>
    /// Context for selecting which resources to use from collection.
    /// </summary>
    public interface IResourceContext
    {
        /// <summary>
        /// Collection of all registered resources.
        /// </summary>
        IResourceCollection Collection { get; }

        /// <summary>
        /// Adds <paramref name="resource"/> to currently used sources (to include).
        /// </summary>
        /// <param name="resource">Resource to use.</param>
        /// <returns>Self (for fluency).</returns>
        IResourceContext Use(IResource resource);

        /// <summary>
        /// Returns enumeration of used (included) resources.
        /// </summary>
        /// <returns>Enumeration of used (included) resources.</returns>
        IEnumerable<IResource> EnumerateResources();
    }
}
