using System.Collections.Generic;
using System.Linq;
using Limbo.Umbraco.MigrationsClient.Parsers.Skybrud;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.Grid;

public class GridRow : LegacyObjectBase {

    public GridSection Section { get; }

    public string Id { get; }

    public string? Label { get; }

    public bool HasLabel => string.IsNullOrWhiteSpace(Label) == false;

    public string Name { get; }

    public IReadOnlyList<GridArea> Areas { get; }

    public GridRow? PreviousRow { get; internal set; }

    public GridRow? NextRow { get; internal set; }

    public bool HasAreas => Areas.Count > 0;

    public GridArea? FirstArea => Areas.FirstOrDefault();

    public GridArea? LastArea => Areas.LastOrDefault();

    public GridRow(JObject json, GridSection section, SkybrudGridDataParser parser) : base(json) {

        Section = section;
        Id = json.GetString("id")!;
        Label = json.GetString("label");
        Name = json.GetString("name")!;

        Areas = json.GetArray("areas", x => parser.ParseGridArea(x, this)) ?? [];

        // Update "PreviousArea" and "NextArea" properties
        for (int i = 1; i < Areas.Count; i++) {
            Areas[i - 1].NextArea = Areas[i];
            Areas[i].PreviousArea = Areas[i - 1];
        }

    }

}