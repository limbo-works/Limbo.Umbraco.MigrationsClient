using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Skybrud.Essentials.Exceptions;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.Skybrud;

public class SkybrudBorgerDkArticle {

    [JsonProperty("id")]
    public int Id { get; }

    [JsonProperty("url")]
    public string Url { get; }

    [JsonProperty("domain")]
    public string Domain { get; }

    [JsonProperty("municipality")]
    public int Municipality { get; }

    [JsonProperty("title")]
    public string Title { get; }

    [JsonProperty("header")]
    public string Header { get; }

    [JsonProperty("byline")]
    public string Byline { get; }

    [JsonProperty("selection")]
    public IReadOnlyList<string> Selection { get; }

    private SkybrudBorgerDkArticle(JObject json) {
        Id = json.GetInt32("id");
        Url = json.GetString("url") ?? throw new BjernerSaysNoException();
        Domain = json.GetString("domain") ?? throw new BjernerSaysNoException();
        Municipality = json.GetInt32("municipality");
        Title = json.GetString("title") ?? throw new BjernerSaysNoException();
        Header = json.GetString("header") ?? throw new BjernerSaysNoException();
        Byline = json.GetString("byline") ?? throw new BjernerSaysNoException();
        Selection = json.GetStringArray("selection");
    }

    public static SkybrudBorgerDkArticle Parse(JObject json) {
        return new SkybrudBorgerDkArticle(json);
    }

}