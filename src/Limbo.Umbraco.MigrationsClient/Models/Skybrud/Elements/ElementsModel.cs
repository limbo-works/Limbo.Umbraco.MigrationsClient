using System.Collections;
using System.Collections.Generic;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.Elements;

public class ElementsModel : IReadOnlyList<ElementsItem> {

    public int Count => Items.Count;

    public ElementsItem this[int index] => Items[index];

    public IReadOnlyList<ElementsItem> Items { get; }

    public ElementsModel(IReadOnlyList<ElementsItem> items) {
        Items = items;
    }

    public IEnumerator<ElementsItem> GetEnumerator() {
        return Items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

}