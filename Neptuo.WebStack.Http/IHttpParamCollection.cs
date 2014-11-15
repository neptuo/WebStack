using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// The collection of request parameters (query string or form) for extending.
    /// </summary>
    public interface IHttpParamCollection
    {
        /// <summary>
        /// The inner collection of request parameters.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        IKeyValueCollection Values { get; }
    }
}
