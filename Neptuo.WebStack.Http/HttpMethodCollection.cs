using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Collections of known Http methods.
    /// </summary>
    public class HttpMethodCollection : KeyedCollection<string, HttpMethod>
    {
        protected override string GetKeyForItem(HttpMethod item)
        {
            return item.Name;
        }
    }
}
