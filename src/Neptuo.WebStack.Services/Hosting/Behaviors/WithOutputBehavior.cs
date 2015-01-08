using Neptuo.WebStack.Services.Behaviors;
using Neptuo.WebStack.Http;
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

        protected override async Task<IHttpResponse> ExecuteAsync(IWithOutput<T> handler, IHttpRequest httpRequest, IHttpResponse httpResponse)
        {
            string output = handler.Output as string;
            if (output != null)
            {
                await httpResponse.OutputWriter().WriteAsync(output);
                return httpResponse;
            }

            Stream outputStream = handler.Output as Stream;
            if (outputStream != null)
            {
                await outputStream.CopyToAsync(httpResponse.OutputStream());
                return httpResponse;
            }

            if (handler.Output != null)
            {
                HttpMediaType contentType = httpResponse.HeaderContentType();
                if(contentType == null)
                    httpResponse.HeaderContentType(contentType = httpRequest.HeaderAccept());


                if (!await serializers.TrySerialize(contentType, httpResponse.OutputStream(), handler.Output))
                    throw new NotSupportedException();
            }

            return httpResponse;
        }
    }
}
