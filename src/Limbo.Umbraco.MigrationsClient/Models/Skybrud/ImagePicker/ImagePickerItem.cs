using System;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.ImagePicker {

    public class ImagePickerItem {

        public string? Title { get; set; }

        public string? Description { get; set; }

        public int ImageId { get; set; }

        public JToken? Link { get; set; }

        public string? ImageUrl { get; set; }

        public ImagePickerItem() { }

        public ImagePickerItem(JObject json) {
            if (json is null) throw new ArgumentNullException(nameof(json));
            Title = json.GetString("title");
            Description = json.GetString("description");
            ImageId = json.GetInt32("imageId");
            Link = json.GetValue("link");
            ImageUrl = json.GetString("imageUrl");
        }

    }

}