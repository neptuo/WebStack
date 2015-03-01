using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Services.Hosting.Behaviors
{
    /// <summary>
    /// Provides access to currently executing pipeline.
    /// </summary>
    public interface IBehaviorContext
    {
        /// <summary>
        /// Promotes execution to next behavior in pipeline.
        /// </summary>
        /// <param name="httpContext">Current HTTP context.</param>
        /// <returns><c>true</c> if request was handled; <c>false</c> to process request by next handler.</returns>
        Task<bool> NextAsync(IHttpContext httpContext);
    }
}
