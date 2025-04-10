using System;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Time;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;
using System.Globalization;

namespace Limbo.Umbraco.MigrationsClient.Models.Users;

public class LegacyUser {

    public int Id { get; }

    public Guid Key { get; }

    public string Username { get; }

    public string Email { get; }

    public string Name { get; }

    public string Language { get; }

    public EssentialsTime CreateDate { get; }

    public EssentialsTime UpdateDate { get; }

    public string? Avatar { get; }

    public string State { get; }

    public bool IsActive => State == "active";

    public bool IsDisabled => State == "disabled";

    public LegacyUser(int id, Guid key, string username, string email, string name, string language, EssentialsTime createDate, EssentialsTime updateDate, string? avatar, string state) {
        Id = id;
        Key = key;
        Username = username;
        Email = email;
        Name = name;
        Language = language;
        CreateDate = createDate;
        UpdateDate = updateDate;
        Avatar = avatar;
        State = state;
    }

    public static LegacyUser Parse(JObject json) {

        int id = json.GetRequiredInt32("id");
        Guid key = json.GetRequiredGuid("key");
        string username = json.GetRequiredString("username");
        string email = json.GetRequiredString("email");
        string name = json.GetRequiredString("name");
        string language = json.GetRequiredString("language");
        EssentialsTime createDate = json.GetRequiredString("createDate", ParseIso8601Timestamp);
        EssentialsTime updateDate = json.GetRequiredString("updateDate", ParseIso8601Timestamp);
        string? avatar = json.GetString("avatar");
        string state = json.GetRequiredString("state");

        return new LegacyUser(id, key, username, email, name, language, createDate, updateDate, avatar, state);

    }

    private static EssentialsTime ParseIso8601Timestamp(string value) {
        return EssentialsTime.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
    }

}