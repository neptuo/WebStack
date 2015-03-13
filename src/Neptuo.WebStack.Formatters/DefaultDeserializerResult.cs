using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Formatters
{
    /// <summary>
    /// Default implementation of <see cref="IDeserializerResult"/>
    /// </summary>
    public class DefaultDeserializerResult : IDeserializerResult
    {
        public bool IsSuccessful { get; private set; }
        public object Instance { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="isSuccessful">Whether deserialization was successful.</param>
        /// <param name="instance">Deserialized instance.</param>
        public DefaultDeserializerResult(bool isSuccessful, object instance)
        {
            IsSuccessful = isSuccessful;
            Instance = instance;
        }

        /// <summary>
        /// Creates new instance and sets <see cref="IDeserializerResult.IsSuccessful"/> 
        /// based on <paramref name="instance"/> is or is not <c>null</c>.
        /// </summary>
        /// <param name="instance">Deserialized instance.</param>
        public DefaultDeserializerResult(object instance)
            : this(instance != null, instance)
        { }
    }
}
