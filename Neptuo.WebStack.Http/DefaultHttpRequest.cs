using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Base implementation of <see cref="IHttpResponse"/>.
    /// </summary>
    public class DefaultHttpRequest : IHttpRequest
    {
        private readonly IDependencyProvider dependencyProvider;
        private readonly HttpMethod method;
        private readonly IReadOnlyUrl url;
        private readonly IReadOnlyKeyValueCollection headers;
        private readonly IHttpParamCollection queryString;
        private readonly Stream inputStream;
        private readonly IHttpParamCollection form;
        private readonly IEnumerable<IHttpFile> files;
        private readonly CancellationToken cancellationToken;
        private readonly IKeyValueCollection customValues;

        public event Action OnDisposing;

        public DefaultHttpRequest(
            IDependencyProvider dependencyProvider, 
            HttpMethod method, 
            IReadOnlyUrl url, 
            IReadOnlyKeyValueCollection headers, 
            IHttpParamCollection queryString,
            Stream inputStream,
            IHttpParamCollection form,
            IEnumerable<IHttpFile> files,
            CancellationToken cancellationToken,
            IKeyValueCollection customValues)  
        {
            this.dependencyProvider = dependencyProvider;
            this.method = method;
            this.url = url;
            this.headers = headers;
            this.queryString = queryString;
            this.inputStream = inputStream;
            this.form = form;
            this.files = files;
            this.cancellationToken = cancellationToken;
            this.customValues = customValues;
        }

        public IDependencyProvider DependencyProvider()
        {
            return dependencyProvider;
        }

        public HttpMethod Method()
        {
            return method;
        }

        public IReadOnlyUrl Url()
        {
            return url;
        }

        public IReadOnlyKeyValueCollection Headers()
        {
            return headers;
        }

        public IHttpParamCollection QueryString()
        {
            return queryString;
        }

        public Stream InputStream()
        {
            return inputStream;
        }

        public IHttpParamCollection Form()
        {
            return form;
        }

        public IEnumerable<IHttpFile> Files()
        {
            return files;
        }

        public CancellationToken CancellationToken()
        {
            return cancellationToken;
        }

        public IKeyValueCollection CustomValues()
        {
            return customValues;
        }

        public void RaiseOnDisposing()
        {
            if (OnDisposing != null)
                OnDisposing();
        }
    }
}
