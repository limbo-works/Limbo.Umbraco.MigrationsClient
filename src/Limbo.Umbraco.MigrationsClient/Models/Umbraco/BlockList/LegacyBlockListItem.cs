namespace Limbo.Umbraco.MigrationsClient.Models.Umbraco.BlockList;

public class LegacyBlockListItem {

    public GuidUdi ContentUdi { get; }

    public GuidUdi? SettingsUdi { get; }

    public LegacyBlockListPart Content { get; }

    public LegacyBlockListPart? Settings { get; }

    public LegacyBlockListItem(GuidUdi contentUdi, GuidUdi? settingsUdi, LegacyBlockListPart content, LegacyBlockListPart? settings) {
        ContentUdi = contentUdi;
        SettingsUdi = settingsUdi;
        Content = content;
        Settings = settings;
    }

}