using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Terratype;

/// <summary>
/// Class representing a legacy Terratype model.
/// </summary>
/// <see>
///     <cref>https://github.com/Joniff/Terratype</cref>
/// </see>
public class TerratypeModel : JsonObjectBase {

    public int? Zoom { get; }

    public TerratypePosition Position { get; }

    public string? Lookup { get; set; }

    private TerratypeModel(JObject json) : base(json) {
        Zoom = json.GetInt32OrNull("zoom");
        Position = json.GetObject("position", TerratypePosition.Parse)!;
        Lookup = json.GetString("lookup");
    }

    public static TerratypeModel Parse(JObject json) {
        return new TerratypeModel(json);
    }

}