using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Services.Hosting.Behaviors
{
    /// <summary>
    /// Executes <see cref="IGet"/> handler.
    /// </summary>
    public class GetBehavior : IBehavior<IGet>
    {
        /// <summary>
        /// Executes <see cref="IGet.ExecuteAsync"/> method on <paramref name="handler"/> if current request is GET request.
        /// </summary>
        /// <param name="handler">Behavior interface.</param>
        /// <param name="context">Current HTTP request.</param>
        /// <param name="pipeline">Processing pipeline.</param>
        public async Task<IHttpResponse> ExecuteAsync(IGet handler, IHttpRequest httpRequest, IBehaviorContext pipeline)
        {
            if (httpRequest.IsMethodGet())
            {
                if (await handler.ExecuteAsync())
                    return new DefaultHttpResponse();
            }

            return await pipeline.NextAsync(httpRequest);
        }
    }
}
