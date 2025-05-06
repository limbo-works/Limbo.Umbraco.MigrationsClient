using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;
using Skybrud.Essentials.Time;

namespace Limbo.Umbraco.MigrationsClient.Models.ContentTypes;

public class LegacyContentType : LegacyObjectBase, IJsonParsable<LegacyContentType> {

    #region Properties

    public int Id { get; }

    public Guid Key { get; }

    public string Alias { get; }

    public ISet<int> Path { get; }

    public string Name { get; }

    public string Icon { get; }

    public bool IsElement { get; }

    public EssentialsTime CreateDate { get; }

    public EssentialsTime UpdateDate { get; }

    public IReadOnlyList<LegacyPropertyGroup> Tabs { get; }

    public IReadOnlyList<LegacyPropertyType> Properties { get; }

    #endregion

    #region Constructors

    public LegacyContentType(JObject json) : base(json) {
        Id = json.GetInt32("id");
        Key = json.GetGuid("key");
        Alias = json.GetString("alias")!;
        Path = new HashSet<int>(json.GetInt32Array("path"));
        Name = json.GetString("name")!;
        Icon = json.GetString("icon") ?? string.Empty;
        IsElement = json.GetBoolean("element");
        CreateDate = json.GetString("createDate", EssentialsTime.FromIso8601)!;
        UpdateDate = json.GetString("updateDate", EssentialsTime.FromIso8601)!;
        Tabs = json.GetArrayItems("tabs", LegacyPropertyGroup.Parse);
        Properties = Tabs.SelectMany(x => x.Properties).ToArray();
    }

    #endregion

    #region Member methods

    public bool TryGetPropertyType(string alias, [NotNullWhen(true)] out LegacyPropertyType? result) {
        result = Properties.FirstOrDefault(x => x.Alias == alias);
        return result != null;
    }

    #endregion

    #region Static methods

    public static LegacyContentType? Parse(JObject? json) {
        return json == null ? null : new LegacyContentType(json);
    }

    #endregion

}