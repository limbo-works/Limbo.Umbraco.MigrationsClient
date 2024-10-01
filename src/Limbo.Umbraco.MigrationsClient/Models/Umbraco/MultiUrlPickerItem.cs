using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Umbraco;

public class MultiUrlPickerItem {

    public string Name { get; }

    public string? Udi { get; }

    public string? Target { get; }

    public string? Url { get; }

    private MultiUrlPickerItem(JObject json) {
        Name = json.GetString("name")!;
        Udi = json.GetString("udi");
        Target = json.GetString("target");
        Url = json.GetString("url");
    }

    [return: NotNullIfNotNull(nameof(json))]
    public static MultiUrlPickerItem? Parse(JObject? json) {
        return json is null ? null : new MultiUrlPickerItem(json);
    }

}