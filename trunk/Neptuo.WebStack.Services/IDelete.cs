using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Services
{
    /// <summary>
    /// Handler for Http DELETE requests.
    /// </summary>
    public interface IDelete
    {
        /// <summary>
        /// Invoked on Http DELETE request.
        /// </summary>
        Task ExecuteAsync();
    }
}
