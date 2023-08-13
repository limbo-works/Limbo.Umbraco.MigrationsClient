using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Limbo.Umbraco.MigrationsClient.Models.Umbraco.NestedContent {

    public class NestedContentModel : IReadOnlyList<NestedContentItem> {

        private readonly List<NestedContentItem> _items;

        #region Properties

        public int Count => _items.Count;

        public NestedContentItem this[int index] => _items[index];

        #endregion

        #region Constructors

        public NestedContentModel(IEnumerable<NestedContentItem> items) {
            _items = items.ToList();
        }

        #endregion

        #region Member methods

        public IEnumerator<NestedContentItem> GetEnumerator() {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        #endregion

        #region Static methods

        public static NestedContentModel Parse(JArray json) {
            return new NestedContentModel(json.Cast<JObject>().Select(x => new NestedContentItem(x)));
        }

        #endregion

    }

}