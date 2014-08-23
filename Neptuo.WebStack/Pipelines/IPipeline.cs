using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Pipelines
{
    /// <summary>
    /// Defines invokable pipeline for processing complete Http request with response.
    /// </summary>
    public interface IPipeline
    {
        /// <summary>
        /// Process <paramref name="httpContext"/>.
        /// </summary>
        /// <param name="httpContext">Current Http context.</param>
        void Invoke(IHttpContext httpContext);
    }
}
