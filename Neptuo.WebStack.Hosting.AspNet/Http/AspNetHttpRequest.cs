using Neptuo.Collections.Specialized;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Wraps <see cref="HttpRequest"/>.
    /// </summary>
    public class AspNetHttpRequest : IHttpRequest
    {
        /// <summary>
        /// Original http request.
        /// </summary>
        private readonly HttpRequest request;


        /// <summary>
        /// Cached collection of http headers.
        /// </summary>
        private IReadOnlyDictionary<string, string> headers;

        /// <summary>
        /// Cached collection of query string parameters.
        /// </summary>
        private IReadOnlyDictionary<string, string> queryString;

        /// <summary>
        /// Cached collection of form posted parameters.
        /// </summary>
        private IReadOnlyDictionary<string, string> form;

        /// <summary>
        /// Cached collectio of posted files.
        /// </summary>
        private List<IHttpFile> files;

        public HttpMethod Method { get; private set; }

        public Uri Url
        {
            get { return request.Url; }
        }

        public IReadOnlyDictionary<string, string> Headers
        {
            get
            {
                if (headers == null)
                    headers = new NameValueReadOnlyDictionary(request.Headers);

                return headers;
            }
        }

        public Stream Input
        {
            get { return request.InputStream; }
        }

        public IReadOnlyDictionary<string, string> QueryString
        {
            get
            {
                if (queryString == null)
                    queryString = new NameValueReadOnlyDictionary(request.QueryString);

                return queryString;
            }
        }

        public IReadOnlyDictionary<string, string> Form
        {
            get
            {
                if (form == null)
                    form = new NameValueReadOnlyDictionary(request.Form);

                return form;
            }
        }

        public IEnumerable<IHttpFile> Files
        {
            get
            {
                if (files == null)
                {
                    files = new List<IHttpFile>();
                    foreach (HttpPostedFile file in request.Files)
                        files.Add(new AspNetHttpFile(file));
                }
                return files;
            }
        }

        public AspNetHttpRequest(HttpRequest request)
        {
            Guard.NotNull(request, "request");
            this.request = request;
            Method = (HttpMethod)request.HttpMethod;
        }
    }
}
