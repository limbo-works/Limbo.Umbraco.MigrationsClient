using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Limbo.Umbraco.MigrationsClient.Models.Properties;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;
using Skybrud.Essentials.Time;

namespace Limbo.Umbraco.MigrationsClient.Models;

public class LegacyEntity : JsonObjectBase, ILegacyEntity {

    private readonly Dictionary<string, ILegacyProperty> _properties;

    #region Member methods

    public int Id { get; }

    public Guid Key { get; }

    public string Name { get; }

    public string Url { get; }

    public string ContentTypeAlias { get; }

    public EssentialsTime CreateDate { get; }

    public EssentialsTime UpdateDate { get; }

    public IReadOnlyList<ILegacyProperty> Properties { get; }

    #endregion

    #region Constructors

    protected LegacyEntity(JObject json) : base(json) {

        Id = json.GetInt32("id");
        Key = json.GetGuid("key");
        Name = json.GetString("name")!;
        Url = json.GetString("url")!;
        ContentTypeAlias = json.GetString("type")!;

        CreateDate = json.GetString("createDate", EssentialsTime.Parse)!;
        UpdateDate = json.GetString("updateDate", EssentialsTime.Parse)!;

        JObject jsonProperties = json.GetObject("properties")!;

        try {

            List<ILegacyProperty> properties = [];

            foreach (var property in jsonProperties.Properties()) {
                LegacyProperty lp = jsonProperties.GetObject(property.Name, LegacyProperty.Parse)!;
                if (lp.EditorAlias is null or "Umbraco.ListView") continue;
                properties.Add(lp);
            }

            Properties = properties;
            _properties = Properties.ToDictionary(x => x.Alias, StringComparer.CurrentCultureIgnoreCase);
        } catch (Exception ex) {
            throw new Exception($"Failed parsing entity properties from JSON.\r\n\r\n{json}", ex);
        }

    }

    #endregion

    #region Member methods

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
    /// Returns the property with the specified <paramref name="alias"/>, or <see langword="null"/> if not found.
    /// </summary>
    /// <param name="alias">The alias of the property.</param>
    /// <returns>An instance of <see cref="ILegacyProperty"/> representing the property, or <see langword="null"/> if not found.</returns>
    public ILegacyProperty? GetProperty(string alias) {
        return _properties.GetValueOrDefault(alias);
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

    /// <summary>
    /// Attempts to get the property with the specified <paramref name="alias"/>.
    /// </summary>
    /// <param name="alias">The alias of the property.</param>
    /// <param name="result">When this method returns, holds an instance of <see cref="ILegacyProperty"/> if successful; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
    public bool TryGetProperty(string alias, [NotNullWhen(true)] out ILegacyProperty? result) {
        return _properties.TryGetValue(alias, out result);
    }

    #endregion

}