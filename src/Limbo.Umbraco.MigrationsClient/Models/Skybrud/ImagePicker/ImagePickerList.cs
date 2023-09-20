using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.ImagePicker {

    public class ImagePickerList : JsonObjectBase {

        public string? Title { get; }

        public IReadOnlyList<ImagePickerItem> Items { get; set; } = null!;

        public ImagePickerList() : base(null) { }

        public ImagePickerList(JObject json) : base(json) {
            Title = json.GetString("title");
            Items = json.GetArrayItems("items", x => new ImagePickerItem(x));
        }

        [return: NotNullIfNotNull("json")]
        public static ImagePickerList? Parse(JObject? json) {
            return json is null ? null : new ImagePickerList(json);
        }

    }

}