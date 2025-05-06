using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Limbo.Umbraco.MigrationsClient.Exceptions;
using Limbo.Umbraco.MigrationsClient.Models.Content;
using Limbo.Umbraco.MigrationsClient.Parsers.Skybrud;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Strings;
using Skybrud.Essentials.Strings.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.Elements;

public class ElementsItem {

    [JsonProperty(Order = -999)]
    public Guid Key { get; }

    [JsonProperty(Order = -998)]
    public Guid ContentType { get; }

    [JsonProperty(Order = 999)]
    public IReadOnlyDictionary<string, ElementsProperty> Properties { get; }

    public ElementsItem(Guid key, Guid contentType, IReadOnlyDictionary<string, ElementsProperty> properties) {
        Key = key;
        ContentType = contentType;
        Properties = properties;
    }

    public ElementsItem(ElementsItem item) {
        Key = item.Key;
        ContentType = item.ContentType;
        Properties = item.Properties;
    }

    /// <summary>
    /// Returns the property with the specified <paramref name="alias"/>, or <see langword="null"/> if not found.
    /// </summary>
    /// <param name="alias">The alias of the property.</param>
    /// <returns>An instance of <see cref="ElementsProperty"/> representing the property, or <see langword="null"/> if not found.</returns>
    public ElementsProperty? GetProperty(string alias) {
        return Properties.GetValueOrDefault(alias);
    }

    /// <summary>
    /// Returns an instance of <see cref="JToken"/> representing the value of the property with the specified
    /// <paramref name="alias"/>. If a matching property isn't found, <see langword="null"/> is returned instead.
    /// </summary>
    /// <param name="alias">The alias of the property.</param>
    /// <returns>An instance of <see cref="JToken"/> representing the property value.</returns>
    public JToken? GetValue(string alias) {
        if (Properties.TryGetValue(alias, out ElementsProperty? property) && property.Value.Type != JTokenType.Null) {
            return property.Value;
        }
        return null;
    }

    /// <summary>
    /// Returns whether a property with the specified <paramref name="alias"/> exists.
    /// </summary>
    /// <param name="alias">The alias of the property.</param>
    /// <returns><see langword="true"/> if the property exists; otherwise, <see langword="false"/>.</returns>
    public bool HasProperty(string alias) {
        return Properties.ContainsKey(alias);
    }

    /// <summary>
    /// Attempts to get the property with the specified <paramref name="alias"/>.
    /// </summary>
    /// <param name="alias">The alias of the property.</param>
    /// <param name="result">When this method returns, holds an instance of <see cref="ElementsProperty"/> if successful; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
    public bool TryGetProperty(string alias, [NotNullWhen(true)] out ElementsProperty? result) {
        return Properties.TryGetValue(alias, out result);
    }

    /// <summary>
    /// Attempts to get the value of the property with the specified <paramref name="alias"/>.
    /// </summary>
    /// <param name="alias">The alias of the property.</param>
    /// <param name="result">When this method returns, holds an instance of <see cref="JToken"/> representing the property value if successful; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
    public bool TryGetValue(string alias, [NotNullWhen(true)] out JToken? result) {
        if (Properties.TryGetValue(alias, out ElementsProperty? property) && property.Value.Type != JTokenType.Null) {
            result = property.Value;
            return true;
        }
        result = null;
        return false;
    }

    public bool TryGetValue<T>(string alias, Func<JObject, T> callback, [NotNullWhen(true)] out T? result) {
        if (Properties.TryGetValue(alias, out ElementsProperty? property) && property.Value is JObject value) {
            result = callback(value);
            return result is not null;
        }
        result = default;
        return false;
    }

    public bool GetBoolean(string alias) {
        if (!TryGetValue(alias, out JToken? value)) return false;
        return value.Type switch {
            JTokenType.Boolean => value.Value<bool>(),
            JTokenType.Integer => value.Value<int>() == 1,
            JTokenType.String => StringUtils.ParseBoolean(value.Value<string>()),
            _ => false,
        };
    }

