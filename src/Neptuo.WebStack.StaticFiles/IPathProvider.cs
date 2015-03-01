using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.StaticFiles
{
    public interface IPathProvider
    {
        string GetPath(IHttpContext httpContext);
    }
}
