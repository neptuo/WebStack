using Neptuo.Collections.Specialized;
using Neptuo.ComponentModel.Behaviors;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Services.Hosting
{
    public static class _BehaviorContextExtensions
    {
        #region IHttpContext

        public static IHttpContext HttpContext(this IBehaviorContext context)
        {
            Ensure.NotNull(context, "context");
            return context.CustomValues.Get<IHttpContext>("HttpContext");
        }

        public static IBehaviorContext HttpContext(this IBehaviorContext context, IHttpContext httpContext)
        {
            Ensure.NotNull(context, "context");
            Ensure.NotNull(httpContext, "httpContext");
            context.CustomValues.Set("HttpContext", httpContext);
            return context;
        }

        #endregion

        #region IsHandled

        public static bool IsHandled(this IBehaviorContext context)
        {
            Ensure.NotNull(context, "context");
            return context.CustomValues.Get("IsHandled", true);
        }

        public static IBehaviorContext MarkAsNotHandled(this IBehaviorContext context)
        {
            Ensure.NotNull(context, "context");
            context.CustomValues.Set("IsHandled", false);
            return context;
        }

        #endregion
    }
}
