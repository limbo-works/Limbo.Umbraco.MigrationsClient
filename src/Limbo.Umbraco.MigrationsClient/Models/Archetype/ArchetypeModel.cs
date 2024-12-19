using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Archetype;

public class ArchetypeModel : LegacyObjectBase {

    public IReadOnlyList<ArchetypeFieldsetModel> Fieldsets { get; }

    private ArchetypeModel(JObject json) : base(json) {
        Fieldsets = json.GetArrayItems("fieldsets", ArchetypeFieldsetModel.Parse);
    }

    [return: NotNullIfNotNull(nameof(json))]
    public static ArchetypeModel? Parse(JObject? json) {
        return json is null ? null : new ArchetypeModel(json);
    }

}