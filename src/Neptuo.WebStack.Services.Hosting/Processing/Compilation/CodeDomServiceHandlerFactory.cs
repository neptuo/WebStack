using Neptuo.Activators;
using Neptuo.Compilers;
using Neptuo.ComponentModel.Behaviors;
using Neptuo.ComponentModel.Behaviors.Processing.Compilation;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Services.Hosting.Processing
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler"/> for wrapping concrete type as compiled pipeline.
    /// </summary>
    public class CodeDomServiceHandlerFactory : IRequestHandler
    {
        private readonly IActivator<IRequestHandler> activator;

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="handlerType">Target handler to compile pipeline for.</param>
        /// <param name="behaviorCollection">Collection of behaviors mapping for handler.</param>
        /// <param name="compilerConfiguration">Compiler configuration.</param>
        public CodeDomServiceHandlerFactory(Type handlerType, IBehaviorCollection behaviorCollection, ICompilerConfiguration compilerConfiguration)
        {
            this.activator = new CodeDomPipelineFactory<IRequestHandler>(handlerType, behaviorCollection, compilerConfiguration);
        }

        /// <summary>
        /// Creates new instance for <paramref name="handlerType"/> with default configuration for behaviors and compilation.
        /// </summary>
        /// <param name="handlerType">Target handler to compile pipeline for.</param>
        public CodeDomServiceHandlerFactory(Type handlerType)
            : this(handlerType, Engine.Environment.WithWebServices().WithBehaviors(), Engine.Environment.WithWebServices().WithCodeDomConfiguration())
        { }

        public Task<bool> TryHandleAsync(IHttpContext httpContext)
        {
            return activator.Create().TryHandleAsync(httpContext);
        }
    }
}
