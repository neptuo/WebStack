﻿using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Http request.
    /// </summary>
    public interface IHttpRequest
    {
        /// <summary>
        /// Http method.
        /// </summary>
        HttpMethod Method { get; }

        /// <summary>
        /// Requested url.
        /// </summary>
        Uri Url { get; }

        /// <summary>
        /// Http request headers.
        /// </summary>
        IReadOnlyDictionary<string, string> Headers { get; }

        /// <summary>
        /// Input stream
        /// </summary>
        Stream Input { get; }

        /// <summary>
        /// Input query string.
        /// </summary>
        IReadOnlyDictionary<string, string> QueryString { get; }

        /// <summary>
        /// Data posted as form data.
        /// </summary>
        IReadOnlyDictionary<string, string> Form { get; }

        /// <summary>
        /// Posted files.
        /// </summary>
        IEnumerable<IHttpFile> Files { get; }
    }
}
