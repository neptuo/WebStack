using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Formatters
{
    /// <summary>
    /// Context for deserialization by <see cref="IDeserializer"/>.
    /// </summary>
    public interface IDeserializerContext
    {
        /// <summary>
        /// Input source.
        /// </summary>
        Stream Input { get; }

        /// <summary>
        /// Content type of <see cref="IDeserializerContext.Input"/>.
        /// </summary>
        HttpMediaType ContentType { get; }

        /// <summary>
        /// Required base type that should be created.
        /// </summary>
        Type RequiredType { get; }
    }
}
