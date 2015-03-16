using Neptuo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Templates
{
    /// <summary>
    /// Implementation of <see cref="IHtmlWriter"/> for writing html.
    /// </summary>
    public class HtmlTextWriter : IHtmlWriter
    {
        /// <summary>
        /// Html special characters.
        /// </summary>
        public static class Html
        {
            public const char StartTag = '<';
            public const char CloseTag = '>';
            public const char Slash = '/';
            public const char Space = ' ';

            public const char Equal = '=';
            public const char DoubleQuote = '"';
            public const char Quote = '\'';
        }

        /// <summary>
        /// Inner text writer.
        /// </summary>
        protected TextWriter InnerWriter { get; private set; }

        /// <summary>
        /// List of opened tags.
        /// </summary>
        protected Stack<string> OpenTags { get; private set; }

        /// <summary>
        /// Flag to see if there is currently opened tag.
        /// </summary>
        protected bool IsOpenTag { get; set; }

        /// <summary>
        /// Flag to see if current tag has content.
        /// </summary>
        protected bool HasContent { get; set; }

        /// <summary>
        /// Flag to see if attribute can be renderend at current place.
        /// </summary>
        protected bool CanWriteAttribute { get; set; }

        public HtmlTextWriter(TextWriter innerWriter)
        {
            Ensure.NotNull(innerWriter, "innerWriter");
            InnerWriter = innerWriter;
            OpenTags = new Stack<string>();
        }

        /// <summary>
        /// Writes content.
        /// </summary>
        /// <param name="content">Content</param>
        /// <returns>This.</returns>
        public virtual IHtmlWriter Content(object content)
        {
            EnsureCloseOpeningTag();
            InnerWriter.Write(content);
            return this;
        }

        /// <summary>
        /// Writes text content.
        /// </summary>
        /// <param name="content">Text content</param>
        /// <returns>This.</returns>
        public virtual IHtmlWriter Content(string content)
        {
            EnsureCloseOpeningTag();
            InnerWriter.Write(content);
            return this;
        }

        /// <summary>
        /// Writes start tag.
        /// </summary>
        /// <param name="name">Tag name.</param>
        /// <returns>This.</returns>
        public virtual IHtmlWriter Tag(string name)
        {
            Ensure.NotNullOrEmpty(name, "name");
            EnsureCloseOpeningTag();

            CanWriteAttribute = true;
            HasContent = false;
            IsOpenTag = true;

            OpenTags.Push(name);
            InnerWriter.Write(Html.StartTag);
            InnerWriter.Write(name);
            return this;
        }

        /// <summary>
        /// Writes close tag (can be self closed, if there is no content in current tag).
        /// </summary>
        /// <returns>This.</returns>
        public virtual IHtmlWriter CloseTag()
        {
            WriteCloseTag(HasContent);
            return this;
        }

        /// <summary>
        /// Writes close tag (forces full close tag).
        /// </summary>
        /// <returns>This.</returns>
        public virtual IHtmlWriter CloseFullTag()
        {
            WriteCloseTag(true);
            return this;
        }

        /// <summary>
        /// Writers attribute <paramref name="name"/> with value <paramref name="value"/>.
        /// </summary>
        /// <param name="name">Attribute name.</param>
        /// <param name="value">Attribute value.</param>
        /// <returns>This.</returns>
        public virtual IHtmlWriter Attribute(string name, string value)
        {
            Ensure.NotNullOrEmpty(name, "name");

            if (!CanWriteAttribute)
                throw new HtmlTextWriterException("Unnable to write attribute in current state!");

            InnerWriter.Write(Html.Space);
            InnerWriter.Write(name);
            InnerWriter.Write(Html.Equal);
            InnerWriter.Write(Html.DoubleQuote);
            InnerWriter.Write(value);
            InnerWriter.Write(Html.DoubleQuote);

            return this;
        }

        /// <summary>
        /// Do write close tag.
        /// </summary>
        /// <param name="hasContent">Whether use self closing or full tag.</param>
        protected void WriteCloseTag(bool hasContent)
        {
            if (OpenTags.Count == 0)
                throw new HtmlTextWriterException("Unnable to close tag! All tags has been closed.");

            string name = OpenTags.Pop();
            if (hasContent)
            {
                EnsureCloseOpeningTag();
                InnerWriter.Write(Html.StartTag);
                InnerWriter.Write(Html.Slash);
                InnerWriter.Write(name);
                InnerWriter.Write(Html.CloseTag);
            }
            else
            {
                IsOpenTag = false;
                InnerWriter.Write(Html.Space);
                InnerWriter.Write(Html.Slash);
                InnerWriter.Write(Html.CloseTag);
            }

            HasContent = true;
        }

        /// <summary>
        /// Ensures writing close tag if needed.
        /// </summary>
        protected virtual void EnsureCloseOpeningTag()
        {
            if (IsOpenTag)
                InnerWriter.Write(Html.CloseTag);

            IsOpenTag = false;
            CanWriteAttribute = false;
            HasContent = true;
        }
    }
}
