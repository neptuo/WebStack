using Neptuo.WebStack.Services.Behaviors;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Neptuo.WebStack.Services.Hosting.Behaviors
{
    /// <summary>
    /// Implementation of <see cref="IForInput<>"/> contract.
    /// </summary>
    /// <typeparam name="T">Type of input.</typeparam>
    public class ForInputBehavior<T> : ForBehavior<IForInput<T>>
    {
        protected override Task<bool> ExecuteAsync(IForInput<T> handler, IHttpContext context)
        {
            if (typeof(T) == typeof(string))
            {
                using (StreamReader reader = new StreamReader(context.Request().InputStream()))
                    handler.Input = (T)Convert.ChangeType((reader.ReadToEnd()), typeof(T));
            }
            else
            {
                throw new NotImplementedException();
                //T model;
                //if (context.Request.InputContext.Deserializer.TryDeserialize(context.Request, out model))
                //    handler.Input = model;
            }

            return Task.FromResult(true);
        }
    }
}
