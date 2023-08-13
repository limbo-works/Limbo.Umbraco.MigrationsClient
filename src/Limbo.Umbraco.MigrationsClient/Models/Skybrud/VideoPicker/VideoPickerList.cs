using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.VideoPicker {

    public class VideoPickerList {

        public string? Title { get; }

        public IReadOnlyList<VideoPickerItem> Items { get; }

        public VideoPickerList(JObject json) {
            Title = json.GetString("title");
            Items = json.GetArrayItems("items", VideoPickerItem.Parse)!;
        }

        [return: NotNullIfNotNull("json")]
        public static VideoPickerList? Parse(JObject? json) {
            return json is null ? null : new VideoPickerList(json);
        }

    }

}