    public bool? GetBooleanOrNull(string alias) {
        if (!TryGetValue(alias, out JToken? value)) return null;
        return value.Type switch {
            JTokenType.Boolean => value.Value<bool>(),
            JTokenType.Integer => value.Value<int>() switch {
                0 => false,
                1 => true,
                _ => null
            },
            JTokenType.String => StringUtils.ParseBooleanOrNull(value.Value<string>()),
            _ => null,
        };
    }

    public int GetInt32(string alias) {
        if (!TryGetValue(alias, out JToken? value)) return 0;
        return value.Type switch {
            JTokenType.Boolean => value.ToObject<bool>() ? 1 : 0,
            JTokenType.Integer => value.ToObject<int>(),
            JTokenType.Float => value.ToObject<int>(),
            JTokenType.String => StringUtils.ParseInt32(value.Value<string>()),
            _ => 0
        };
    }

    public int? GetInt32Null(string alias) {
        if (!TryGetValue(alias, out JToken? value)) return null;
        return value.Type switch {
            JTokenType.Boolean => value.ToObject<bool>() ? 1 : 0,
            JTokenType.Integer => value.ToObject<int>(),
            JTokenType.Float => value.ToObject<int>(),
            JTokenType.String => StringUtils.ParseInt32OrNull(value.Value<string>()),
            _ => null
        };
    }

    public int? GetInt32OrNull(string propertyAlias) {
        if (!TryGetProperty(propertyAlias, out ElementsProperty? property)) return null;
        return property.Value.Type switch {
            JTokenType.Integer => property.Value.Value<int>(),
            _ => null
        };
    }

    public string? GetString(string alias) {
        return !TryGetValue(alias, out JToken? value) ? null : string.Format(CultureInfo.InvariantCulture, "{0}", value);
    }

    public Guid GetGuid(string alias) {
        return Guid.TryParse(GetString(alias), out Guid result) ? result : Guid.Empty;
    }

    public Guid? GetGuidOrNull(string alias) {
        return Guid.TryParse(GetString(alias), out Guid result) ? result : null;
    }

    public T? GetString<T>(string alias, Func<string, T> callback) {
        return !TryGetValue(alias, out JToken? value) ? default : callback(string.Format(CultureInfo.InvariantCulture, "{0}", value));
    }

    public JObject? GetObject(string alias) {
        return TryGetValue(alias, out JToken? value) ? value as JObject : null;
    }

    public T? GetObject<T>(string alias, Func<JObject, T> callback) {
        return TryGetValue(alias, out JToken? value) && value is JObject obj ? callback(obj) : default;
    }

    public JArray? GetArray(string alias) {
        return TryGetValue(alias, out JToken? value) ? value as JArray : null;
    }

    public T? GetArray<T>(string alias, Func<JArray, T> callback) {
        return TryGetValue(alias, out JToken? value) && value is JArray array ? callback(array) : default;
    }

    public double? GetDoubleOrNull(string alias) {
        if (!TryGetValue(alias, out JToken? result)) return null;
        return result.Type switch {
            JTokenType.Float => result.Value<double>(),
            JTokenType.String => result.Value<string>().ToDoubleOrNull(),
            JTokenType.Integer => result.Value<int>(),
            _ => null
        };
    }

    public ElementsModel? GetElements(string propertyName, ILegacyElement owner, SkybrudElementsParser parser) {
        return GetArray(propertyName) is not { } array ? null : parser.ParseElements(array, owner, null);
    }

    public IReadOnlyList<ElementsItem> GetElementItems(string propertyName, ILegacyElement? owner, SkybrudElementsParser parser) {
        return GetArray(propertyName) is not { } array ? [] : parser.ParseElements(array, owner, null)?.Items ?? [];
    }

    public IReadOnlyList<T> GetElementItems<T>(string propertyName, ILegacyElement? owner, SkybrudElementsParser parser) where T : ElementsItem {

        IReadOnlyList<ElementsItem> source = GetElementItems(propertyName, owner, parser);

        if (source.Count == 0) return [];

        List<T> temp = [];
        foreach (ElementsItem item in source) {
            if (item is not T t) throw new MigrationsParseException($"An item is not of expected type '{typeof(T)}', got '{item.GetType()}' instead...\r\n\r\n{JObject.FromObject(item)}");
            temp.Add(t);
        }

        return temp;

    }

}