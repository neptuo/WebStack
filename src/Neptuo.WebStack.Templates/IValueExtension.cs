using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.WebStack.Templates
{
    /// <summary>
    /// Interface that enables usage of type as markup extension.
    /// </summary>
    public interface IValueExtension
    {
        /// <summary>
        /// Called to provide value of markup extension.
        /// </summary>
        /// <param name="context">Context of extensio.</param>
        /// <returns>Value of markup extension.</returns>
        object ProvideValue(IValueExtensionContext context);
    }
}
