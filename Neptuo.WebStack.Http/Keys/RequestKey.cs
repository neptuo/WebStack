using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http.Keys
{
    public static class RequestKey
    {
        public const string Method = "Method";
        public const string Url = "Url";
        public const string Headers = "Headers";
        public const string InputStream = "InputStream";
        public const string QueryString = "QueryString";
        public const string Form = "Form";
        public const string Files = "Files";
        public const string CancellationToken = "CancellationToken";
    }
}
