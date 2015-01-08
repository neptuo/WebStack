using Neptuo.Collections.Specialized;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.StaticFiles
{
    public class UrlPathProvider : IPathProvider
    {
        public string GetPath(IHttpRequest httpRequest)
        {
            string rawUrl;
            if (!httpRequest.CustomValues().TryGet("FileSystemRequestHandler:FileName", out rawUrl))
                rawUrl = httpRequest.Url().Path;

            rawUrl = rawUrl.Replace("%20", " ");
            //string path = rawUrl.Substring(rawUrl.IndexOf('/', 9));

            if (rawUrl.StartsWith("/"))
                rawUrl = rawUrl.Substring(1);

            return rawUrl;
        }
    }
}
