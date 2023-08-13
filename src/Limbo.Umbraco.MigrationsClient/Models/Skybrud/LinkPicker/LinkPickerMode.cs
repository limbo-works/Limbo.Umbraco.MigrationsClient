using Newtonsoft.Json;
using Skybrud.Essentials.Json.Newtonsoft.Converters.Enums;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.LinkPicker {

    [JsonConverter(typeof(EnumStringConverter))]
    public enum LinkPickerMode {

        Url,

        Content,

        Media

    }

}