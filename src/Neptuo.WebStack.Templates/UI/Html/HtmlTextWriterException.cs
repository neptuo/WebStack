using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Templates.UI.Html
{
    /// <summary>
    /// Exception used in <see cref="HtmlTextWriter"/>.
    /// </summary>
    public class HtmlTextWriterException : Exception
    {
        public HtmlTextWriterException(string message)
            : base(message)
        { }
    }
}
