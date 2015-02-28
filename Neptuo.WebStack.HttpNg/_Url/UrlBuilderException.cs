using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Describes error in building url.
    /// </summary>
    [Serializable]
    public class UrlBuilderException : Exception
    {
        public UrlBuilderException() 
        { }

        public UrlBuilderException(string message) 
            : base(message) 
        { }
        
        public UrlBuilderException(string message, Exception inner) 
            : base(message, inner) 
        { }
        
        protected UrlBuilderException(SerializationInfo info, StreamingContext context)
            : base(info, context) 
        { }
    }

    /// <summary>
    /// Describes error in part of URL building.
    /// </summary>
    [Serializable]
    public class UrlPartMalFormattedException : UrlBuilderException
    {
        public UrlPartMalFormattedException(string partName, string value)
            : base(String.Format("Part of URL '{0}' can't accept value of '{1}'.", partName, value))
        { }
    }
}
