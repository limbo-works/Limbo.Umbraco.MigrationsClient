namespace Limbo.Umbraco.MigrationsClient.Models.Umbraco.Grid;

public class LegacyGridEditor {

    public string Name { get; }

    public string? NameTemplate { get; }

    public string Alias { get; }

    public string View { get; }

    public string? Render { get; }

    public string Icon { get; }

    public object? Config { get; }

    public LegacyGridEditor(string name, string? nameTemplate, string alias, string view, string? render, string icon, object? config) {
        Name = name;
        NameTemplate = nameTemplate;
        Alias = alias;
        View = view;
        Render = render;
        Icon = icon;
        Config = config;
    }

}

public class LegacyGridEditor<TConfig> : LegacyGridEditor {

    public new TConfig Config { get; }

    public LegacyGridEditor(string name, string? nameTemplate, string alias, string view, string? render, string icon, TConfig config) : base(name, nameTemplate, alias, view, render, icon, config) {
        Config = config;
    }

}