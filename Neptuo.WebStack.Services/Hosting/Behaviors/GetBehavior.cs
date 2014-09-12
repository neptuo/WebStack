﻿using Neptuo.WebStack.Http;
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
        /// Executes <see cref="IGet.Execute"/> method on <paramref name="handler"/> if current request is GET request.
        /// </summary>
        /// <param name="handler">Behavior interface.</param>
        /// <param name="context">Current Http context.</param>
        /// <param name="pipeline">Processing pipeline.</param>
        public void Execute(IGet handler, IHttpContext context, IBehaviorContext pipeline)
        {
            if (context.Request.Method == HttpMethod.Get)
                handler.Execute();
            else
                pipeline.Next();
        }
    }
}
