using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.Grid;

public class GridSection : LegacyObjectBase {

    public string Name { get; }

    [JsonIgnore]
    public GridDataModel Model { get; }

    public int Grid { get; }

    public IReadOnlyList<GridRow> Rows { get; }

    public bool HasRows => Rows.Count > 0;

    [JsonIgnore]
    public GridRow? FirstRow => Rows.FirstOrDefault();

    [JsonIgnore]
    public GridRow? LastRow => Rows.LastOrDefault();

    public GridSection(JObject json, int grid, string name, List<GridRow> rows, GridDataModel model) : base(json) {
        Grid = grid;
        Name = name;
        Rows = rows;
        Model = model;
    }

}