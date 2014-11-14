using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http.Keys
{
    /// <summary>
    /// Common keys for HTTP response object.
    /// </summary>
    public static class ResponseKey
    {
        public const string Status = "Status";
        public const string Headers = "Headers";
        public const string OutputStream = "OutputStream";
        public const string OutputWriter = "OutputWriter";
    }
}
