using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;
using Skybrud.Essentials.Strings.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Umbraco.UrlPicker;

public class LegacyUrlPickerItem : LegacyObjectBase {

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("target", NullValueHandling = NullValueHandling.Ignore)]
    public string? Target { get; set; }

    [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
    public LegacyUrlPickerType Type { get; set; }

    [JsonProperty("udi", NullValueHandling = NullValueHandling.Ignore)]
    public GuidUdi? Udi { get; set; }

    [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
    public string? Url { get; set; }

    [JsonProperty("queryString", NullValueHandling = NullValueHandling.Ignore)]
    public string? QueryString { get; set; }

    private LegacyUrlPickerItem(JObject json) : base(json) {
        Name = json.GetString("name") ?? string.Empty;
        Target = json.GetString("target").NullIfWhiteSpace();
        Udi = json.GetString("udi", GuidUdi.ParseOrNullIfEmpty);
        Url = json.GetString("url").NullIfWhiteSpace();
        QueryString = json.GetString("queryString").NullIfWhiteSpace();

        if (Udi is not null && Udi.EntityType == "document") {
            Type = LegacyUrlPickerType.Content;
        } else if (Udi is not null && Udi.EntityType == "media") {
            Type = LegacyUrlPickerType.Media;
        } else {
            Type = LegacyUrlPickerType.External;
        }
    }

    public static LegacyUrlPickerItem Parse(JObject json) {
        return new LegacyUrlPickerItem(json);
    }

}