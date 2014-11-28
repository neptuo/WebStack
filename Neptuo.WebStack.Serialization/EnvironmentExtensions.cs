using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Serialization
{
    /// <summary>
    /// Some extensions for <see cref="EngineEnvironment"/>.
    /// </summary>
    public static class EnvironmentExtensions
    {
        /// <summary>
        /// Registers serializer and deserializer collection and uses <paramref name="mapper"/> to register serializers and deserializers.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <param name="mapper">Mapper function for registering serializers and deserializers.</param>
        /// <returns><paramref name="environment"/>.</returns>
        public static EngineEnvironment UseSerialization(this EngineEnvironment environment, Action<ISerializerCollection, IDeserializerCollection> mapper)
        {
            Guard.NotNull(environment, "environment");
            Guard.NotNull(mapper, "mapper");
            return environment;
        }

        /// <summary>
        /// Tries to retrieve <see cref="ISerializerCollection"/> from <paramref name="environment"/>.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <returns>Registered <see cref="ISerializerCollection"/>.</returns>
        public static ISerializerCollection WithSerializerCollection(this EngineEnvironment environment)
        {
            Guard.NotNull(environment, "environment");
            return environment.With<ISerializerCollection>();
        }

        /// <summary>
        /// Tries to retrieve <see cref="IDeserializerCollection"/> from <paramref name="environment"/>.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <returns>Registered <see cref="IDeserializerCollection"/>.</returns>
        public static IDeserializerCollection WithDeserializerCollection(this EngineEnvironment environment)
        {
            Guard.NotNull(environment, "environment");
            return environment.With<IDeserializerCollection>();
        }
    }
}
