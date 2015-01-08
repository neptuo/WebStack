using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    public class AspNetParamCollection : KeyValueCollection, IHttpParamCollection
    {
        public AspNetParamCollection(NameValueCollection source)
            : base(source)
        {
            IsReadOnly = true;
        }
    }
}
