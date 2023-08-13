using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.LinkPicker {

    public class LinkPickerList {

        public string Title { get; }

        public IReadOnlyList<LinkPickerItem> Items { get; }

        public LinkPickerList(JObject json) {
            Title = json.GetString("title")!;
            Items = json.GetArrayItems("items", x => new LinkPickerItem(x));
        }

        [return: NotNullIfNotNull("json")]
        public static LinkPickerList? Parse(JObject? json) {
            return json is null ? null : new LinkPickerList(json);
        }

    }

}