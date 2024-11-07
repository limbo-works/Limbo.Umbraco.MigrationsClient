using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Limbo.Umbraco.MigrationsClient.Models.ContentTypes;
using Limbo.Umbraco.MigrationsClient.Models.Properties;
using Newtonsoft.Json.Linq;

namespace Limbo.Umbraco.MigrationsClient.Models.Umbraco.NestedContent;

public class NestedContentElement : ILegacyElement {

    private readonly Dictionary<string, ILegacyProperty> _properties;

    #region Member methods

    public Guid Key { get; }

    public string Name { get; }

    public string ContentTypeAlias { get; }

    public IReadOnlyList<ILegacyProperty> Properties { get; }

    #endregion

    #region Constructors

    public NestedContentElement(NestedContentItem item, LegacyContentType contentType) {

        Key = item.Key;
        Name = item.Name;
        ContentTypeAlias = item.ContentTypeAlias;

        List<ILegacyProperty> properties = [];

        foreach (var property in item.Properties) {

            if (!contentType.TryGetPropertyType(property.Key, out LegacyPropertyType? propertyType)) {
                throw new Exception($"Content type '{contentType.Alias}' does not contain a property type with the alias '{property.Key}'...");
            }

            properties.Add(new NestedContentProperty(property.Key, propertyType.EditorAlias, property.Value));

        }

        Properties = properties;
        _properties = Properties.ToDictionary(x => x.Alias, StringComparer.CurrentCultureIgnoreCase);

    }

    #endregion

    #region Member methods

    /// <summary>
    /// Returns the property with the specified <paramref name="alias"/>, or <see langword="null"/> if not found.
    /// </summary>
    /// <param name="alias">The alias of the property.</param>
    /// <returns>An instance of <see cref="ILegacyProperty"/> representing the property, or <see langword="null"/> if not found.</returns>
    public ILegacyProperty? GetProperty(string alias) {
        return _properties.GetValueOrDefault(alias);
    }

    /// <summary>
    /// Returns an instance of <see cref="JToken"/> representing the value of the property with the specified
    /// <paramref name="alias"/>. If a matching property isn't found, <see langword="null"/> is returned instead.
    /// </summary>
    /// <param name="alias">The alias of the property.</param>
    /// <returns>An instance of <see cref="JToken"/> representing the property value.</returns>
    public JToken? GetValue(string alias) {
        if (_properties.TryGetValue(alias, out ILegacyProperty? property) && property.Value.Type != JTokenType.Null) {
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
        return _properties.ContainsKey(alias);
    }

    /// <summary>
    /// Attempts to get the property with the specified <paramref name="alias"/>.
    /// </summary>
    /// <param name="alias">The alias of the property.</param>
    /// <param name="result">When this method returns, holds an instance of <see cref="ILegacyProperty"/> if successful; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
    public bool TryGetProperty(string alias, [NotNullWhen(true)] out ILegacyProperty? result) {
        return _properties.TryGetValue(alias, out result);
    }

    /// <summary>
    /// Attempts to get the value of the property with the specified <paramref name="alias"/>.
    /// </summary>
    /// <param name="alias">The alias of the property.</param>
    /// <param name="result">When this method returns, holds an instance of <see cref="JToken"/> representing the property value if successful; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
    public bool TryGetValue(string alias, [NotNullWhen(true)] out JToken? result) {
        if (_properties.TryGetValue(alias, out ILegacyProperty? property) && property.Value.Type != JTokenType.Null) {
            result = property.Value;
            return true;
        }
        result = null;
        return false;
    }

    public string? GetString(string alias) {
        return !TryGetValue(alias, out JToken? value) ? null : string.Format(CultureInfo.InvariantCulture, "{0}", value);
    }

    public T? GetString<T>(string alias, Func<string, T> callback) {
        return !TryGetValue(alias, out JToken? value) ? default : callback(string.Format(CultureInfo.InvariantCulture, "{0}", value));
    }

    #endregion

}