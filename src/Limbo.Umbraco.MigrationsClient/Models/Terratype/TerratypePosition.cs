using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Essentials.Json.Newtonsoft;
using Skybrud.Essentials.Strings.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Terratype;

/// <summary>
/// Class representing a legacy Terratype position.
/// </summary>
/// <see>
///     <cref>https://github.com/Joniff/Terratype</cref>
/// </see>
public class TerratypePosition : JsonObjectBase {

    public string Id { get; }

    public string Datum { get; }

    public double Latitude { get; }

    public double Longitude { get; }

    private TerratypePosition(JObject json) : base(json) {
        Id = json.GetString("id")!;
        Datum = json.GetString("datum")!;
        Latitude = Datum.Split(',')[0].ToDouble();
        Longitude = Datum.Split(',')[1].ToDouble();

    }

    public static TerratypePosition Parse(JObject json) {
        return new TerratypePosition(json);
    }

}