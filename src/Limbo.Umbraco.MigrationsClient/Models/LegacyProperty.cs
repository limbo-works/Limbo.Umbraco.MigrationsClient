using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models {

    public class LegacyProperty : IJsonParsable<LegacyProperty> {

        public JObject JObject { get; }

        public string Alias { get; }

        public string EditorAlias { get; }

        public JToken Value { get; }

        private LegacyProperty(JObject json) {

            JObject = json;

            Alias = json.GetString("alias")!;
            EditorAlias = json.GetString("editorAlias")!;
            Value = json.GetValue("value");

        }

        [return: NotNullIfNotNull("json")]
        public static LegacyProperty? Parse(JObject? json) {
            return json is null ? null : new LegacyProperty(json);
        }

    }

}