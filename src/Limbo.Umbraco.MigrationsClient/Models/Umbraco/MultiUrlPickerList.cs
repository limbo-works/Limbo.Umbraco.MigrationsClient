using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Limbo.Umbraco.MigrationsClient.Models.Umbraco;

public class MultiUrlPickerList : IReadOnlyList<MultiUrlPickerItem> {

    private readonly IReadOnlyList<MultiUrlPickerItem> _items;

    public int Count => _items.Count;

    public MultiUrlPickerItem this[int index] => _items[index];

    public MultiUrlPickerList(IReadOnlyList<MultiUrlPickerItem> items) {
        _items = items;
    }

    public IEnumerator<MultiUrlPickerItem> GetEnumerator() {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    public static MultiUrlPickerList Parse(JArray array) {

        List<MultiUrlPickerItem> items = [];

        foreach (JToken token in array) {

            if (token is JObject obj) items.Add(MultiUrlPickerItem.Parse(obj));

        }

        return new MultiUrlPickerList(items);

    }

}