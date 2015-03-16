using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Formatters
{
    /// <summary>
    /// Default implementation of <see cref="ISerializer"/> and <see cref="IDeserializer"/> for <see cref="HttpMediaType.Json"/>.
    /// </summary>
    public class JsonFormatter : ISerializer, IDeserializer
    {
        public async Task<ISerializerResult> TrySerializeAsync(ISerializerContext context, object instance)
        {
            Ensure.NotNull(context, "context");

            try
            {
                string serialized = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(instance));
                using (StreamWriter writer = new StreamWriter(context.Output))
                {
                    await writer.WriteAsync(serialized);
                }

                return new DefaultSerializerResult(true);
            }
            catch (Exception)
            {
                // Ignore all serialization errors.
                return new DefaultSerializerResult(false);
            }
        }

        public Task<IDeserializerResult> TryDeserializeAsync(IDeserializerContext context)
        {
            Ensure.NotNull(context, "context");

            using (StreamReader inputReader = new StreamReader(context.Input))
            {
                return Task.Factory.StartNew<IDeserializerResult>(() =>
                {
                    try
                    {
                        object instance = JsonConvert.DeserializeObject(inputReader.ReadToEnd(), context.RequiredType, new JsonSerializerSettings());
                        return new DefaultDeserializerResult(true, instance);
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
}
