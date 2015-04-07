using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.WebStack.Templates.UI
{
    /// <summary>
    /// Output writer.
    /// </summary>
    public interface IHtmlWriter
    {
        /// <summary>
        /// Writes content to the ouput.
        /// </summary>
        /// <param name="content">Content.</param>
        /// <returns>Self.</returns>
        IHtmlWriter Content(object content);

        /// <summary>
        /// Writes content to the ouput.
        /// </summary>
        /// <param name="content">Content.</param>
        /// <returns>Self.</returns>
        IHtmlWriter Content(string content);

        /// <summary>
        /// Writes opening tag.
        /// </summary>
        /// <param name="name">Tag name.</param>
        /// <returns>Self.</returns>
        IHtmlWriter Tag(string name);

        /// <summary>
        /// Writes close tag for the currently opened one.
        /// If body of this tag is empty, writes self closed tag.
        /// </summary>
        /// <returns>Self.</returns>
        IHtmlWriter CloseTag();

        /// <summary>
        /// Disable self close tag event if the current tag has empty body (eg. &lt;div&gt;&lt;/div&gt;).
        /// </summary>
        /// <returns>Self.</returns>
        IHtmlWriter CloseFullTag();

        /// <summary>
        /// Writes attribute to the currently opened tag.
        /// </summary>
        /// <param name="name">Attribute name.</param>
        /// <param name="value">Attribute value.</param>
        /// <exception cref="HtmlTextWriterException">If current state doesn't allow attribut (eg. no opened tag or opened tag has already content).</exception>
        /// <returns>Self.</returns>
        IHtmlWriter Attribute(string name, string value);
    }
}
