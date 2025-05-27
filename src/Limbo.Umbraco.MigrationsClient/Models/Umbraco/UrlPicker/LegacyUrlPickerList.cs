using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Limbo.Umbraco.MigrationsClient.Models.Umbraco.UrlPicker;

public class LegacyUrlPickerList : IReadOnlyList<LegacyUrlPickerItem> {

    private readonly List<LegacyUrlPickerItem> _items = [];

    #region Properties

    public int Count => _items.Count;

    public LegacyUrlPickerItem this[int index] => _items[index];

    #endregion

    #region Constructors

    public LegacyUrlPickerList() { }

    public LegacyUrlPickerList(LegacyUrlPickerItem item) {
        Add(item);
    }

    public LegacyUrlPickerList(IEnumerable<LegacyUrlPickerItem> items) {
        _items.AddRange(items);
    }

    #endregion

    #region Member methods

    public void Add(LegacyUrlPickerItem item) {
        _items.Add(item);
    }

    public IEnumerator<LegacyUrlPickerItem> GetEnumerator() {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    #endregion

    #region Static methods

    public static LegacyUrlPickerList Parse(JArray array) {
            return [.. array.Cast<JObject>().Select(LegacyUrlPickerItem.Parse)];
    }

    #endregion

}