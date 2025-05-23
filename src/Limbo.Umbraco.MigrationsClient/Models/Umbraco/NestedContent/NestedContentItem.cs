using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Umbraco.NestedContent;

public class NestedContentItem : LegacyObjectBase {

    #region Properties

    public Guid Key { get; }

    public string Name { get; }

    public string ContentTypeAlias { get; }

    public IReadOnlyDictionary<string, JToken> Properties { get; }

    #endregion

    #region Constructors

    public NestedContentItem(NestedContentItem item) : base(item.JObject) {
        Key = item.Key;
        Name = item.Name;
        ContentTypeAlias = item.ContentTypeAlias;
        Properties = item.Properties;
    }

    public NestedContentItem(JObject json) : base(json) {

        Key = json.GetGuid("key");
        Name = json.GetString("name")!;
        ContentTypeAlias = json.GetString("ncContentTypeAlias")!;

        Dictionary<string, JToken> properties = new();

        foreach (JProperty property in json.Properties()) {
            if (property.Name == "key") continue;
            if (property.Name == "name") continue;
            if (property.Name == "ncContentTypeAlias") continue;
            properties.Add(property.Name, property.Value);
        }

        Properties = properties;

    }

    #endregion

    #region Member methods

    public string? GetString(string propertyAlias) {
        return Properties.TryGetValue(propertyAlias, out JToken? value) ? string.Format(CultureInfo.InvariantCulture, "{0}", value) : null;
    }

    public GuidUdi? GetGuidUdi(string propertyAlias) {
        string? value = GetString(propertyAlias);
        return string.IsNullOrWhiteSpace(value) ? null : GuidUdi.Parse(value);
    }

    public JObject? GetObject(string propertyAlias) {

        if (Properties.TryGetValue(propertyAlias, out JToken? value) && value is JObject json) {
            return json;
        }

        return null;

    }

    public TResult? GetObject<TResult>(string propertyAlias, Func<JObject, TResult> callback) {

        if (Properties.TryGetValue(propertyAlias, out JToken? value) && value is JObject json) {
            return callback(json);
        }

        return default;

    }

    public bool TryGetString(string propertyAlias, [NotNullWhen(true)] out string? result) {
        if (Properties.TryGetValue(propertyAlias, out JToken? value)) {
            result = string.Format(CultureInfo.InvariantCulture, "{0}", value);
            return true;
        }
        result = null;
        return false;
    }

    #endregion

}