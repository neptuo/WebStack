using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemSerializer = System.Xml.Serialization.XmlSerializer;

namespace Neptuo.WebStack.Serialization.Xml
{
    /// <summary>
    /// Default implementation of <see cref="ISerializer"/> and <see cref="IDeserializer"/> for <see cref="HttpMediaType.Xml"/>.
    /// </summary>
    public class XmlSerializer : ISerializer, IDeserializer
    {
        public Task<bool> TrySerializeAsync(Stream stream, object instance)
        {
            Guard.NotNull(stream, "stream");
            Guard.NotNull(instance, "instance");

            return Task.Factory.StartNew(() =>
            {
                SystemSerializer serializer = new SystemSerializer(instance.GetType());
                serializer.Serialize(stream, instance);
                return true;
            });
        }

        public Task<object> TryDeserializeAsync(Stream stream, Type type)
        {
            Guard.NotNull(stream, "stream");
            Guard.NotNull(type, "instanceType");

            return Task.Factory.StartNew(() =>
            {
                SystemSerializer serializer = new SystemSerializer(type);
                return serializer.Deserialize(stream);
            });
        }
    }
}
