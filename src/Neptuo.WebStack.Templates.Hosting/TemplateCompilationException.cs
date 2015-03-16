using Neptuo.ComponentModel;
using Neptuo.Templates.Compilation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Templates.Hosting
{
    /// <summary>
    /// Exception raised when error in compiling view.
    /// </summary>
    public class TemplateCompilationException : Exception
    {
        /// <summary>
        /// Enumeration of compilation errors.
        /// </summary>
        public IEnumerable<IErrorInfo> Errors { get; private set; }

        /// <summary>
        /// Template content.
        /// </summary>
        public ISourceContent SourceContent { get; private set; }

        public TemplateCompilationException(IEnumerable<IErrorInfo> errors, ISourceContent sourceContent)
        {
            Ensure.NotNull(errors, "errors");
            Ensure.NotNull(sourceContent, "sourceContent");
            Errors = errors;
            SourceContent = sourceContent;
        }
    }
}
