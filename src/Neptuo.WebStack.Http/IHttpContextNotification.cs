using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Events in HTTP request processing.
    /// </summary>
    public interface IHttpContextNotification
    {
        event Action OnHeadersSending;
        event Action OnHeadersSent;

        event Action OnOutputFlushing;
        event Action OnOutputFlushed;

        event Action OnDisposing;
        event Action OnDisposed;
    }
}
