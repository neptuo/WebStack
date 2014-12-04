using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Services.Hosting.Behaviors
{
    /// <summary>
    /// Executes <see cref="IPut"/> handler.
    /// </summary>
    public class PutBehavior : IBehavior<IPut>
    {
        /// <summary>
        /// Executes <see cref="IPut.ExecuteAsync"/> method on <paramref name="handler"/> if current request is PUT request.
        /// </summary>
        /// <param name="handler">Behavior interface.</param>
        /// <param name="httpRequest">Current HTTP request.</param>
        /// <param name="pipeline">Processing pipeline.</param>
        public async Task<IHttpResponse> ExecuteAsync(IPut handler, IHttpRequest httpRequest, IBehaviorContext pipeline)
        {
            if (httpRequest.IsMethodPut())
            {
                await handler.ExecuteAsync();
                return new DefaultHttpResponse();
            }
            
            return await pipeline.NextAsync(httpRequest);
        }
    }
}
