using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Limbo.Umbraco.MigrationsClient.Models.Umbraco.MediaPicker;

public class LegacyMediaPickerList : List<LegacyMediaPickerItem> {

    public LegacyMediaPickerList() { }

    public LegacyMediaPickerList(IEnumerable<LegacyMediaPickerItem> items) : base(items) { }

    public static LegacyMediaPickerList Parse(JArray array) {
        return [.. array.Cast<JObject>().Select(LegacyMediaPickerItem.Parse)];
    }

}