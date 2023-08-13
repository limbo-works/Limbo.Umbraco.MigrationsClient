using System.Diagnostics.CodeAnalysis;
using System;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;
using System.Collections.Generic;

namespace Limbo.Umbraco.MigrationsClient.Models {

    public class LegacyContentItem : IJsonParsable<LegacyContentItem> {

        public JObject JObject { get; }

        public int Id { get; }

        public Guid Key { get; }

        public string Name { get; }

        public IReadOnlyList<LegacyContentItem> Children { get; }

        private LegacyContentItem(JObject json) {

            JObject = json;

            Id = json.GetInt32("id");
            Key = json.GetGuid("key");
            Name = json.GetString("name")!;
            Children = json.GetArrayItems("children", Parse);

        }

        [return: NotNullIfNotNull("json")]
        public static LegacyContentItem? Parse(JObject? json) {
            return json is null ? null : new LegacyContentItem(json);
        }

    }

}