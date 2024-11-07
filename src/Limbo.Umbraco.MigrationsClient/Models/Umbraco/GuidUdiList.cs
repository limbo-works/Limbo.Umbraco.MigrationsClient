using System;
using System.Collections.Generic;
using Limbo.Umbraco.MigrationsClient.Models.Content;
using Limbo.Umbraco.MigrationsClient.Models.Media;
using Newtonsoft.Json;
using Skybrud.Essentials.Json.Newtonsoft.Converters;

namespace Limbo.Umbraco.MigrationsClient.Models.Umbraco;

/// <summary>
/// Class representing a list of <see cref="GuidUdi"/> items.
/// </summary>
/// <remarks>When serialized to JSON using <strong>Newtonsoft.Json</strong>, this list will be serialized as a comma
/// separated string of UDIs.</remarks>
[JsonConverter(typeof(StringJsonConverter))]
public class GuidUdiList : List<GuidUdi> {

	public GuidUdiList() {}

	public GuidUdiList(IEnumerable<GuidUdi> items) {
		AddRange(items);
	}

    public void AddContent(Guid key) {
        Add(new GuidUdi("document", key));
    }

    public void AddContent(LegacyContent content) {
        Add(new GuidUdi("document", content.Key));
    }

    public void AddMedia(LegacyMedia media) {
        Add(new GuidUdi("media", media.Key));
    }

    public override string ToString() {
        return string.Join(",", this);
    }

    public GuidUdiList? NullIfEmpty() {
        return Count == 0 ? null : this;
    }

    public static GuidUdiList? Parse(string? input) {

        if (string.IsNullOrWhiteSpace(input)) return null;

        GuidUdiList udis = [];

        foreach (string piece in input.Split(',')) {
            if (GuidUdi.TryParse(piece, out GuidUdi? udi)) {
                udis.Add(udi);
            }
        }

        return udis;

    }

}