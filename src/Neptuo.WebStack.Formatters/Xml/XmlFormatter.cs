using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemSerializer = System.Xml.Serialization.XmlSerializer;

namespace Neptuo.WebStack.Formatters
{
    /// <summary>
    /// Default implementation of <see cref="ISerializer"/> and <see cref="IDeserializer"/> for <see cref="HttpMediaType.Xml"/>.
    /// </summary>
    public class XmlFormatter : ISerializer, IDeserializer
    {
        public Task<ISerializerResult> TrySerializeAsync(ISerializerContext context, object instance)
        {
            Ensure.NotNull(context, "context");

            if (instance == null)
                return Task.FromResult<ISerializerResult>(new DefaultSerializerResult(true));

            return Task.Factory.StartNew<ISerializerResult>(() =>
            {
                try
                {
                    SystemSerializer serializer = new SystemSerializer(instance.GetType());
                    serializer.Serialize(context.Output, instance);
                    return new DefaultSerializerResult(true);
                }
                catch (Exception)
                {
                    // Ignore all serialization errors.
                    return new DefaultSerializerResult(false);
                }   
            });
        }

        public Task<IDeserializerResult> TryDeserializeAsync(IDeserializerContext context)
        {
            Ensure.NotNull(context, "context");

            return Task.Factory.StartNew<IDeserializerResult>(() =>
            {
                try
                {
                    SystemSerializer serializer = new SystemSerializer(context.RequiredType);
                    return new DefaultDeserializerResult(true, serializer.Deserialize(context.Input));
                }
                catch (Exception)
                {
                    // Ignore all deserialization errors.
                    return new DefaultDeserializerResult(false, null);
                }
            });
        }
    }
}
