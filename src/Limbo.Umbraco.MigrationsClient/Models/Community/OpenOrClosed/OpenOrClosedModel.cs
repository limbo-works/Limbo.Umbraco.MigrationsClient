using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Limbo.Umbraco.MigrationsClient.Models.Community.OpenOrClosed;

public class OpenOrClosedModel : IReadOnlyList<OpenOrClosedItem> {

    private readonly IReadOnlyList<OpenOrClosedItem> _items;

    public int Count => _items.Count;

    public OpenOrClosedItem this[int index] => _items[index];

    private OpenOrClosedModel(IReadOnlyList<OpenOrClosedItem> items) {
        _items = items;
    }

    public IEnumerator<OpenOrClosedItem> GetEnumerator() {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    public static OpenOrClosedModel Parse(JArray array) {

        List<OpenOrClosedItem> items = new();

        foreach (JToken token in array) {

            if (token is JObject obj) items.Add(OpenOrClosedItem.Parse(obj));

        }

        return new OpenOrClosedModel(items);

    }

}