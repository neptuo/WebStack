﻿using Neptuo.WebStack.Routing;
using Neptuo.WebStack.Services.Hosting.Behaviors;
using Neptuo.WebStack.Services.Hosting.Behaviors.Providers;
using Neptuo.WebStack.Services.Hosting.Pipelines.Compilation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Services.Hosting
{
    /// <summary>
    /// WebStack.Services extensions for <see cref="EngineEnvironment"/>.
    /// </summary>
    public static class EnvironmentExtensions
    {
        /// <summary>
        /// Registers singleton behaviors collection.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <param name="behaviors">Behaviors collection.</param>
        /// <returns><paramref name="environment"/>.</returns>
        public static EngineEnvironment UseBehaviors(this EngineEnvironment environment, IBehaviorCollection behaviors)
        {
            Guard.NotNull(environment, "environment");
            return environment.Use<IBehaviorCollection>(behaviors);
        }

        /// <summary>
        /// Registers behaviors collection, add enpoint behaviors and add <paramref name="providers"/> into.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <param name="providers">List of behavior providers to add.</param>
        /// <returns><paramref name="environment"/>.</returns>
        public static EngineEnvironment UseBehaviors(this EngineEnvironment environment, params IBehaviorProvider[] providers)
        {
            Guard.NotNull(environment, "environment");
            Guard.NotNull(providers, "providers");

            IBehaviorCollection collection = new BehaviorCollectionBase()
                .AddEndpoints();

            foreach (IBehaviorProvider provider in providers)
                collection.Add(provider);

            return environment.UseBehaviors(collection);
        }

        /// <summary>
        /// Registers behaviors collection, add enpoint behaviors and invokes <paramref name="mapper"/> to map interface behaviors.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <param name="mapper">Interface behavior mapper.</param>
        /// <returns><paramref name="environment"/>.</returns>
        public static EngineEnvironment UseBehaviors(this EngineEnvironment environment, Action<InterfaceBehaviorProvider> mapper)
        {
            Guard.NotNull(environment, "environment");
            Guard.NotNull(mapper, "mapper");

            InterfaceBehaviorProvider provider = new InterfaceBehaviorProvider();
            mapper(provider);
            return environment.UseBehaviors(provider);
        }
        

        /// <summary>
        /// Tries to retrieve behaviors collection.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <returns>Registered behaviors collection.</returns>
        public static IBehaviorCollection WithBehaviors(this EngineEnvironment environment)
        {
            return environment.With<IBehaviorCollection>();
        }


        /// <summary>
        /// Registers singleton code dom pipeline configuration.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <param name="configuration">Code dom pipeline configuration.</param>
        /// <returns><paramref name="environment"/>.</returns>
        public static EngineEnvironment UseCodeDomConfiguration(this EngineEnvironment environment, CodeDomPipelineConfiguration configuration)
        {
            return environment.Use<CodeDomPipelineConfiguration>(configuration);
        }

        /// <summary>
        /// Tries to retrieve code dom pipeline configuration.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <returns>Registered code dom pipeline configuration.</returns>
        public static CodeDomPipelineConfiguration WithCodeDomConfiguration(this EngineEnvironment environment)
        {
            return environment.With<CodeDomPipelineConfiguration>();
        }
    }
}
