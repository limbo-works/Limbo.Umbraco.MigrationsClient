using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Archetype {

    public class ArchetypeModel {

        public IReadOnlyList<ArchetypeFieldsetModel> Fieldsets { get; }

        private ArchetypeModel(JObject json) {
            Fieldsets = json.GetArrayItems("fieldsets", ArchetypeFieldsetModel.Parse);
        }

        [return: NotNullIfNotNull("json")]
        public static ArchetypeModel? Parse(JObject? json) {
            return json is null ? null : new ArchetypeModel(json);
        }

    }

}