using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Diagnostics
{
    /// <summary>
    /// Mapper for handling specific exception types.
    /// </summary>
    public interface IExceptionTable
    {
        /// <summary>
        /// Adds handler executed when no exception handler was found for exception type.
        /// </summary>
        /// <param name="searchHandler">Func that should provide exception handler for exception type. </param>
        /// <returns>Self (for fluency).</returns>
        IExceptionTable AddSearchHandler(OutFunc<Type, IExceptionHandler, bool> searchHandler);

        /// <summary>
        /// Adds handler executed when no request handler was found for exception type.
        /// </summary>
        /// <param name="searchHandler">Func that should provide request handler for exception type. </param>
        /// <returns>Self (for fluency).</returns>
        IExceptionTable AddSearchHandler(OutFunc<Type, IRequestHandler, bool> searchHandler);

        /// <summary>
        /// Maps handling of <paramref name="exceptionType"/> to <paramref name="exceptionHandler"/>.
        /// </summary>
        /// <param name="exceptionType">Type of exception to handle.</param>
        /// <param name="exceptionHandler">Handler for exception of type <paramref name="exceptionType"/>.</param>
        /// <returns>Self (for fluency).</returns>
        IExceptionTable MapException(Type exceptionType, IExceptionHandler exceptionHandler);

        /// <summary>
        /// Maps handling of <paramref name="exceptionType"/> to <paramref name="requestHandler"/>.
        /// </summary>
        /// <param name="exceptionType">Type of exception to handle.</param>
        /// <param name="requestHandler">Handler for exception of type <paramref name="exceptionType"/>.</param>
        /// <returns>Self (for fluency).</returns>
        IExceptionTable MapException(Type exceptionType, IRequestHandler requestHandler);

        /// <summary>
        /// Tries to find exception handler for exception of type <paramref name="exceptionType"/>.
        /// </summary>
        /// <param name="exceptionType">Type of exception to find handler for.</param>
        /// <param name="exceptionHandler">Exception handler for exceptions of type <paramref name="exceptionType"/>.</param>
        /// <returns><c>true</c> if handler was found; <c>false</c> otherwise.</returns>
        bool TryGet(Type exceptionType, out IExceptionHandler exceptionHandler);
    }
}
