using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Services.Hosting.Behaviors
{
    /// <summary>
    /// Executes <see cref="IPost"/> handler.
    /// </summary>
    public class PostBehavior : IBehavior<IPost>
    {
        /// <summary>
        /// Executes <see cref="IPost.ExecuteAsync"/> method on <paramref name="handler"/> if current request is POST request.
        /// </summary>
        /// <param name="handler">Behavior interface.</param>
        /// <param name="context">Current HTTP request.</param>
        /// <param name="pipeline">Processing pipeline.</param>
        public async Task<IHttpResponse> ExecuteAsync(IPost handler, IHttpRequest httpRequest, IBehaviorContext pipeline)
        {
            if (httpRequest.IsMethodPost())
            {
                if (await handler.ExecuteAsync())
                    return new DefaultHttpResponse();
            }

            return await pipeline.NextAsync(httpRequest);
        }
    }
}
