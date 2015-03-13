using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Formatters.Collections
{
    /// <summary>
    /// Content-type based collection of registered deserializers.
    /// </summary>
    public interface IDeserializerCollection : IDeserializer
    {
        /// <summary>
        /// Mapps <paramref name="deserializer"/> for deserializing when required content type is <paramref name="contentType"/>.
        /// </summary>
        /// <param name="contentType">Content type for which <paramref name="deserializer"/> will be used.</param>
        /// <param name="deserializer">Deserializer to use for responses with content type <paramref name="contentType"/>.</param>
        /// <returns>Self (for fluency).</returns>
        IDeserializerCollection Map(HttpMediaType contentType, IDeserializer deserializer);

        /// <summary>
        /// Tries to find deserializer for <paramref name="contentType"/>.
        /// </summary>
        /// <param name="contentType">Content type for which to return the deserializer.</param>
        /// <param name="deserializer">Deserializer mapped to <paramref name="contentType"/>.</param>
        /// <returns><c>true</c> if such deserializer exists; <c>false</c> otherwise.</returns>
        bool TryGet(HttpMediaType contentType, out IDeserializer deserializer);
    }
}
