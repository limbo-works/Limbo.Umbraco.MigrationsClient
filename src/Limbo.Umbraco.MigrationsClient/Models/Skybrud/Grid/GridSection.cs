using System.Collections.Generic;
using System.Linq;
using Limbo.Umbraco.MigrationsClient.Parsers.Skybrud;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.Grid;

public class GridSection : JsonObjectBase {

    public string Name { get; }

    public GridDataModel Model { get; }

    public int Grid { get; }

    public IReadOnlyList<GridRow> Rows { get; }

    public bool HasRows => Rows.Count > 0;

    public GridRow? FirstRow => Rows.FirstOrDefault();

    public GridRow? LastRow => Rows.LastOrDefault();

    public GridSection(JObject json, GridDataModel grid, SkybrudGridDataParser parser) : base(json) {

        Model = grid;
        Grid = json.GetInt32("grid");
        Name = grid.Name;
        Rows = json.GetArray("rows", x => parser.ParseGridRow(x, this)) ?? [];

        // Update "PreviousRow" and "NextRow" properties
        for (int i = 1; i < Rows.Count; i++) {
            Rows[i - 1].NextRow = Rows[i];
            Rows[i].PreviousRow = Rows[i - 1];
        }

    }

}