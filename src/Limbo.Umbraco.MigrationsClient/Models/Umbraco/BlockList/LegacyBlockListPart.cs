using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft;
using Skybrud.Essentials.Strings;

namespace Limbo.Umbraco.MigrationsClient.Models.Umbraco.BlockList;

public class LegacyBlockListPart {

    [JsonIgnore]
    public JObject JObject { get; }

    public Guid Key => Udi.Guid;

    public Guid ContentTypeKey { get; }

    public GuidUdi Udi { get; }

    public IReadOnlyDictionary<string, JToken> Properties { get; }

    public LegacyBlockListPart(Guid contentTypeKey, GuidUdi udi, JObject source) {
        ContentTypeKey = contentTypeKey;
        Udi = udi;
        JObject = source;
        Properties = source
            .Properties()
            .Where(x => x.Name is not "contentTypeKey" and not "udi")
            .ToDictionary(x => x.Name, x => x.Value);
    }

    public JToken? GetValue(string property) {
        return Properties.TryGetValue(property, out JToken? result) ? result : null;
    }

    public JObject? GetObject(string property) {
        if (!Properties.TryGetValue(property, out JToken? result)) return null;
        return result.Type switch {
            JTokenType.Object => result.Value<JObject>()!,
            JTokenType.String => JsonUtils.TryParseJsonObject(result.Value<string>(), out JObject? obj) ? obj : null,
            _ => null
        };
    }

    public T? GetObject<T>(string property, Func<JObject, T> callback) {
        if (!Properties.TryGetValue(property, out JToken? result)) return default;
        return result.Type switch {
            JTokenType.Object => callback(result.Value<JObject>()!),
            JTokenType.String => JsonUtils.TryParseJsonObject(result.Value<string>(), out JObject? obj)
                ? callback(obj)
                : default,
            _ => default
        };
    }

    public T[]? GetArray<T>(string property, Func<JObject, T> callback) {
        if (!Properties.TryGetValue(property, out JToken? result)) return null;
        return result.Type switch {
            JTokenType.Array => result.Value<JArray>()?.OfType<JObject>().Select(callback).ToArray(),
            JTokenType.String => JsonUtils.TryParseJsonArray(result.Value<string>(), out JArray? array) ? array.OfType<JObject>().Select(callback).ToArray() : default,
            _ => null
        };
    }

    public T? GetArray<T>(string property, Func<JArray, T> callback) {
        if (!Properties.TryGetValue(property, out JToken? result)) return default;
        return result.Type switch {
            JTokenType.Array => callback(result.Value<JArray>()!),
            JTokenType.String => JsonUtils.TryParseJsonArray(result.Value<string>(), out JArray? array) ? callback(array) : default,
            _ => default
        };
    }

    public string? GetString(string property) {
        return TryGetValue(property, out string? result) ? result : null;
    }

    public Guid GetGuid(string property) {
        return TryGetValue(property, out string? result) ? Guid.Parse(result) : Guid.Empty;
    }

    public T? GetString<T>(string property, Func<string, T> callback) {
        return TryGetValue(property, out string? result) ? callback(result) : default;
    }

    public T? GetString<T>(string property, Func<JObject, T> callback) {
        if (!TryGetValue(property, out string? result)) return default;
        return string.IsNullOrWhiteSpace(result) ? default : callback(JsonUtils.ParseJsonObject(result));
    }

    public T? GetString<T>(string property, Func<JArray, T> callback) {
        if (!TryGetValue(property, out string? result)) return default;
        return string.IsNullOrWhiteSpace(result) ? default : callback(JsonUtils.ParseJsonArray(result));
    }

    public int GetInt32(string property) {
        return TryGetValue(property, out int? result) ? result.Value : 0;
    }

    public bool GetBoolean(string property) {
        return TryGetBoolean(property, out bool? result) && result.Value;
    }

    public bool GetBoolean(string property, bool fallback) {
        return TryGetBoolean(property, out bool? result) ? result.Value : fallback;
    }

    public bool TryGetValue(string property, [NotNullWhen(true)] out JToken? result) {
        return Properties.TryGetValue(property, out result);
    }

    public bool TryGetValue(string property, [NotNullWhen(true)] out string? result) {
        if (Properties.TryGetValue(property, out JToken? token) && token.Type == JTokenType.String) {
            result = token.ToString();
            return true;
        }
        result = null;
        return false;
    }

    public bool TryGetBoolean(string property, [NotNullWhen(true)] out bool? result) {
        if (Properties.TryGetValue(property, out JToken? token)) {
            switch (token.Type) {
                case JTokenType.String:
                    return StringUtils.TryParseBoolean(token.ToString(), out result);
                case JTokenType.Boolean:
                    result = token.Value<bool>();
                    return true;
                case JTokenType.Integer:
                    result = token.Value<int>() == 1;
                    return true;
            }
        }
        result = null;
        return false;
    }

    public bool TryGetValue(string property, [NotNullWhen(true)] out int? result) {
        if (Properties.TryGetValue(property, out JToken? token) && token.Type == JTokenType.Integer) {
            result = token.Value<int>();
            return true;
        }
        result = null;
        return false;
    }

}