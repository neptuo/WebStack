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
    /// Default implementation of <see cref="IDeserializerContext"/>.
    /// </summary>
    public class DefaultDeserializerContext : IDeserializerContext
    {
        public Stream Input { get; private set; }
        public HttpMediaType ContentType { get; private set; }
        public Type RequiredType { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="input">Input source.</param>
        /// <param name="contentType">Content type of <paramref name="input"/>.</param>
        /// <param name="requiredType">Required base type that should be created.</param>
        public DefaultDeserializerContext(Stream input, HttpMediaType contentType, Type requiredType)
        {
            Ensure.NotNull(input, "input");
            Ensure.NotNull(contentType, "contentType");
            Ensure.NotNull(requiredType, "requiredType");
            Input = input;
            ContentType = contentType;
            RequiredType = requiredType;
        }
    }
}
