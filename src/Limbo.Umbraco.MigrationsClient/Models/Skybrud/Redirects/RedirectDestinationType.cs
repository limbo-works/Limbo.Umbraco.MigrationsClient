using Newtonsoft.Json;
using Skybrud.Essentials.Json.Newtonsoft.Converters.Enums;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.Redirects;

[JsonConverter(typeof(EnumStringConverter))]
public enum RedirectDestinationType {

    Url,

    Content,

    Media

}