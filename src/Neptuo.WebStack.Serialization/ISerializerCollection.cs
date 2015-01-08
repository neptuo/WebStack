using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Serialization
{
    /// <summary>
    /// Collection of registered deserializers.
    /// </summary>
    public interface ISerializerCollection
    {
        /// <summary>
        /// Mapps <paramref name="serializer"/> for serializing when required content type is <paramref name="contentType"/>.
        /// </summary>
        /// <param name="contentType">Content type for which <paramref name="serializer"/> will be used.</param>
        /// <param name="serializer">Serializer to use for requests with content type <paramref name="contentType"/>.</param>
        /// <returns>Self (for fluency).</returns>
        ISerializerCollection Map(HttpMediaType contentType, ISerializer serializer);

        /// <summary>
        /// Tries to find serializer for <paramref name="contentType"/>.
        /// </summary>
        /// <param name="contentType">Content type for which to return the serializer.</param>
        /// <param name="serializer">Serializer mapped to <paramref name="contentType"/>.</param>
        /// <returns><c>true</c> if such serializer exists; <c>false</c> otherwise.</returns>
        bool TryGet(HttpMediaType contentType, out ISerializer serializer);
    }
}
