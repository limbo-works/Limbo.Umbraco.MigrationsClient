using Limbo.Umbraco.MigrationsClient.Models.Properties;
using Newtonsoft.Json.Linq;

namespace Limbo.Umbraco.MigrationsClient.Models.Umbraco.NestedContent {

    public class NestedContentProperty : ILegacyProperty {

        public string Alias { get; }

        public string EditorAlias { get; }

        public JToken Value { get; }

        public NestedContentProperty(string alias, string editorAlias, JToken value) {
            Alias = alias;
            EditorAlias = editorAlias;
            Value = value;
        }

    }

}