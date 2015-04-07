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
    public class DefaultValueExtensionContext : IValueExtensionContext
    {
        public object TargetObject { get; private set; }
        public PropertyInfo TargetProperty { get; private set; }

        public DefaultValueExtensionContext(object targetObject, PropertyInfo targetProperty)
        {
            Ensure.NotNull(targetObject, "targetObject");
            Ensure.NotNull(targetProperty, "targetProperty");
            TargetObject = targetObject;
            TargetProperty = targetProperty;
        }
    }
}
