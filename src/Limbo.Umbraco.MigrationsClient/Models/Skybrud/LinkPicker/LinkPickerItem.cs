using System;
using System.Diagnostics.CodeAnalysis;
using Limbo.Umbraco.MigrationsClient.Exceptions;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.LinkPicker;

public class LinkPickerItem : LegacyObjectBase {

    public int Id { get; }

    public string Name { get; }

    public string? Udi { get; }

    public string? Url { get; }

    public string? Target { get; }

    public LinkPickerType Type { get; }

    public LinkPickerItem(JObject json) : base(json) {
        if (json is null) throw new ArgumentNullException(nameof(json), "JSON source cannot be null.");
        Id = json.GetInt32("id");
        Name = json.GetString("name")!;
        Udi = json.GetString("udi");
        Url = json.GetString("url");
        Target = json.GetString("target");
        Type = json.GetEnumOrNull<LinkPickerType>("type") ?? json.GetEnumOrNull<LinkPickerType>("mode") ?? throw new MigrationsParseException("Link item JSON source does contain either a 'type' or 'mode' property.");
    }

    [return: NotNullIfNotNull(nameof(json))]
    public static LinkPickerItem? Parse(JObject? json) {
        return json is null ? null : new LinkPickerItem(json);
    }

}