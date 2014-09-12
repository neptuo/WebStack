using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Services.Behaviors
{
    /// <summary>
    /// Provides access to Http response status.
    /// </summary>
    public interface IWithStatus
    {
        /// <summary>
        /// Http response status.
        /// </summary>
        HttpStatus Status { get; }
    }
}
