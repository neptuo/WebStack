using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Serialization
{
    /// <summary>
    /// Common extensions for serializing and deserialiting objects.
    /// </summary>
    public static class _SerializationExtensions
    {
        public static async Task<T> TryDeserialize<T>(this IDeserializerCollection collection, HttpMediaType dataType, Stream rawData)
        {
            Ensure.NotNull(collection, "collection");

            IDeserializer deserializer;
            if (!collection.TryGet(dataType, out deserializer))
                return default(T);

            return (T)(await deserializer.TryDeserializeAsync(rawData, typeof(T)));
        }

        public static async Task<bool> TrySerialize<T>(this ISerializerCollection collection, HttpMediaType dataType, Stream target, T instance)
        {
            Ensure.NotNull(collection, "collection");

            ISerializer serializer;
            if (!collection.TryGet(dataType, out serializer))
                return false;

            return await serializer.TrySerializeAsync(target, instance);
        }
    }
}
