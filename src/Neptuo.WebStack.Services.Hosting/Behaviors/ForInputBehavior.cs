using Neptuo.WebStack.Formatters;
using Neptuo.WebStack.Http;
using Neptuo.WebStack.Http.Messages;
using Neptuo.WebStack.Services.Behaviors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Services.Hosting.Behaviors
{
    /// <summary>
    /// Implementation of <see cref="IForInput<>"/> contract.
    /// </summary>
    /// <typeparam name="T">Type of input.</typeparam>
    public class ForInputBehavior<T> : ForBehavior<IForInput<T>>
    {
        private readonly IDeserializer deserializer;

        public ForInputBehavior()
            : this(Engine.Environment.WithDeserializer())
        { }

        protected ForInputBehavior(IDeserializer deserializer)
        {
            Ensure.NotNull(deserializer, "deserializer");
            this.deserializer = deserializer;
        }

        protected override async Task<bool> ExecuteAsync(IForInput<T> handler, IHttpContext httpContext)
        {
            if (typeof(T) == typeof(string))
            {
                using (StreamReader reader = new StreamReader(httpContext.RequestMessage().BodyStream))
                    handler.Input = (T)(object)(await reader.ReadToEndAsync());
            }
            else if (typeof(Stream).IsAssignableFrom(typeof(T)))
            {
                handler.Input = (T)(object)httpContext.RequestMessage().BodyStream;
            }
            else
            {
                HttpMediaType contentType = httpContext.Request().Headers().ContentType();
                if (contentType != null)
                {
                    IDeserializerResult result = await deserializer.TryDeserializeAsync(
                        new DefaultDeserializerContext(
                            httpContext.RequestMessage().BodyStream, 
                            contentType, 
                            typeof(T)
                        )
                    );

                    if(!result.IsSuccessful)
                        throw new NotSupportedException();

                    handler.Input = (T)result.Instance;
                }
            }

            return false;
        }
    }
}
