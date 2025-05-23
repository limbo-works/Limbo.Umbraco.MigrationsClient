using System.Collections.Generic;
using System.Linq;
using Limbo.Umbraco.MigrationsClient.Models;
using Limbo.Umbraco.MigrationsClient.Models.Properties;
using Limbo.Umbraco.MigrationsClient.Models.Umbraco.NestedContent;
using Newtonsoft.Json.Linq;

namespace Limbo.Umbraco.MigrationsClient.Parsers.Community;

public class NestedContentParser : INestedContentParser {

    public virtual NestedContentModel ParseModel(JArray array, ILegacyElement? content, ILegacyProperty? property) {

        List<NestedContentItem> items = [];

        foreach (JObject item in array.Cast<JObject>()) {
            items.Add(ParseItem(item, content, property));
        }

        return new NestedContentModel(items);

    }

    protected virtual NestedContentItem ParseItem(JObject json, ILegacyElement? content, ILegacyProperty? property) {
        return new NestedContentItem(json);
    }

}