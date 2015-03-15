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
    /// Default implementation of <see cref="ISerializerContext"/>
    /// </summary>
    public class DefaultSerializerContext : ISerializerContext
    {
        public Stream Output { get; private set; }
        public HttpMediaType ContentType { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="output">Output stream.</param>
        /// <param name="contentType">Content type in which output should be provided.</param>
        public DefaultSerializerContext(Stream output, HttpMediaType contentType)
        {
            Ensure.NotNull(output, "output");
            Ensure.NotNull(contentType, "contentType");
            Output = output;
            ContentType = contentType;
        }
    }
}
