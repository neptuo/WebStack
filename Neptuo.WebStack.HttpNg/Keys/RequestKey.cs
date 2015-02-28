using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http.Keys
{
    public static class RequestKey
    {
        public const string Root = "Request";
        public const string Method = "Request.Method";
        public const string Url = "Request.Url";
        public const string Headers = "Request.Headers";
        public const string InputStream = "Request.InputStream";
        public const string QueryString = "Request.QueryString";
        public const string Form = "Request.Form";
        public const string Files = "Request.Files";
        public const string CancellationToken = "Request.CancellationToken";
        public const string ApplicationPath = "Request.ApplicationPath";
    }
}
