using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Limbo.Umbraco.MigrationsClient.Models;
using Limbo.Umbraco.MigrationsClient.Models.Properties;
using Limbo.Umbraco.MigrationsClient.Models.Skybrud.Elements;
using Limbo.Umbraco.MigrationsClient.Models.Skybrud.Grid;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Exceptions;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Parsers.Skybrud;

public class SkybrudElementsParser {

    public static readonly SkybrudElementsParser DefaultParser = new();

    public virtual ElementsModel? ParseElements(JArray array, ILegacyElement? content, ILegacyProperty? property) {

        List<ElementsItem> temp = [];

        foreach (JObject obj in array.OfType<JObject>()) {
            if (ParseElement(obj, content, property) is { } item) temp.Add(item);
        }

        return temp.Count > 0 ? new ElementsModel(temp) : null;

    }

    public virtual ElementsItem? ParseElement(GridControl control, ILegacyElement? owner, ILegacyProperty? property) {
        return control.Value is JArray array ? ParseElement(array, owner, property) : null;
	}

    public virtual bool TryParseElement(GridControl control, ILegacyElement? owner, ILegacyProperty? property, [NotNullWhen(true)] out ElementsItem? result) {
        result = ParseElement(control, owner, property);
        return result is not null;
	}

    public virtual ElementsItem? ParseElement(JArray array, ILegacyElement? content, ILegacyProperty? property) {

        foreach (JToken token in array) {

            if (token is not JObject obj) continue;

            return ParseElement(obj, content, property);

        }

        return null;

	}

    public virtual ElementsItem? ParseElement(JObject obj, ILegacyElement? content, ILegacyProperty? property) {

            Guid key = obj.GetGuidOrNull("key") ?? throw new BjernerSaysNoException("JSON object does not contain a valid for the the 'key' property.");
            Guid contentType = obj.GetGuidOrNull("contentType") ?? throw new BjernerSaysNoException("JSON object does not contain a valid for the the 'contentType' property.");

            Dictionary<string, ElementsProperty> properties = obj
                .GetObject("properties")?
                .Properties()
                .Select(x => new ElementsProperty(x.Name, x.Value))
                .ToDictionary(x => x.Alias) ?? throw new BjernerSaysNoException("Skybrud elements item has no properties ¯\\_(ツ)_/¯");

            return ParseItem(key, contentType, properties, content, property);


	}

    protected virtual ElementsItem? ParseItem(Guid key, Guid contentTypeKey, IReadOnlyDictionary<string, ElementsProperty> properties, ILegacyElement? content, ILegacyProperty? property) {
        return new ElementsItem(key, contentTypeKey, properties);
    }

}