using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.WebStack.Templates
{
    /// <summary>
    /// Enables setting attributes.
    /// </summary>
    public interface IHtmlAttributeCollectionAware
    {
        /// <summary>
        /// Collection of custom HTML attibutes.
        /// </summary>
        HtmlAttributeCollection HtmlAttributes { get; }
    }
}
