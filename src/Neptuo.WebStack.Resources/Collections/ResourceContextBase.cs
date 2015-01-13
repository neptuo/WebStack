using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Resources.Collections
{
    /// <summary>
    /// Base implementation of <see cref="IResourceContext"/>.
    /// </summary>
    public class ResourceContextBase : IResourceContext
    {
        public IResourceCollection Collection { get; private set; }

        /// <summary>
        /// Collection of included resources.
        /// </summary>
        protected HashSet<IResource> Included { get; private set; }

        /// <summary>
        /// Creates new instance from <paramref name="collection"/>.
        /// </summary>
        /// <param name="collection">Collection of all registered resources.</param>
        public ResourceContextBase(IResourceCollection collection)
            : this(collection, new HashSet<IResource>())
        { }

        /// <summary>
        /// Creates new instance from <paramref name="collection"/> with initial use of <paramref name="included"/> resources.
        /// </summary>
        /// <param name="collection">Collection of all registered resources.</param>
        /// <param name="included">Initial collection of used resources.</param>
        public ResourceContextBase(IResourceCollection collection, IEnumerable<IResource> included)
        {
            Guard.NotNull(collection, "collection");
            Guard.NotNull(included, "included");
            Collection = collection;
            Included = new HashSet<IResource>(included);
        }

        public IResourceContext Use(IResource resource)
        {
            Guard.NotNull(resource, "resource");
            Included.Add(resource);
            return this;
        }

        public IEnumerable<IResource> EnumerateResources()
        {
            return Included;
        }
    }
}
