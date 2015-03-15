using Neptuo.WebStack.Formatters.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Formatters
{
    /// <summary>
    /// Some extensions for <see cref="EngineEnvironment"/>.
    /// </summary>
    public static class _EnvironmentExtensions
    {
        /// <summary>
        /// Registers serializer and deserializer collection and uses <paramref name="mapper"/> to register serializers and deserializers.
        /// These collections will be mapped both to collection (<see cref="ISerializerCollection"/> and <see cref="IDeserializerCollection"/>) and
        /// fomatter itself (<see cref="ISerializer"/> and <see cref="ISerializer"/>).
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <param name="mapper">Mapper function for registering serializers and deserializers.</param>
        /// <returns><paramref name="environment"/>.</returns>
        public static EngineEnvironment UseFormatters(this EngineEnvironment environment, Action<ISerializerCollection, IDeserializerCollection> mapper)
        {
            Ensure.NotNull(environment, "environment");
            Ensure.NotNull(mapper, "mapper");

            DefaultFomatterCollection collection = new DefaultFomatterCollection();
            mapper(collection, collection);
            environment.Use<ISerializer>(collection);
            environment.Use<ISerializerCollection>(collection);
            environment.Use<IDeserializer>(collection);
            environment.Use<IDeserializerCollection>(collection);
            return environment;
        }

        /// <summary>
        /// Tries to retrieve <see cref="ISerializer"/> from <paramref name="environment"/>.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <returns>Registered <see cref="ISerializer"/>.</returns>
        public static ISerializer WithSerializer(this EngineEnvironment environment)
        {
            Ensure.NotNull(environment, "environment");
            return environment.With<ISerializer>();
        }

        /// <summary>
        /// Tries to retrieve <see cref="IDeserializer"/> from <paramref name="environment"/>.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <returns>Registered <see cref="IDeserializer"/>.</returns>
        public static IDeserializer WithDeserializer(this EngineEnvironment environment)
        {
            Ensure.NotNull(environment, "environment");
            return environment.With<IDeserializer>();
        }
    }
}
