using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Umbraco;

public class MultiUrlPickerItem : LegacyObjectBase {

    public string Name { get; }

    public GuidUdi? Udi { get; }

    public string? Target { get; }

    public string? Url { get; }

    public string? QueryString { get; }

    private MultiUrlPickerItem(JObject json) : base(json) {
        Name = json.GetString("name")!;
        Udi = json.GetString("udi", GuidUdi.Parse);
        Target = json.GetString("target");
        Url = json.GetString("url");
        QueryString = json.GetString("queryString");
    }

    [return: NotNullIfNotNull(nameof(json))]
    public static MultiUrlPickerItem? Parse(JObject? json) {
        return json is null ? null : new MultiUrlPickerItem(json);
    }

}