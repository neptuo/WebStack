using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Exceptions
{
    public class ExceptionModel
    {
        public bool IsHandled { get; internal set; }
        public Exception Exception { get; private set; }

        public ExceptionModel PreviousModel { get; internal set; }
        public ExceptionModel NextModel { get; internal set; }

        public ExceptionModel(Exception exception)
            : this(false, exception)
        { }

        internal ExceptionModel(bool isHandled, Exception exception)
        {
            Ensure.NotNull(exception, "exception");
            IsHandled = isHandled;
            Exception = exception;
        }
    }
}
