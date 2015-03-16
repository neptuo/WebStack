using Neptuo.Compilers;
using Neptuo.ComponentModel.Behaviors;
using Neptuo.ComponentModel.Behaviors.Providers;
using Neptuo.ComponentModel.Behaviors.Processing.Compilation;
using Neptuo.WebStack.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neptuo.WebStack.Services.Hosting.Behaviors;

namespace Neptuo.WebStack.Services.Hosting
{
    /// <summary>
    /// WebStack.Services extensions for <see cref="EngineEnvironment"/>.
    /// </summary>
    public static class _EnvironmentExtensions
    {
        public class WebServiceEngineEnvironment
        {
            public EngineEnvironment Environment { get; private set; }

            public WebServiceEngineEnvironment(EngineEnvironment environment)
            {
                Ensure.NotNull(environment, "environment");
                Environment = environment;
            }
        }

        /// <summary>
        /// Registers app services.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        public static WebServiceEngineEnvironment UseWebServices(this EngineEnvironment environment)
        {
            Ensure.NotNull(environment, "environment");
            return new WebServiceEngineEnvironment(environment);
        }

        /// <summary>
        /// Registers singleton behaviors collection.
        /// </summary>
        /// <param name="webService">Engine environment.</param>
        /// <param name="behaviors">Behaviors collection.</param>
        /// <returns><paramref name="webService"/>.</returns>
        public static WebServiceEngineEnvironment UseBehaviors(this WebServiceEngineEnvironment webService, IBehaviorCollection behaviors)
        {
            Ensure.NotNull(webService, "webService");
            webService.Environment.Use<IBehaviorCollection>(behaviors, "WebService.Behaviors");
            return webService;
        }

        /// <summary>
        /// Registers behaviors collection.
        /// </summary>
        /// <param name="webService">Engine environment.</param>
        /// <param name="providers">List of behavior providers to add.</param>
        /// <returns><paramref name="webService"/>.</returns>
        public static WebServiceEngineEnvironment UseBehaviors(this WebServiceEngineEnvironment webService, params IBehaviorProvider[] providers)
        {
            Ensure.NotNull(webService, "webService");
            Ensure.NotNull(webService, "environment");
            Ensure.NotNull(providers, "providers");

            IBehaviorCollection collection = new BehaviorProviderCollection();
            foreach (IBehaviorProvider provider in providers)
                collection.Add(provider);

            return webService.UseBehaviors(collection);
        }

        /// <summary>
        /// Registers behaviors collection, add enpoint behaviors and invokes <paramref name="mapper"/> to map interface behaviors.
        /// </summary>
        /// <param name="webService">Engine environment.</param>
        /// <param name="mapper">Interface behavior mapper.</param>
        /// <returns><paramref name="webService"/>.</returns>
        public static WebServiceEngineEnvironment UseBehaviors(this WebServiceEngineEnvironment webService, Action<InterfaceBehaviorProvider> mapper)
        {
            Ensure.NotNull(webService, "webService");
            Ensure.NotNull(mapper, "mapper");
            
            InterfaceBehaviorProvider provider = new InterfaceBehaviorProvider();
            mapper(provider);

            provider
                .AddMapping<IGet, GetBehavior>()
                .AddMapping<IPost, PostBehavior>()
                .AddMapping<IPut, PutBehavior>()
                .AddMapping<IDelete, DeleteBehavior>();

            return webService.UseBehaviors(provider);
        }

        /// <summary>
        /// Returns registration of app services.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        public static WebServiceEngineEnvironment WithWebServices(this EngineEnvironment environment)
        {
            Ensure.NotNull(environment, "environment");
            return new WebServiceEngineEnvironment(environment);
        }

        /// <summary>
        /// Tries to retrieve behaviors collection.
        /// </summary>
        /// <param name="webService">Engine environment.</param>
        /// <returns>Registered behaviors collection.</returns>
        public static IBehaviorCollection WithBehaviors(this WebServiceEngineEnvironment webService)
        {
            Ensure.NotNull(webService, "webService");
            return webService.Environment.With<IBehaviorCollection>("WebService.Behaviors");
        }

        /// <summary>
        /// Registers singleton code dom pipeline configuration.
        /// </summary>
        /// <param name="webService">Engine environment.</param>
        /// <param name="configuration">Code dom pipeline configuration.</param>
        /// <returns><paramref name="webService"/>.</returns>
        public static WebServiceEngineEnvironment UseCodeDomConfiguration(this WebServiceEngineEnvironment webService, ICompilerConfiguration configuration)
        {
            Ensure.NotNull(webService, "webService");
            webService.Environment.Use<ICompilerConfiguration>(configuration, "WebService.CodeDomConfiguration");
            return webService;
        }

        /// <summary>
        /// Registers singleton code dom pipeline configuration.
        /// </summary>
        /// <param name="webService">Engine environment.</param>
        /// <param name="baseType">Custom base type (extending <see cref="DefaultPipelineBase{T}"/>).</param>
        /// <param name="tempDirectory">Path to temp directory.</param>
        /// <param name="binDirectories">List of bin directories to add as references.</param>
        /// <returns><paramref name="webService"/>.</returns>
        public static WebServiceEngineEnvironment UseCodeDomConfiguration(this WebServiceEngineEnvironment webService, Type baseType, string tempDirectory, params string[] binDirectories)
        {
            ICompilerConfiguration configuration = new CompilerConfiguration()
                .BaseType(baseType)
                .TempDirectory(tempDirectory);

            configuration
                .References()
                .AddDirectories(binDirectories);

            return UseCodeDomConfiguration(webService, configuration);
        }

        /// <summary>
        /// Registers singleton code dom pipeline configuration.
        /// </summary>
        /// <param name="webService">Engine environment.</param>
        /// <param name="mapper">Configuration initializer.</param>
        public static WebServiceEngineEnvironment UseCodeDomConfiguration(this WebServiceEngineEnvironment webService, Action<ICompilerConfiguration> mapper)
        {
            Ensure.NotNull(mapper, "mapper");

            ICompilerConfiguration configuration = new CompilerConfiguration();
            mapper(configuration);

            return UseCodeDomConfiguration(webService, configuration);
        }

        /// <summary>
        /// Tries to retrieve code dom pipeline configuration.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <returns>Registered code dom pipeline configuration.</returns>
        public static ICompilerConfiguration WithCodeDomConfiguration(this WebServiceEngineEnvironment webService)
        {
            return webService.Environment.With<ICompilerConfiguration>("WebService.CodeDomConfiguration");
        }
    }
}
