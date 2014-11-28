using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Serialization
{
    /// <summary>
    /// Contract for serializing objects.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serializes <paramref name="instance"/> to the <paramref name="stream"/>.
        /// </summary>
        /// <param name="stream">Output stream.</param>
        /// <param name="instance">Object to serialize.</param>
        Task SerializeAsync(Stream stream, object instance);
    }
}
