using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;
using Skybrud.Essentials.Strings.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.DataTypes;

public class LegacyDataEditor : LegacyObjectBase, IJsonParsable<LegacyDataEditor> {

    public string Alias { get; }

    public string Name { get; }

    public string Icon { get; }

    public string? Group { get; }

    public string Type { get; }

    public bool IsDeprecated { get; }

    private LegacyDataEditor(JObject json) : base(json) {
        Alias = json.GetString("alias")!;
        Name = json.GetString("name")!;
        Icon = json.GetString("icon")!;
        Group = json.GetString("group").NullIfWhiteSpace();
        Type = json.GetString("type")!;
        IsDeprecated = json.GetBoolean("deprecated");
    }

    public static LegacyDataEditor Parse(JObject json) {
        return new LegacyDataEditor(json);
    }

}