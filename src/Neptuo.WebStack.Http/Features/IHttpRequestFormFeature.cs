using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http.Features
{
    public interface IHttpRequestFormFeature
    {
        IDictionary<string, string> ParseCollection(Stream body);
    }
}
