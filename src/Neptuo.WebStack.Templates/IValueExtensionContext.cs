using Neptuo.Activators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Neptuo.WebStack.Templates
{
    /// <summary>
    /// Describes context in which <see cref="IValueExtension"/> is used.
    /// </summary>
    public interface IValueExtensionContext
    {
        /// <summary>
        /// Target controller, on which extension is declared.
        /// </summary>
        object TargetObject { get; }
        
        /// <summary>
        /// Target property, on which extensions is declared.
        /// </summary>
        PropertyInfo TargetProperty { get; }
    }
}