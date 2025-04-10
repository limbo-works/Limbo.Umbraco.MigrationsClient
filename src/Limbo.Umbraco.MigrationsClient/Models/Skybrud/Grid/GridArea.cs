using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.Grid;

public class GridArea : LegacyObjectBase {

    [JsonIgnore]
    public GridDataModel Model => Section.Model;

    [JsonIgnore]
    public GridSection Section => Row.Section;

    [JsonIgnore]
    public GridRow Row { get; }

    public int Grid { get; }

    public bool AllowAll { get; }

    public IReadOnlyList<string> Allowed { get; }

    public IReadOnlyList<GridControl> Controls { get; }

    [JsonIgnore]
    public GridArea? PreviousArea { get; internal set; }

    [JsonIgnore]
    public GridArea? NextArea { get; internal set; }

    [JsonIgnore]
    public bool HasControls => Controls.Count > 0;

    [JsonIgnore]
    public GridControl? FirstControl => Controls.FirstOrDefault();

    [JsonIgnore]
    public GridControl? LastControl => Controls.LastOrDefault();

    public GridArea(JObject json, int grid, bool allowAll, IReadOnlyList<string> allowed, IReadOnlyList<GridControl> controls, GridRow row) : base(json) {
        Grid = grid;
        AllowAll = allowAll;
        Allowed = allowed;
        Controls = controls;
        Row = row;
    }

}