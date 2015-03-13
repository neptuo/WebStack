using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http.Features
{
    public interface IHttpRequestFileFeature
    {
        IEnumerable<IHttpFile> ParseCollection(Stream body);
    }
}
