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
        public string GetPath(IHttpContext httpContext)
        {
            string rawUrl;
            if (!httpContext.CustomValues().TryGet("FileSystemRequestHandler:FileName", out rawUrl))
                rawUrl = httpContext.Request().Url().Path;

            rawUrl = rawUrl.Replace("%20", " ");
            //string path = rawUrl.Substring(rawUrl.IndexOf('/', 9));

            if (rawUrl.StartsWith("/"))
                rawUrl = rawUrl.Substring(1);

            return rawUrl;
        }
    }
}
