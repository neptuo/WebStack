using Neptuo.FeatureModels;
using Neptuo.WebStack.Http.Features;
using Neptuo.WebStack.Http.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Common extensions for <see cref="HttpRequest"/> for parsing request content as files.
    /// </summary>
    public static class _HttpRequestExtensions_Files
    {
        public static IEnumerable<IHttpFile> Files(this HttpRequest httpRequest)
        {
            Guard.NotNull(httpRequest, "httpRequest");

            IEnumerable<IHttpFile> files;
            if (!httpRequest.CustomValues().TryGet(RequestKey.Files, out files))
            {
                files = httpRequest.Context().With<IHttpRequestFileFeature>().ParseCollection(httpRequest.RawMessage().BodyStream);
                httpRequest.CustomValues().Set(RequestKey.Files, files);
            }

            return files;
        }
    }
}
