using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Exceptions
{
    /// <summary>
    /// Mapper for handling specific exception types.
    /// </summary>
    public interface IExceptionTable
    {
        /// <summary>
        /// Maps handling of <paramref name="exceptionType"/> to <paramref name="exceptionHandler"/>.
        /// </summary>
        /// <param name="exceptionType">Type of exception to handle.</param>
        /// <param name="exceptionHandler">Handler for exception of type <paramref name="exceptionType"/>.</param>
        /// <returns>Self (for fluency).</returns>
        IExceptionTable MapException(Type exceptionType, IExceptionRequestHandler exceptionHandler);
    }
}
