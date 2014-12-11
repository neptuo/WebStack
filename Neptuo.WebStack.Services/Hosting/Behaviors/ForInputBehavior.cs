using Neptuo.WebStack.Services.Behaviors;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Neptuo.WebStack.Serialization;

namespace Neptuo.WebStack.Services.Hosting.Behaviors
{
    /// <summary>
    /// Implementation of <see cref="IForInput<>"/> contract.
    /// </summary>
    /// <typeparam name="T">Type of input.</typeparam>
    public class ForInputBehavior<T> : ForBehavior<IForInput<T>>
    {
        private readonly IDeserializerCollection deserializers;

        public ForInputBehavior()
            : this(Engine.Environment.WithDeserializers())
        { }

        protected ForInputBehavior(IDeserializerCollection deserializers)
        {
            Guard.NotNull(deserializers, "deserializers");
            this.deserializers = deserializers;
        }

        protected override async Task<IHttpResponse> ExecuteAsync(IForInput<T> handler, IHttpRequest httpRequest)
        {
            if (typeof(T) == typeof(string))
            {
                using (StreamReader reader = new StreamReader(httpRequest.InputStream()))
                    handler.Input = (T)(object)(await reader.ReadToEndAsync());
            }
            else if (typeof(Stream).IsAssignableFrom(typeof(T)))
            {
                handler.Input = (T)(object)httpRequest.InputStream();
            }
            else
            {
                HttpMediaType contentType = httpRequest.HeaderContentType();
                if (contentType != null)
                {
                    T instance = await deserializers.TryDeserialize<T>(contentType, httpRequest.InputStream());
                    if(instance == null)
                        throw new NotSupportedException();

                    handler.Input = instance;
                }
            }

            return null;
        }
    }
}
