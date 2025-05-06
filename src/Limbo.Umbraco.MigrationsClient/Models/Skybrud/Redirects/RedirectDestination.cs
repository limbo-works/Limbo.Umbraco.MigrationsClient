using System;
using System.Diagnostics.CodeAnalysis;
using Limbo.Umbraco.MigrationsClient.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.Redirects;

public class RedirectDestination : LegacyObjectBase {

    [JsonProperty("id")]
    public int Id { get; }

    [JsonProperty("key")]
    public Guid Key { get; }

    [JsonProperty("name")]
    public string Name { get; }

    [JsonProperty("url")]
    public string? Url { get; }

    [JsonProperty("target", NullValueHandling = NullValueHandling.Ignore)]
    public string? Target { get; }

    [JsonProperty("type")]
    public RedirectDestinationType Type { get; }

    public RedirectDestination(JObject json) : base(json) {
        if (json is null) throw new ArgumentNullException(nameof(json), "JSON source cannot be null.");
        Id = json.GetInt32("id");
        Key = json.GetGuid("key");
        Name = json.GetString("name")!;
        Url = json.GetString("url");
        Target = json.GetString("target");
        Type = json.GetEnumOrNull<RedirectDestinationType>("type") ?? json.GetEnumOrNull<RedirectDestinationType>("mode") ?? throw new MigrationsParseException("Redirect destination JSON source does contain either a 'type' or 'mode' property.");
    }

    [return: NotNullIfNotNull(nameof(json))]
    public static RedirectDestination? Parse(JObject? json) {
        return json is null ? null : new RedirectDestination(json);
    }

}