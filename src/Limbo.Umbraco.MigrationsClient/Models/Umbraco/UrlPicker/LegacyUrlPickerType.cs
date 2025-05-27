using Newtonsoft.Json;
using Skybrud.Essentials.Json.Newtonsoft.Converters.Enums;

namespace Limbo.Umbraco.MigrationsClient.Models.Umbraco.UrlPicker;

[JsonConverter(typeof(EnumCamelCaseConverter))]
public enum LegacyUrlPickerType {
    Content,
    Media,
    External
}