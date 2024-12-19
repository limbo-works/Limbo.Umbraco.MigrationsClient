using System.Collections;
using System.Collections.Generic;
using Limbo.Umbraco.MigrationsClient.Models.Content;
using Limbo.Umbraco.MigrationsClient.Models.Properties;
using Limbo.Umbraco.MigrationsClient.Parsers.Skybrud;
using Newtonsoft.Json.Linq;

// ReSharper disable MethodOverloadWithOptionalParameter

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

    public static ElementsModel? Parse(JArray array) {
        return SkybrudElementsParser.DefaultParser.ParseElements(array, null, null);
    }

    public static ElementsModel? Parse(JArray array, LegacyContent? content = null, LegacyProperty? property = null) {
        return SkybrudElementsParser.DefaultParser.ParseElements(array, content, property);
    }

}