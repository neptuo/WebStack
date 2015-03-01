using Neptuo.WebStack.Http;
using Neptuo.WebStack.Http.Messages;
using Neptuo.WebStack.Services.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neptuo.ComponentModel.Converters;
using Neptuo.WebStack.Serialization;
using System.IO;

namespace Neptuo.WebStack.Services.Hosting.Behaviors
{
    /// <summary>
    /// Implementation of <see cref="IWithOutput<>"/> contract.
    /// </summary>
    /// <typeparam name="T">Type of output.</typeparam>
    public class WithOutputBehavior<T> : WithBehavior<IWithOutput<T>>
    {
        private readonly ISerializerCollection serializers;

        public WithOutputBehavior()
            : this(Engine.Environment.WithSerializers())
        { }

        protected WithOutputBehavior(ISerializerCollection serializers)
        {
            Guard.NotNull(serializers, "serializers");
            this.serializers = serializers;
        }

        protected override async Task<bool> ExecuteAsync(IWithOutput<T> handler, IHttpContext httpContext)
        {
            string output = handler.Output as string;
            if (output != null)
            {
                await httpContext.Response().OutputWriter().WriteAsync(output);
                return true;
            }

            Stream outputStream = handler.Output as Stream;
            if (outputStream != null)
            {
                await outputStream.CopyToAsync(httpContext.ResponseMessage().BodyStream);
                return true;
            }

            if (handler.Output != null)
            {
                HttpMediaType contentType = httpContext.Response().Headers().ContentType();
                if(contentType == null)
                    httpContext.Response().Headers().ContentType(contentType = httpContext.Request().Headers().Accept().FirstOrDefault());

                if (!await serializers.TrySerialize(contentType, httpContext.ResponseMessage().BodyStream, handler.Output))
                    throw new NotSupportedException();
            }

            return true;
        }
    }
}
