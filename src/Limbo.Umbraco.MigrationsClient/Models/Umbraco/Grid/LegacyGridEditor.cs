using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Exceptions;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

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

    public static LegacyGridEditor Parse(JObject json) {


        string name = json.GetRequiredString("name");
        string? nameTemplate = json.GetString("nameTemplate");
        string alias = json.GetRequiredString("alias");
        string view = json.GetRequiredString("view");
        string? render = json.GetString("render");
        string icon = json.GetRequiredString("icon");

        JToken? config = json.GetValue("config");

        return config switch {
            null => new LegacyGridEditor(name, nameTemplate, alias, view, render, icon, config),
            JObject obj => new LegacyGridEditor<JObject>(name, nameTemplate, alias, view, render, icon, obj),
            JArray array => new LegacyGridEditor<JArray>(name, nameTemplate, alias, view, render, icon, array),
            _ => new LegacyGridEditor<JToken>(name, nameTemplate, alias, view, render, icon, config)
        };

    }

}

public class LegacyGridEditor<TConfig> : LegacyGridEditor {

    public new TConfig Config { get; }

    public LegacyGridEditor(string name, string? nameTemplate, string alias, string view, string? render, string icon, TConfig config) : base(name, nameTemplate, alias, view, render, icon, config) {
        Config = config;
    }

}