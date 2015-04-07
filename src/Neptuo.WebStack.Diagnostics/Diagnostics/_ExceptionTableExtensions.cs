using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Diagnostics
{
    /// <summary>
    /// Common extensions for <see cref="IExceptionTable"/>.
    /// </summary>
    public static class _ExceptionTableExtensions
    {
        /// <summary>
        /// Maps handling of <typeparamref name="T"/> to <paramref name="exceptionHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type of exception to handle.</typeparam>
        /// <param name="exceptionHandler">Handler for exception of type <typeparamref name="T"/>.</param>
        /// <returns>Self (for fluency).</returns>
        public static IExceptionTable MapException<T>(this IExceptionTable exceptionTable, IExceptionHandler exceptionHandler)
        {
            Ensure.NotNull(exceptionTable, "exceptionTable");
            return exceptionTable.MapException(typeof(T), exceptionHandler);
        }

        /// <summary>
        /// Maps handling of <typeparamref name="T"/> to <paramref name="requestHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type of exception to handle.</typeparam>
        /// <param name="requestHandler">Handler for exception of type <typeparamref name="T"/>.</param>
        /// <returns>Self (for fluency).</returns>
        public static IExceptionTable MapException<T>(this IExceptionTable exceptionTable, IRequestHandler requestHandler)
        {
            Ensure.NotNull(exceptionTable, "exceptionTable");
            return exceptionTable.MapException(typeof(T), requestHandler);
        }
    }
}
