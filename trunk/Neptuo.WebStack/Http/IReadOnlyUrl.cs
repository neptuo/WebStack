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
        /// Domain name part of the URL.
        /// </summary>
        string Domain { get; }

        /// <summary>
        /// <c>true</c> if the URL contains information about domain name.
        /// </summary>
        bool HasDomain { get; }
        
        /// <summary>
        /// Absolute path of the URL.
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Application relative path of the URL 
        /// (if the URL was specified as application relative).
        /// </summary>
        string VirtualPath { get; }

        /// <summary>
        /// <c>true</c> if the URL defined using application relative path.
        /// </summary>
        bool HasVirtualPath { get; }
    }
}
