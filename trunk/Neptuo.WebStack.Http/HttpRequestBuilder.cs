using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    public class HttpRequestBuilder : IHttpRequest
    {
        public HttpRequestBuilder()
        {

        }

        public IHttpRequest ToRequest()
        {
            return this;
        }

        #region IHttpRequest

        IDependencyProvider IHttpRequest.DependencyProvider()
        {
            throw new NotImplementedException();
        }

        HttpMethod IHttpRequest.Method()
        {
            throw new NotImplementedException();
        }

        IReadOnlyUrl IHttpRequest.Url()
        {
            throw new NotImplementedException();
        }

        IReadOnlyKeyValueCollection IHttpRequest.Headers()
        {
            throw new NotImplementedException();
        }

        IHttpParamCollection IHttpRequest.QueryString()
        {
            throw new NotImplementedException();
        }

        System.IO.Stream IHttpRequest.InputStream()
        {
            throw new NotImplementedException();
        }

        IHttpParamCollection IHttpRequest.Form()
        {
            throw new NotImplementedException();
        }

        IEnumerable<IHttpFile> IHttpRequest.Files()
        {
            throw new NotImplementedException();
        }

        CancellationToken IHttpRequest.CancellationToken()
        {
            throw new NotImplementedException();
        }

        event Action IHttpRequest.OnDisposing
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        IKeyValueCollection IHttpRequest.CustomValues()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
