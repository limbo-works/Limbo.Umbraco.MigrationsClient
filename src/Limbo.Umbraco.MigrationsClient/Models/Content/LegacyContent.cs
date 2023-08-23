using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Content {

    public class LegacyContent : LegacyEntity, ILegacyContent<LegacyContent> {

        public IReadOnlyList<ILegacyContentItem> Path { get; }

        public IReadOnlyList<ILegacyContentItem> Children { get; }

        private LegacyContent(JObject json) : base(json) {
            Path = json.GetArrayItems("path", LegacyContentItem.Parse);
            Children = json.GetArrayItems("children", LegacyContentItem.Parse);
        }

        [return: NotNullIfNotNull("json")]
        public static LegacyContent? Parse(JObject? json) {
            return json == null ? null : new LegacyContent(json);
        }

    }

}