using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Neptuo.WebStack.Templates
{
    /// <summary>
    /// Default implementation of <see cref="IValueExtensionContext"/>.
    /// </summary>
    public class DefaultExtensionContext : IValueExtensionContext
    {
        public object TargetObject { get; set; }
        public PropertyInfo TargetProperty { get; set; }
        public IDependencyProvider DependencyProvider { get; set; }

        public DefaultExtensionContext(object targetObject, PropertyInfo targetProperty, IDependencyProvider dependencyProvider)
        {
            Guard.NotNull(targetObject, "targetObject");
            Guard.NotNull(targetProperty, "targetProperty");
            Guard.NotNull(dependencyProvider, "dependencyProvider");
            TargetObject = targetObject;
            TargetProperty = targetProperty;
            DependencyProvider = dependencyProvider;
        }
    }
}
