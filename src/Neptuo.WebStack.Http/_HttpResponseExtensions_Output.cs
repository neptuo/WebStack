using Neptuo.FeatureModels;
using Neptuo.WebStack.Http.Keys;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Common extensions for <see cref="HttpResponse"/> for writing response body.
    /// </summary>
    public static class _HttpResponseExtensions_Output
    {
        /// <summary>
        /// Response text writer.
        /// </summary>
        public static TextWriter OutputWriter(this HttpResponse httpResponse)
        {
            Ensure.NotNull(httpResponse, "httpResponse");

            TextWriter writer;
            if (!httpResponse.CustomValues().TryGet<TextWriter>(ResponseKey.BodyWriter, out writer))
            {
                writer = new StreamWriter(httpResponse.RawMessage().BodyStream) { AutoFlush = true };
                httpResponse.CustomValues().Set(ResponseKey.BodyWriter, writer);
                httpResponse.Context().With<IHttpContextNotification>().OnOutputFlushing += writer.Flush;
            }

            return writer;
        }
    }
}
