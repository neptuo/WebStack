﻿using Neptuo.WebStack.Hosting.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Services.Hosting.Routing
{
    /// <summary>
    /// Attribute for mapping handlers to routes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RouteAttribute : Attribute
    {
        public RoutePattern Url { get; private set; }

        /// <summary>
        /// Creates new instance mapped to <paramref name="url"/>.
        /// </summary>
        /// <param name="url">Target url.</param>
        public RouteAttribute(string url)
        {
            Url = url;
        }
    }
}
