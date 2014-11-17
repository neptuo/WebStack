﻿using System;
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
        public const string Status = "Response.Status";
        public const string Headers = "Response.Headers";
        public const string OutputStream = "Response.OutputStream";
        public const string OutputWriter = "Response.OutputWriter";
    }
}
