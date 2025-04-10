using Newtonsoft.Json.Linq;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.Grid;

public class GridEditor : LegacyObjectBase {

    public string? Name { get; }

    public string? NameTemplate { get; }

    public string Alias { get; }

    public string? View { get; }

    public string? Render { get; }

    public string? Icon { get; }

    public JToken Config { get; internal set; }

    public GridEditor(JObject json, string? name, string? nameTemplate, string alias, string? view, string? render, string? icon, JToken config) : base(json) {
        Name = name;
        NameTemplate = nameTemplate;
        Alias = alias;
        View = view;
        Render = render;
        Icon = icon;
        Config = config;
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