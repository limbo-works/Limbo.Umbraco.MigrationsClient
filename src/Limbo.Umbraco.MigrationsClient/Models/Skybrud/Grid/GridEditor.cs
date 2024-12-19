using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.Grid;

public class GridEditor : LegacyObjectBase {

    public string? Name { get; }

    public string Alias { get; }

    public string? View { get; }

    public string? Render { get; }

    public string? Icon { get; }

    public JToken Config { get; internal set; }

    public GridEditor(JObject json) : base(json) {
        Name = json.GetString("name")!;
        Alias = json.GetString("alias")!;
        View = json.GetString("view")!;
        Render = json.GetString("render");
        Icon = json.GetString("icon")!;
        Config = json.GetValue("config");
    }

    public GridEditor(GridEditor editor) : base(editor.JObject) {
        Name = editor.Name;
        Alias = editor.Alias;
        View = editor.View;
        Render = editor.Render;
        Icon = editor.Icon;
        Config = editor.Config;
    }

}

public class GridEditor<TConfig> : GridEditor {

    public new TConfig Config { get; }

    public GridEditor(GridEditor editor, TConfig config) : base(editor) {
        Config = config;
    }

}