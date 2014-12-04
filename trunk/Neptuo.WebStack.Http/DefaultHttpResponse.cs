using Neptuo.Collections.Specialized;
using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Base implementation of <see cref="IHttpResponse"/>.
    /// </summary>
    public class DefaultHttpResponse : DisposableBase, IHttpResponse
    {
        private HttpStatus status;
        private IKeyValueCollection headers;
        private Stream outputStream;
        private IKeyValueCollection customValues;

        public event Action OnDisposing;

        public DefaultHttpResponse()
            : this(HttpStatus.Ok)
        { }

        public DefaultHttpResponse(HttpStatus status)
            : this(HttpStatus.Ok, new MemoryStream(100000))
        { }

        public DefaultHttpResponse(HttpStatus status, Stream outputStream)
            : this(status, new KeyValueCollection(), outputStream, new KeyValueCollection())
        { }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="status">Initial status code.</param>
        /// <param name="headers">Initial header collection.</param>
        /// <param name="outputStream">Output stream.</param>
        /// <param name="customValues">Collection for custom values.</param>
        public DefaultHttpResponse(HttpStatus status, IKeyValueCollection headers, Stream outputStream, IKeyValueCollection customValues)
        {
            Guard.NotNull(status, "status");
            Guard.NotNull(headers, "headers");
            Guard.NotNull(outputStream, "outputStream");
            Guard.NotNull(customValues, "customValues");
            this.status = status;
            this.headers = headers;
            this.outputStream = outputStream;
            this.customValues = customValues;
        }

        public HttpStatus Status()
        {
            return status;
        }

        public IHttpResponse Status(HttpStatus status)
        {
            Guard.NotNull(status, "status");
            this.status = status;
            return this;
        }

        public IKeyValueCollection Headers()
        {
            return headers;
        }

        public Stream OutputStream()
        {
            return outputStream;
        }

        public IKeyValueCollection CustomValues()
        {
            return customValues;
        }

        /// <summary>
        /// Executes <see cref="IHttpResponse.OnDisposing"/> action.
        /// </summary>
        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();

            if (OnDisposing != null)
                OnDisposing();

            outputStream.Dispose();
        }
    }
}
