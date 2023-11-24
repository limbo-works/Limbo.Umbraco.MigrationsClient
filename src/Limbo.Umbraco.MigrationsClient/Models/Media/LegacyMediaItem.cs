using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Media {

    public class LegacyMediaItem : ILegacyMediaItem, IJsonParsable<LegacyMediaItem> {

        public JObject JObject { get; }

        public int Id { get; }

        public Guid Key { get; }

        public string ContentTypeAlias { get; }

        public string Name { get; }

        public string Url { get; }

        public IReadOnlyList<ILegacyMediaItem> Children { get; }

        private LegacyMediaItem(JObject json) {

            JObject = json;

            Id = json.GetInt32("id");
            Key = json.GetGuid("key");
            Name = json.GetString("name")!;
            Url = json.GetString("url")!;
            ContentTypeAlias = json.GetString("type")!;
            Children = json.GetArrayItems("children", Parse)!;

        }

        [return: NotNullIfNotNull("json")]
        public static LegacyMediaItem? Parse(JObject? json) {
            return json is null ? null : new LegacyMediaItem(json);
        }

    }

}