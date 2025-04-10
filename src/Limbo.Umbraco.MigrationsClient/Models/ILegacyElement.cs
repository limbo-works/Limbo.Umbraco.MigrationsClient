using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Limbo.Umbraco.MigrationsClient.Models.Properties;
using Newtonsoft.Json.Linq;

namespace Limbo.Umbraco.MigrationsClient.Models;

public interface ILegacyElementItem {

    Guid Key { get; }

    string Name { get; }

    string ContentTypeAlias { get; }

}

public interface ILegacyElement : ILegacyElementItem {

    IReadOnlyList<ILegacyProperty> Properties { get; }

    /// <summary>
    /// Returns an instance of <see cref="JToken"/> representing the value of the property with the specified
    /// <paramref name="alias"/>. If a matching property isn't found, <see langword="null"/> is returned instead.
    /// </summary>
    /// <param name="alias">The alias of the property.</param>
    /// <returns>An instance of <see cref="JToken"/> representing the property value.</returns>
    JToken? GetValue(string alias);

    /// <summary>
    /// Returns the property with the specified <paramref name="alias"/>, or <see langword="null"/> if not found.
    /// </summary>
    /// <param name="alias">The alias of the property.</param>
    /// <returns>An instance of <see cref="ILegacyProperty"/> representing the property, or <see langword="null"/> if not found.</returns>
    ILegacyProperty? GetProperty(string alias);

    /// <summary>
    /// Returns whether a property with the specified <paramref name="alias"/> exists.
    /// </summary>
    /// <param name="alias">The alias of the property.</param>
    /// <returns><see langword="true"/> if the property exists; otherwise, <see langword="false"/>.</returns>
    bool HasProperty(string alias);

    /// <summary>
    /// Attempts to get the value of the property with the specified <paramref name="alias"/>.
    /// </summary>
    /// <param name="alias">The alias of the property.</param>
    /// <param name="result">When this method returns, holds an instance of <see cref="JToken"/> representing the property value if successful; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
    bool TryGetValue(string alias, [NotNullWhen(true)] out JToken? result);

    /// <summary>
    /// Attempts to get the property with the specified <paramref name="alias"/>.
    /// </summary>
    /// <param name="alias">The alias of the property.</param>
    /// <param name="result">When this method returns, holds an instance of <see cref="ILegacyProperty"/> if successful; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
    bool TryGetProperty(string alias, [NotNullWhen(true)] out ILegacyProperty? result);

}