using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;
using Skybrud.Essentials.Time;

namespace Limbo.Umbraco.MigrationsClient.Models.DataTypes;

public class LegacyDataType : LegacyObjectBase, IJsonParsable<LegacyDataType> {

    public int Id { get; }

    public Guid Key { get; }

    public ISet<int> Path { get; }

    public string Name { get; }

    public string DbType { get; }

    public EssentialsTime CreateDate { get; }

    public EssentialsTime UpdateDate { get; }

    public string EditorAlias { get; }

    public LegacyDataEditor? Editor { get; }

    public JObject Config { get; }

    public LegacyDataType(JObject json) : base(json) {
        Id = json.GetInt32("id");
        Key = json.GetGuid("key");
        Path = new HashSet<int>(json.GetInt32Array("path"));
        Name = json.GetString("name")!;
        DbType = json.GetString("dbType")!;
        CreateDate = json.GetString("createDate", EssentialsTime.FromIso8601)!;
        UpdateDate = json.GetString("updateDate", EssentialsTime.FromIso8601)!;
        EditorAlias = json.GetString("editorAlias")!;
        Editor = json.GetObject("editor", LegacyDataEditor.Parse);
        Config = json.GetObject("config")!;
    }

    public static LegacyDataType Parse(JObject json) {
        return new LegacyDataType(json);
    }

}