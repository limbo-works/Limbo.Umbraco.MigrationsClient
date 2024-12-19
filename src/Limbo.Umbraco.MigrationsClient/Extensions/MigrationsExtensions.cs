using Limbo.Umbraco.MigrationsClient.Models.Umbraco;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Extensions;

public static class MigrationsExtensions {

    public static bool TryGetUdi(this JObject json, string propertyName, out GuidUdi? result) {
        string? str = json.GetString(propertyName);
        return GuidUdi.TryParse(str, out result);
    }

    public static bool TryGetUdiList(this JObject json, string propertyName, out GuidUdiList? result) {
        string? str = json.GetString(propertyName);
        return GuidUdiList.TryParse(str, out result);
    }

}