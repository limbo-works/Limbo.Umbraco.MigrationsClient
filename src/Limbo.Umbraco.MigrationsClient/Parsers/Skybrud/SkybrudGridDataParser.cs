using Limbo.Umbraco.MigrationsClient.Models;
using Limbo.Umbraco.MigrationsClient.Models.Properties;
using Limbo.Umbraco.MigrationsClient.Models.Skybrud.Grid;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Parsers.Skybrud;

public class SkybrudGridDataParser {

    public virtual GridDataModel ParseGridModel(JObject json, ILegacyElement? content, ILegacyProperty? property) {
        return new GridDataModel(json, this);
    }

    public virtual GridSection ParseGridSection(JObject json, GridDataModel grid) {
        return new GridSection(json, grid, this);
    }

    public virtual GridRow ParseGridRow(JObject json, GridSection section) {
        return new GridRow(json, section, this);
    }

    public virtual GridArea ParseGridArea(JObject json, GridRow row) {
        return new GridArea(json, row, this);
    }

    public virtual GridControl ParseGridControl(JObject json, GridArea area) {
        GridEditor editor = json.GetObject("editor", ParseGridEditor)!;
        return new GridControl(json, area, editor);
    }

    public virtual GridEditor ParseGridEditor(JObject json) {
        return new GridEditor(json);
    }

}