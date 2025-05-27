using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Exceptions;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Umbraco.MediaPicker;

public class LegacyMediaPickerItem {

    public Guid Key { get; init; }

    public Guid MediaKey { get; init; }

    public LegacyMediaPickerItem() { }

    public LegacyMediaPickerItem(Guid key, Guid mediaKey) {
        Key = key;
        MediaKey = mediaKey;
    }

    public static LegacyMediaPickerItem Parse(JObject json) {

        Guid key = json.GetRequiredGuid("key");
        Guid mediaKey = json.GetRequiredGuid("mediaKey");

        if (json.Properties().Count() > 2) throw new BjernerSaysNoException($"Legacy media picker item has more than two properties. Are we perhaps overlooking some data?\r\n\r\n{json}");

        return new LegacyMediaPickerItem(key, mediaKey);

    }

}