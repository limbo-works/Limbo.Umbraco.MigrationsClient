using System.Collections.Generic;
using System.Linq;
using Limbo.Umbraco.MigrationsClient.Parsers.Skybrud;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.Grid;

public class GridArea : LegacyObjectBase {

    public GridDataModel Model => Section.Model;

    public GridSection Section => Row.Section;

    public GridRow Row { get; }

    public int Grid { get; }

    public bool AllowAll { get; }

    public IReadOnlyList<string> Allowed { get; }

    public IReadOnlyList<GridControl> Controls { get; }

    public GridArea? PreviousArea { get; internal set; }

    public GridArea? NextArea { get; internal set; }

    public bool HasControls => Controls.Count > 0;

    public GridControl? FirstControl => Controls.FirstOrDefault();

    public GridControl? LastControl => Controls.LastOrDefault();

    public GridArea(JObject json, GridRow row, SkybrudGridDataParser factory) : base(json) {

        Row = row;
        Grid = json.GetInt32("grid");
        AllowAll = json.GetBoolean("allowAll");
        Allowed = json.GetStringArray("allowed");
        Controls = json.GetArray("controls", x => factory.ParseGridControl(x, this)) ?? [];

        // Update "PreviousControl" and "NextControl" properties
        for (int i = 1; i < Controls.Count; i++) {
            Controls[i - 1].NextControl = Controls[i];
            Controls[i].PreviousControl = Controls[i - 1];
        }

    }

}