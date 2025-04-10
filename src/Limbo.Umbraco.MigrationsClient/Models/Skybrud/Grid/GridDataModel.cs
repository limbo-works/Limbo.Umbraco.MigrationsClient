using System.Collections.Generic;
using Newtonsoft.Json.Linq;

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

    public GridDataModel(JObject json, string name, IReadOnlyList<GridSection> sections) : base(json) {
        Name = name;
        Sections = sections;
    }

}