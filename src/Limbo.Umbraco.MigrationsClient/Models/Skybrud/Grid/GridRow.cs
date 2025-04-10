using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.Grid;

public class GridRow : LegacyObjectBase {

    [JsonIgnore]
    public GridSection Section { get; }

    public string Id { get; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? Label { get; }

    [JsonIgnore]
    public bool HasLabel => string.IsNullOrWhiteSpace(Label) == false;

    public string Name { get; }

    public IReadOnlyList<GridArea> Areas { get; }

    [JsonIgnore]
    public GridRow? PreviousRow { get; internal set; }

    [JsonIgnore]
    public GridRow? NextRow { get; internal set; }

    [JsonIgnore]
    public bool HasAreas => Areas.Count > 0;

    [JsonIgnore]
    public GridArea? FirstArea => Areas.FirstOrDefault();

    [JsonIgnore]
    public GridArea? LastArea => Areas.LastOrDefault();

    public GridRow(JObject json, string id, string? label, string name, IReadOnlyList<GridArea> areas, GridSection section) : base(json) {
        Id = id;
        Label = label;
        Name = name;
        Areas = areas;
        Section = section;
    }

}