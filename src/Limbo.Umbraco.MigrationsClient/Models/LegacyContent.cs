using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Diagnostics.CodeAnalysis;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models {

    public class LegacyContent : LegacyEntity, ILegacyContent<LegacyContent> {

        public IReadOnlyList<LegacyContentItem> Path { get; }

        public IReadOnlyList<LegacyContentItem> Children { get; }

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