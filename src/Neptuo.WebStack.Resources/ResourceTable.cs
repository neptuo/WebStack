﻿using Neptuo.FileSystems;
using Neptuo.WebStack.Resources.Collections;
using Neptuo.WebStack.Resources.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Resources
{
    /// <summary>
    /// Provides singleton instance of resource.
    /// </summary>
    public static class ResourceTable
    {
        private static object lockObject = new object();
        private static IResourceCollection resources;

        /// <summary>
        /// Singleton instance of resources.
        /// </summary>
        public static IResourceCollection Resources
        {
            get
            {
                if (resources == null)
                {
                    lock (lockObject)
                    {
                        resources = ResourceCollectionProvider.ProviderGetter();
                    }
                }
                return resources;
            }
        }
    }
}
