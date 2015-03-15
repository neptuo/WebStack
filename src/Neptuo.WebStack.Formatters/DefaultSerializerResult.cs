using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Formatters
{
    /// <summary>
    /// Default implementation of <see cref="ISerializerResult"/>
    /// </summary>
    public class DefaultSerializerResult : ISerializerResult
    {
        public bool IsSuccessful { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="isSuccessful">Whether serialization was successful.</param>
        public DefaultSerializerResult(bool isSuccessful)
        {
            IsSuccessful = isSuccessful;
        }
    }
}
