using Neptuo.WebStack.Diagnostics;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TestWebApp.Exceptions
{
    public class NullReferenceExceptionHandler : ExceptionHandlerBase<NullReferenceException>
    {
        protected async override Task<bool> TryHandleAsync(NullReferenceException exception, IHttpContext httpContext)
        {
            await httpContext.Response().OutputWriter().WriteLineAsync("Raised NullReferenceException!");
            return true;
        }
    }
}