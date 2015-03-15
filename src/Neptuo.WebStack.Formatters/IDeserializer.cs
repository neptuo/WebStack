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
    /// Contract for deserializing objects.
    /// </summary>
    public interface IDeserializer
    {
        /// <summary>
        /// Tries to deserialize object from <paramref name="context"/>.
        /// </summary>
        /// <param name="context">Deserialization context.</param>
        /// <returns>Deserialization result.</returns>
        Task<IDeserializerResult> TryDeserializeAsync(IDeserializerContext context);
    }
}
