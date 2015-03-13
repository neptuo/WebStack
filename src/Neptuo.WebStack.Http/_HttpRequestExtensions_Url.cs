using Neptuo.FeatureModels;
using Neptuo.WebStack.Http.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Common extensions for <see cref="HttpRequest"/> for request URL.
    /// </summary>
    public static class _HttpRequestExtensions_Url
    {
        public static IReadOnlyUrl Url(this HttpRequest httpRequest)
        {
            Ensure.NotNull(httpRequest, "httpRequest");

            IReadOnlyUrl url;
            if (!httpRequest.CustomValues().TryGet(RequestKey.Url, out url))
            {
                url = httpRequest
                    .Context()
                    .With<IUrlBuilder>()
                    .FromUrl(httpRequest.RawMessage().Url);

                httpRequest.CustomValues().Set(RequestKey.Url, url);
            }

            return url;
        }
    }
}
