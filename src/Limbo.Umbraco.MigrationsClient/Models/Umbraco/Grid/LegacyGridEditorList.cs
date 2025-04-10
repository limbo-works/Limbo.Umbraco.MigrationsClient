using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Limbo.Umbraco.MigrationsClient.Models.Umbraco.Grid;

public class LegacyGridEditorList : IReadOnlyList<LegacyGridEditor> {

    private readonly IReadOnlyList<LegacyGridEditor> _editors;

    public int Count => _editors.Count;

    public LegacyGridEditor this[int index] => _editors[index];

    public LegacyGridEditorList(IEnumerable<LegacyGridEditor> editors) {
        _editors = [.. editors];
    }

    public LegacyGridEditor? GetByAlias(string alias) {
        return _editors.FirstOrDefault(x => x.Alias == alias);
    }

    public IEnumerator<LegacyGridEditor> GetEnumerator() {
        return _editors.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

}