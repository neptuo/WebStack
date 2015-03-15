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
    /// Context for serialization by <see cref="ISerializer"/>.
    /// </summary>
    public interface ISerializerContext
    {
        /// <summary>
        /// Output stream.
        /// </summary>
        Stream Output { get; }

        /// <summary>
        /// Content type in which output should be provided.
        /// </summary>
        HttpMediaType ContentType { get; }
    }
}
