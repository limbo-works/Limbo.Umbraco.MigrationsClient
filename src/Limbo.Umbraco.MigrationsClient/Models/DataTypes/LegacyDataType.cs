using System;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;
using Skybrud.Essentials.Time;

namespace Limbo.Umbraco.MigrationsClient.Models.DataTypes;

public class LegacyDataType : LegacyObjectBase, IJsonParsable<LegacyDataType> {

    public int Id { get; }

    public Guid Key { get; }

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
        Name = json.GetString("name")!;
        DbType = json.GetString("dbType")!;
        CreateDate = json.GetString("createDate", EssentialsTime.Parse)!;
        UpdateDate = json.GetString("updateDate", EssentialsTime.Parse)!;
        EditorAlias = json.GetString("editorAlias")!;
        Editor = json.GetObject("editor", LegacyDataEditor.Parse);
        Config = json.GetObject("config")!;
    }

    public static LegacyDataType? Parse(JObject? json) {
        return json is null ? null : new LegacyDataType(json);
    }

}