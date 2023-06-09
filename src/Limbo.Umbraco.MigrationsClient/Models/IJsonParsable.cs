using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;

namespace Limbo.Umbraco.MigrationsClient.Models {

    public interface IJsonParsable<out T> {

        #if NET7_0_OR_GREATER

        [return: NotNullIfNotNull(nameof(json))]
        static abstract T? Parse(JObject? json);

        #endif

    }

}