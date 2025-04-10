using System;
using Skybrud.Essentials.Guids;
using Skybrud.Essentials.Time;

namespace Limbo.Umbraco.MigrationsClient.Models.Users;

public class LegacyUser {

    public int Id { get; }

    public Guid Key { get; }

    public string Login { get; }

    public string Email { get; }

    public string Name { get; }

    public string Language { get; }

    public EssentialsTime CreateDate { get; }

    public EssentialsTime UpdateDate { get; }

    public string? Avatar { get; }

    public bool UserDisabled { get; }

    public bool UserNoConsole { get; }

    public LegacyUser(int id, string login, string email, string name, string language, EssentialsTime createDate, EssentialsTime updateDate, string? avatar, bool userDisabled, bool userNoConsole) {
        Id = id;
        Key = GuidUtils.ToGuid(id);
        Login = login;
        Email = email;
        Name = name;
        Language = language;
        CreateDate = createDate;
        UpdateDate = updateDate;
        Avatar = avatar;
        UserDisabled = userDisabled;
        UserNoConsole = userNoConsole;
    }

    public LegacyUser(int id, Guid key, string login, string email, string name, string language, EssentialsTime createDate, EssentialsTime updateDate, string? avatar, bool userDisabled, bool userNoConsole) {
        Id = id;
        Key = key;
        Login = login;
        Email = email;
        Name = name;
        Language = language;
        CreateDate = createDate;
        UpdateDate = updateDate;
        Avatar = avatar;
        UserDisabled = userDisabled;
        UserNoConsole = userNoConsole;
    }

}