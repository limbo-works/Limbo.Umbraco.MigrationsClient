using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.VideoPicker;

public class VideoPickerList : LegacyObjectBase {

    public string? Title { get; }

    public string? Description { get; }

    public IReadOnlyList<VideoPickerItem> Items { get; }

    public VideoPickerList(JObject json) : base(json) {
        Title = json.GetString("title");
        Description = json.GetString("description");
        Items = json.GetArrayItems("items", VideoPickerItem.Parse)!;
    }

    [return: NotNullIfNotNull(nameof(json))]
    public static VideoPickerList? Parse(JObject? json) {
        return json is null ? null : new VideoPickerList(json);
    }

}