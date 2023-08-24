using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Archetype {

    public class ArchetypePropertyModel {

        public string Alias { get; }

        public JToken? Value { get; }

        private ArchetypePropertyModel(JObject json) {
            Alias = json.GetString("alias")!;
            Value = json.GetValue("value");
        }

        [return: NotNullIfNotNull("json")]
        public static ArchetypePropertyModel? Parse(JObject? json) {
            return json is null ? null : new ArchetypePropertyModel(json);
        }

    }

}