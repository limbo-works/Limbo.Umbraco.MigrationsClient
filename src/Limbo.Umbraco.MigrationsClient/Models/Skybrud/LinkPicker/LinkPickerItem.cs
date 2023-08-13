using System;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.LinkPicker {

    public class LinkPickerItem {

        public int Id { get; }

        public string Name { get; }

        public string Url { get; }

        public string Target { get; }

        public LinkPickerMode Mode { get; }

        public LinkPickerItem(JObject json) {
            if (json is null) throw new ArgumentNullException(nameof(json));
            Id = json.GetInt32("id");
            Name = json.GetString("name")!;
            Url = json.GetString("url")!;
            Target = json.GetString("target")!;
            Mode = json.GetEnum<LinkPickerMode>("mode");
        }

    }

}