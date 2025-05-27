using System.Collections;
using System.Collections.Generic;

namespace Limbo.Umbraco.MigrationsClient.Models.Umbraco.BlockList;

public class LegacyBlockListPartList : IReadOnlyList<LegacyBlockListPart> {

    private readonly IReadOnlyList<LegacyBlockListPart> _items;

    public int Count => _items.Count;

    public LegacyBlockListPart this[int index] => _items[index];

    public LegacyBlockListPartList(IReadOnlyList<LegacyBlockListPart> items) {
        _items = items;
    }

    public IEnumerator<LegacyBlockListPart> GetEnumerator() {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

}