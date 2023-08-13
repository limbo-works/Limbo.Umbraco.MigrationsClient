using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.VideoPicker {

    public class VideoPickerItem {

        public string? Url { get; }

        public string? Type { get; }

        public JObject? Details { get; }

        public int ThumbnailId { get; }

        public VideoPickerItem(JObject json) {
            Url = json.GetString("url");
            Type = json.GetString("type");
            Details = json.GetObject("details");
            ThumbnailId = json.GetInt32("thumbnailId");
        }

        [return: NotNullIfNotNull("json")]
        public static VideoPickerItem? Parse(JObject? json) {
            return json is null ? null : new VideoPickerItem(json);
        }

    }

}