using System.Collections.Generic;
using Limbo.Umbraco.MigrationsClient.Parsers.Skybrud;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.Grid;

public class GridDataModel : LegacyObjectBase {

    /// <summary>
    /// Gets the name of the selected layout.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets an array of the columns in the grid.
    /// </summary>
    public IReadOnlyList<GridSection> Sections { get; }

    public GridDataModel(JObject json, SkybrudGridDataParser parser) : base(json) {
        Name = json.GetString("name")!;
       Sections = json.GetArray("sections", x => parser.ParseGridSection(x, this)) ?? [];
    }

}