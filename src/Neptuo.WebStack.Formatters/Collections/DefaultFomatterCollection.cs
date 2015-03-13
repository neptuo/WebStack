using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Formatters.Collections
{
    /// <summary>
    /// Default implementation of <see cref="ISerializerCollection"/> and <see cref="IDeserializerCollection"/>.
    /// </summary>
    public class DefaultFomatterCollection : ISerializerCollection, IDeserializerCollection
    {
        private readonly object serializerLock = new object();
        private readonly object deserilizerLock = new object();

        private readonly Dictionary<HttpMediaType, ISerializer> serializers = new Dictionary<HttpMediaType, ISerializer>();
        private readonly Dictionary<HttpMediaType, IDeserializer> deserializers = new Dictionary<HttpMediaType, IDeserializer>();

        #region ISerializerCollection

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

        public bool TryGet(HttpMediaType contentType, out ISerializer serializer)
        {
            return serializers.TryGetValue(contentType, out serializer);
        }

        public Task<ISerializerResult> TrySerializeAsync(ISerializerContext context, object instance)
        {
            ISerializer serializer;
            if (serializers.TryGetValue(context.ContentType, out serializer))
                return serializer.TrySerializeAsync(context, instance);

            return Task.FromResult<ISerializerResult>(new DefaultSerializerResult(false));
        }

        #endregion

        #region IDeserializerCollection

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

        public bool TryGet(HttpMediaType contentType, out IDeserializer deserializer)
        {
            return deserializers.TryGetValue(contentType, out deserializer);
        }

        public Task<IDeserializerResult> TryDeserializeAsync(IDeserializerContext context)
        {
            IDeserializer deserializer;
            if (deserializers.TryGetValue(context.ContentType, out deserializer))
                return deserializer.TryDeserializeAsync(context);

            return Task.FromResult<IDeserializerResult>(new DefaultDeserializerResult(false, null));
        }

        #endregion
    }
}
