using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Describes parts of url.
    /// </summary>
    public interface IReadOnlyUrl
    {
        /// <summary>
        /// Schema (protocol) name of the URL.
        /// Eg.: http, https.
        /// </summary>
        string Schema { get; }

        /// <summary>
        /// <c>true</c> if the URL contains information about schema.
        /// </summary>
        bool HasSchema { get; }

        /// <summary>
        /// Domain name + port part of the URL.
        /// </summary>
        string Host { get; }

        /// <summary>
        /// <c>true</c> if the URL contains information about domain name.
        /// </summary>
        bool HasHost { get; }
        
        /// <summary>
        /// Absolute path of the URL.
        /// </summary>
        string Path { get; }

        /// <summary>
        /// <c>true</c> if the URL has defined absolute (/abc) path.
        /// </summary>
        bool HasPath { get; }

        /// <summary>
        /// Application relative path of the URL 
        /// </summary>
        string VirtualPath { get; }

        /// <summary>
        /// <c>true</c> if the URL has defined application relative (~/abc) path.
        /// </summary>
        bool HasVirtualPath { get; }

        /// <summary>
        /// URL query string part.
        /// </summary>
        IReadOnlyDictionary<string, string> QueryString { get; }

        /// <summary>
        /// <c>true</c> if the URL has defined query string.
        /// </summary>
        bool HasQueryString { get; }
        
        /// <summary>
        /// Format can contain 'S', 'H', 'P' and 'Q'.
        /// </summary>
        /// <param name="format"></param>
        /// <returns>Formatted string value of this URL.</returns>
        string ToString(string format);
    }
}
