using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HttpWebContext = System.Web.HttpContext;

namespace Neptuo.WebStack.Http
{
    public class AspNetContextNotification : IHttpContextNotification
    {
        private readonly HttpWebContext webContext;
        private bool isHeadersSent;
        private bool isOutputFlushed;
        private bool isDisposed;

        public event Action OnHeadersSending;
        public event Action OnHeadersSent;

        public event Action OnOutputFlushing;
        public event Action OnOutputFlushed;

        public event Action OnDisposing;
        public event Action OnDisposed;

        public AspNetContextNotification(HttpWebContext webContext)
        {
            Guard.NotNull(webContext, "webContext");
            this.webContext = webContext;
        }

        public IDisposable HeadersSend()
        {
            if (isHeadersSent)
                throw Guard.Exception.InvalidOperation("Headers already sent.");

            isHeadersSent = true;
            return new EventDisposable(OnHeadersSending, OnHeadersSent);
        }

        public IDisposable OutputFlush()
        {
            if (isOutputFlushed)
                throw Guard.Exception.InvalidOperation("Output already flushed.");

            isOutputFlushed = true;
            return new EventDisposable(OnOutputFlushing, OnOutputFlushed);
        }

        public IDisposable Dispose()
        {
            if (isDisposed)
                throw Guard.Exception.InvalidOperation("Already disposed.");

            isDisposed = true;
            return new EventDisposable(OnDisposing, OnDisposed);
        }

        private class EventDisposable : DisposableBase
        {
            private readonly Action after;

            public EventDisposable(Action before, Action after)
            {
                if (before != null)
                    before();

                this.after = after;
            }

            protected override void DisposeManagedResources()
            {
                base.DisposeManagedResources();

                if (after != null)
                    after();
            }
        }
    }
}
