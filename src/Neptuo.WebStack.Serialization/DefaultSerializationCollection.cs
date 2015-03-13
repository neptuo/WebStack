using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Serialization
{
    /// <summary>
    /// Default implementation of <see cref="ISerializerCollection"/> and <see cref="IDeserializerCollection"/>.
    /// </summary>
    public class DefaultSerializationCollection : ISerializerCollection, IDeserializerCollection
    {
        private readonly object serializerLock = new object();
        private readonly object deserilizerLock = new object();

        private readonly Dictionary<HttpMediaType, ISerializer> serializers = new Dictionary<HttpMediaType, ISerializer>();
        private readonly Dictionary<HttpMediaType, IDeserializer> deserializers = new Dictionary<HttpMediaType, IDeserializer>();

        public ISerializerCollection Map(HttpMediaType contentType, ISerializer serializer)
        {
            Ensure.NotNull(contentType, "contentType");
            Ensure.NotNull(serializer, "serializer");
            lock (serializerLock)
            {
                serializers[contentType] = serializer;
            }

            return this;
        }

        public IDeserializerCollection Map(HttpMediaType contentType, IDeserializer deserializer)
        {
            Ensure.NotNull(contentType, "contentType");
            Ensure.NotNull(deserializer, "deserializer");
            lock (deserilizerLock)
            {
                deserializers[contentType] = deserializer;
            }

            return this;
        }

        public bool TryGet(HttpMediaType contentType, out ISerializer serializer)
        {
            return serializers.TryGetValue(contentType, out serializer);
        }

        public bool TryGet(HttpMediaType contentType, out IDeserializer deserializer)
        {
            return deserializers.TryGetValue(contentType, out deserializer);
        }
    }
}
