using Neptuo.Collections.Specialized;
using Neptuo.FeatureModels;
using Neptuo.WebStack.Http.Collections.Specialized;
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
    /// Common extensions for <see cref="HttpRequest"/> for query string and form parameters.
    /// </summary>
    public static class _HttpRequestExtensions_Params
    {
        public static HttpRequestParamCollection QueryString(this HttpRequest httpRequest)
        {
            Guard.NotNull(httpRequest, "httpRequest");

            HttpRequestParamCollection queryString;
            if (!httpRequest.CustomValues().TryGet(RequestKey.QueryString, out queryString))
            {
                queryString = new HttpRequestParamCollection(httpRequest.Url().QueryString);
                httpRequest.CustomValues().Set(RequestKey.QueryString, queryString);
            }

            return queryString;
        }

        public static HttpRequestParamCollection Form(this HttpRequest httpRequest)
        {
            HttpRequestParamCollection form;
            if (!httpRequest.CustomValues().TryGet(RequestKey.Form, out form))
            {
                form = new HttpRequestParamCollection(
                    httpRequest
                        .Context()
                        .With<IHttpRequestFormFeature>()
                        .ParseCollection(httpRequest.RawMessage().BodyStream)
                );

                httpRequest.CustomValues().Set(RequestKey.Form, form);
            }

            return form;
        }

        public static HttpRequestParamCollection Params(this HttpRequest httpRequest)
        {
            HttpRequestParamCollection parameters;
            if (!httpRequest.CustomValues().TryGet(RequestKey.Params, out parameters))
            {
                parameters = new HttpRequestParamCollection(
                    new MultiKeyValueCollection(
                        httpRequest.Form(), 
                        httpRequest.QueryString()
                    )
                );

                httpRequest.CustomValues().Set(RequestKey.Form, parameters);
            }

            return parameters;
        }
    }
}
