using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Formatters
{
    /// <summary>
    /// Describes result of <see cref="IDeserializer"/>.
    /// </summary>
    public interface ISerializerResult
    {
        /// <summary>
        /// Whether serialization was successful.
        /// </summary>
        bool IsSuccessful { get; }
    }
}
