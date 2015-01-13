using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Resources
{
    public static class _ResourceContextExtensions
    {
        /// <summary>
        /// Adds <paramref name="resourceName"/> to currently used sources (to include).
        /// If such resource doesn't exist in <see cref="IResourceContext.Collection"/>, throws <see cref="ArgumentOutOfRangeException"/>.
        /// </summary>
        /// <param name="resourceName">Resource to lookup in collection and add.</param>
        /// <returns>Self (for fluency).</returns>
        /// <exception cref="ArgumentOutOfRangeException">When resource collection doesn't contain <paramref name="resourceName"/>.</exception>
        public static IResourceContext Use(IResourceContext context, string resourceName)
        {
            Guard.NotNull(context, "context");

            IResource resource;
            if (context.Collection.TryGet(resourceName, out resource))
                return context.Use(resource);

            throw Guard.Exception.ArgumentOutOfRange("resourceName", "Unnable to find resource with name '{0}'.", resourceName);
        }
    }
}
