using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Skybrud.Essentials.Json.Newtonsoft.Converters;

namespace Limbo.Umbraco.MigrationsClient.Models.Umbraco;

/// <summary>
/// Class representing an Umbraco GUID UDI.
///
/// Since this package doesn't have a dependency for Umbraco, the goal of this class is to mimic Umbraco's
/// <c>GuidUdi</c> class, but with focus on migrations, meaning not necessarily all features found in the Umbraco
/// counterpart.
///
/// In Umbraco, the <c>GuidUdi</c> class also extends the <c>Udi</c> class, but this isn't necessary for our use case
/// in this package, as we're really only supporting GUID based UDIs.
/// </summary>
/// <see>
///     <cref>https://github.com/umbraco/Umbraco-CMS/blob/contrib/src/Umbraco.Core/GuidUdi.cs</cref>
/// </see>
/// <see>
///     <cref>https://github.com/umbraco/Umbraco-CMS/blob/contrib/src/Umbraco.Core/UdiParser.cs</cref>
/// </see>
[JsonConverter(typeof(StringJsonConverter))]
public class GuidUdi {

    public string EntityType { get; }

    public Guid Guid { get; }

    public GuidUdi(string entityType, Guid guid) {
        EntityType = entityType;
        Guid = guid;
    }

    public override string ToString() {
        return $"umb://{EntityType}/{Guid:N}";
    }

    public static GuidUdi Parse(string? s) {

        if (Uri.IsWellFormedUriString(s, UriKind.Absolute) == false || Uri.TryCreate(s, UriKind.Absolute, out Uri? uri) == false) {
            throw new FormatException($"String \"{s}\" is not a valid udi.");
        }

        string entityType = uri.Host;
        string path = uri.AbsolutePath.TrimStart('/');

        if (!Guid.TryParse(path, out Guid guid)) {
            throw new FormatException($"String \"{s}\" is not a valid udi.");
        }

        return new GuidUdi(entityType, guid);

    }

    public static bool TryParse(string? s, [NotNullWhen(true)] out GuidUdi? result) {

        result = null;
        if (Uri.IsWellFormedUriString(s, UriKind.Absolute) == false || Uri.TryCreate(s, UriKind.Absolute, out Uri? uri) == false) {
            return false;
        }

        string entityType = uri.Host;
        string path = uri.AbsolutePath.TrimStart('/');

        if (!Guid.TryParse(path, out Guid guid)) return false;

        result = new GuidUdi(entityType, guid);
        return true;

    }

}