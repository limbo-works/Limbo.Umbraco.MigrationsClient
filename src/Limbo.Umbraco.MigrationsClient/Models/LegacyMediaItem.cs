using System.Diagnostics.CodeAnalysis;
using System;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models {

    public class LegacyMediaItem : IJsonParsable<LegacyMediaItem> {

        public JObject JObject { get; }

        public int Id { get; }

        public Guid Key { get; }

        public string Name { get; }

        private LegacyMediaItem(JObject json) {

            JObject = json;

            Id = json.GetInt32("id");
            Key = json.GetGuid("key");
            Name = json.GetString("name")!;

        }

        [return: NotNullIfNotNull("json")]
        public static LegacyMediaItem? Parse(JObject? json) {
            return json is null ? null : new LegacyMediaItem(json);
        }

    }

}