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

    public GuidUdi(string entityType, string guid) {
        EntityType = entityType;
        Guid = new Guid(guid);
    }

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

    public static GuidUdi? ParseOrNullIfEmpty(string? s) {

        if (string.IsNullOrWhiteSpace(s)) return null;

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

    protected bool Equals(GuidUdi other) {
        return EntityType == other.EntityType && Guid.Equals(other.Guid);
    }

    public override bool Equals(object? obj) {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((GuidUdi) obj);
    }

    public override int GetHashCode() {
        return HashCode.Combine(EntityType, Guid);
    }

    public static GuidUdi CreateContent(Guid key) {
        return CreateDocument(key);
    }

    public static GuidUdi CreateDocument(Guid key) {
        return new GuidUdi("document", key);
    }

    public static GuidUdi CreateMedia(Guid key) {
        return new GuidUdi("media", key);
    }

    public static GuidUdi CreateUser(Guid key) {
        return new GuidUdi("user", key);
    }

    public static bool operator ==(GuidUdi? d1, GuidUdi? d2) {

        // Check for NULL conditions
        if (d1 is null) return d2 is null;
        if (d2 is null) return false;

        // Pass the comparison on the == operator of DateTime
        return d1.ToString() == d2.ToString();

    }

    public static bool operator !=(GuidUdi? d1, GuidUdi? d2) {
        return !(d1 == d2);
    }

}