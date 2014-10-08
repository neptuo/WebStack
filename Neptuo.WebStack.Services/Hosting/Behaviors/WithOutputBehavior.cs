using Neptuo.WebStack.Services.Behaviors;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Services.Hosting.Behaviors
{
    /// <summary>
    /// Implementation of <see cref="IWithOutput<>"/> contract.
    /// </summary>
    /// <typeparam name="T">Type of output.</typeparam>
    public class WithOutputBehavior<T> : WithBehavior<IWithOutput<T>>
    {
        protected override void Execute(IWithOutput<T> handler, IHttpContext context)
        {
            string output = handler.Output as string;
            if (output != null)
            {
                context.Response().OutputWriter().Write(output);
                return;
            }

            throw new NotImplementedException();
            //if (handler.Output != null)
            //    context.Response.OutputContext.Serializer.TrySerialize(context.Response, handler.Output);
        }
    }
}
