using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft;

namespace Limbo.Umbraco.MigrationsClient.Models;

public class LegacyObjectBase : JsonObjectBase {

    [JsonIgnore]
    public new JObject JObject => base.JObject!;

    protected LegacyObjectBase(JObject? json) : base(json) { }

}