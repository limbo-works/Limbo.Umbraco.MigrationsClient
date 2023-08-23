using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Content {

    public class LegacyContentItem : ILegacyContentItem, IJsonParsable<LegacyContentItem> {

        public JObject JObject { get; }

        public int Id { get; }

        public Guid Key { get; }

        public string Name { get; }

        public IReadOnlyList<ILegacyContentItem> Children { get; }

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