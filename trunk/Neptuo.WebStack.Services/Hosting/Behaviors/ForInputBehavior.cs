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
    /// Implementation of <see cref="IForInput<>"/> contract.
    /// </summary>
    /// <typeparam name="T">Type of input.</typeparam>
    public class ForInputBehavior<T> : ForBehavior<IForInput<T>>
    {
        protected override Task ExecuteAsync(IForInput<T> handler, IHttpContext context)
        {
            throw new NotImplementedException();
            //T model;
            //if (context.Request.InputContext.Deserializer.TryDeserialize(context.Request, out model))
            //    handler.Input = model;
        }
    }
}
