using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Default implementation of <see cref="IRawHttpRequest"/>
    /// </summary>
    public class DefaultRawHttpRequest : IRawHttpRequest
    {
        public string Method { get; private set; }
        public string Url { get; private set; }
        public string Protocol { get; private set; }
        public IDictionary<string, string> Headers { get; private set; }
        public Stream InputStream { get; private set; }

        public DefaultRawHttpRequest(string method, string url, string protocol, IDictionary<string, string> headers, Stream inputStream)
        {
            Guard.NotNullOrEmpty(method, "method");
            Guard.NotNullOrEmpty(url, "url");
            Guard.NotNullOrEmpty(protocol, "protocol");
            Guard.NotNull(headers, "headers");
            Guard.NotNull(inputStream, "inputStream");
            Method = method;
            Url = url;
            Protocol = protocol;
            Headers = headers;
            InputStream = inputStream;
        }
    }
}
