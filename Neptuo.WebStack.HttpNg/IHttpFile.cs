using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Describes file sent over HTTP.
    /// </summary>
    public interface IHttpFile
    {
        /// <summary>
        /// File name.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// File content type.
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// File size in bytes.
        /// </summary>
        int ContentLength { get; }

        /// <summary>
        /// File content stream.
        /// </summary>
        Stream Content { get; }
    }
}
