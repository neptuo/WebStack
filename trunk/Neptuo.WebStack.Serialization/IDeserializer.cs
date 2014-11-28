using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Serialization
{
    /// <summary>
    /// Contract for deserializing objects.
    /// </summary>
    public interface IDeserializer
    {
        /// <summary>
        /// Deserializes object of type <paramref name="type"/> from <paramref name="stream"/>.
        /// </summary>
        /// <param name="stream">Input stream to read data from.</param>
        /// <param name="type">Type of object to create.</param>
        /// <returns>Instance of type <paramref name="type"/> created from <paramref name="stream"/>.</returns>
        Task<object> DeserializeAsync(Stream stream, Type type);
    }
}
