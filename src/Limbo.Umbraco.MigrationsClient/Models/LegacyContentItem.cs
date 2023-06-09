using System.Diagnostics.CodeAnalysis;
using System;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models {

    public class LegacyContentItem : IJsonParsable<LegacyContentItem> {

        public JObject JObject { get; }

        public int Id { get; }

        public Guid Key { get; }

        public string Name { get; }

        private LegacyContentItem(JObject json) {

            JObject = json;

            Id = json.GetInt32("id");
            Key = json.GetGuid("key");
            Name = json.GetString("name")!;

        }

        [return: NotNullIfNotNull("json")]
        public static LegacyContentItem? Parse(JObject? json) {
            return json is null ? null : new LegacyContentItem(json);
        }

    }

}