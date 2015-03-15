using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Formatters
{
    /// <summary>
    /// Contract for serializing objects.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Tries to serialize <paramref name="instance"/> to the <paramref name="context"/>.
        /// </summary>
        /// <param name="context">Context for serialization.</param>
        /// <param name="instance">Instance to serialize.</param>
        /// <returns>Serialization result.</returns>
        Task<ISerializerResult> TrySerializeAsync(ISerializerContext context, object instance);
    }
}
