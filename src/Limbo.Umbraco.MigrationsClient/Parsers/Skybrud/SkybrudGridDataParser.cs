using System.Collections.Generic;
using Limbo.Umbraco.MigrationsClient.Exceptions;
using Limbo.Umbraco.MigrationsClient.Models;
using Limbo.Umbraco.MigrationsClient.Models.Properties;
using Limbo.Umbraco.MigrationsClient.Models.Skybrud.Grid;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Exceptions;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Parsers.Skybrud;

public class SkybrudGridDataParser {

    public virtual GridDataModel ParseGridModel(JObject json) {
        return ParseGridModel(json, null, null);
    }

    public virtual GridDataModel ParseGridModel(JObject json, ILegacyElement? owner, ILegacyProperty? property) {

        string name = json.GetRequiredString("name");
        List<GridSection> sections = [];

        GridDataModel model = new(json, name, sections);

        foreach (JObject section in json.GetObjectArray("sections")) {
            if (ParseGridSection(section, model, owner, property) is {} result) sections.Add(result);
        }

        return model;

    }

    public virtual GridSection? ParseGridSection(JObject json, GridDataModel model, ILegacyElement? content, ILegacyProperty? property) {

        int grid = json.GetInt32("grid");
        string name = model.Name; // WTF???
        List<GridRow> rows = [];

        // Initialize a new section
        GridSection section = new(json, grid, name, rows, model);

        // Parse the rows in the section
        foreach (JObject row in json.GetObjectArray("rows")) {
            if (ParseGridRow(row, section, content, property) is {} result) rows.Add(result);
        }

        // Update "PreviousRow" and "NextRow" properties
        for (int i = 1; i < rows.Count; i++) {
            rows[i - 1].NextRow = rows[i];
            rows[i].PreviousRow = rows[i - 1];
        }

        // Return the section
        return section;

    }

    public virtual GridRow? ParseGridRow(JObject json, GridSection section, ILegacyElement? content, ILegacyProperty? property) {

        // Parse the properties of the row
        string id = json.GetRequiredString("id");
        string? label = json.GetString("label");
        string name = json.GetRequiredString("name");
        List<GridArea> areas = [];

        // Initialize a new row
        GridRow row = new(json, id, label, name, areas, section);

        // Parse the areas in the row
        foreach (JObject area in json.GetObjectArray("areas")) {
            if (ParseGridArea(area, row, content, property) is { } result) areas.Add(result);
        }

        // Update "PreviousArea" and "NextArea" properties
        for (int i = 1; i < areas.Count; i++) {
            areas[i - 1].NextArea = areas[i];
            areas[i].PreviousArea = areas[i - 1];
        }

        // Return the row
        return row;

    }

    public virtual GridArea? ParseGridArea(JObject json, GridRow row, ILegacyElement? content, ILegacyProperty? property) {

        // Parse the properties of the area
        int grid = json.GetInt32("grid");
        bool allowAll = json.GetBoolean("allowAll");
        IReadOnlyList<string> allowed = json.GetStringArray("allowed");
        List<GridControl> controls = [];

        // Initialize a new area
        GridArea area = new(json, grid, allowAll, allowed, controls, row);

        // Parse the controls in the area
        foreach (JObject control in json.GetObjectArray("controls")) {
            if (ParseGridControl(control, area, content, property) is { } result) controls.Add(result);
        }

        // Update "PreviousControl" and "NextControl" properties
        for (int i = 1; i < controls.Count; i++) {
            controls[i - 1].NextControl = controls[i];
            controls[i].PreviousControl = controls[i - 1];
        }

        // Return the area
        return area;

    }

    public virtual GridControl? ParseGridControl(JObject json, GridArea area, ILegacyElement? content, ILegacyProperty? property) {

        // Parse the properties of the control
        JToken value = json.GetValue("value");
        GridEditor editor = ParseGridEditor(json.GetRequiredObject("editor"), content, property);

        // Initialize and return a new control
        return new GridControl(json, value, area, editor);

    }

    public virtual GridEditor ParseGridEditor(JObject json, ILegacyElement? content, ILegacyProperty? property) {

        try {

            // Parse the properties of the editor
            string? name = json.GetString("name");
            string? nameTemplate = json.GetString("nameTemplate");
            string alias = json.GetRequiredString("alias");
            string? view = json.GetString("view");
            string? render = json.GetString("render");
            string? icon = json.GetString("icon");
            JToken config = json.GetValue("config");

            // Initialize and return a new editor
            return new GridEditor(json, name, nameTemplate, alias, view, render, icon, config);

        } catch (JsonPropertyNotFoundException ex) {

            throw new MigrationsParseExcetion($"Failed parsing grid editor.\r\n\r\n{json}", ex);

        }

    }

}