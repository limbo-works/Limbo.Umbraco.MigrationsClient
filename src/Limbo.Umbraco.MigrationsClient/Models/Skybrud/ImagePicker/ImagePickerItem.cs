using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.ImagePicker {

    public class ImagePickerItem : JsonObjectBase {

        public string? Title { get; set; }

        public string? Description { get; set; }

        public int ImageId { get; set; }

        public JToken? Link { get; set; }

        public string? ImageUrl { get; set; }

        public ImagePickerItem() : base(null) { }

        public ImagePickerItem(JObject json) : base(json) {
            if (json is null) throw new ArgumentNullException(nameof(json));
            Title = json.GetString("title");
            Description = json.GetString("description");
            ImageId = json.GetInt32("imageId");
            Link = json.GetValue("link");
            ImageUrl = json.GetString("imageUrl");
        }

        [return: NotNullIfNotNull("json")]
        public static ImagePickerItem? Parse(JObject? json) {
            return json is null ? null : new ImagePickerItem(json);
        }

    }

}