using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Media {

    public class LegacyMedia : LegacyEntity, ILegacyMedia, IJsonParsable<LegacyMedia> {

        public int Width { get; }

        public int Height { get; }

        public int Size { get; }

        public JToken? UmbracoFile { get; }

        public IReadOnlyList<ILegacyMediaItem> Path { get; }

        public IReadOnlyList<ILegacyMediaItem> Children { get; }

        private LegacyMedia(JObject json) : base(json) {

            Width = json.GetInt32("width");
            Height = json.GetInt32("height");
            Size = json.GetInt32("size");
            UmbracoFile = json.GetValue("umbracoFile");

            Path = json.GetArrayItems("path", LegacyMediaItem.Parse);
            Children = json.GetArrayItems("children", LegacyMediaItem.Parse);

        }

        [return: NotNullIfNotNull("json")]
        public static LegacyMedia? Parse(JObject? json) {
            return json == null ? null : new LegacyMedia(json);
        }

    }

}