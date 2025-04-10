using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

#pragma warning disable CS0109 // Member does not hide an inherited member; new keyword is not required

namespace Limbo.Umbraco.MigrationsClient.Models.Users;

public class LegacyUserList : IReadOnlyList<LegacyUser> {

    private readonly IReadOnlyList<LegacyUser> _list;

    private static LegacyUserList? _empty;

    private Dictionary<int, LegacyUser>? _byId;
    private Dictionary<Guid, LegacyUser>? _byKey;
    private Dictionary<string, LegacyUser>? _byLogin;
    private Dictionary<string, LegacyUser>? _byEmail;

    public static new LegacyUserList Empty => _empty ??= [];

    public int Count => _list.Count;

    public LegacyUser this[int index] => _list[index];

    public LegacyUserList() {
        _list = [];
    }

    public LegacyUserList(IReadOnlyList<LegacyUser> list) {
        _list = list;
    }

    public bool TryGet(int id, [NotNullWhen(true)] out LegacyUser? result) {
        _byId ??= this.ToDictionary(x => x.Id);
        return _byId.TryGetValue(id, out result);
    }

    public bool TryGet(Guid key, [NotNullWhen(true)] out LegacyUser? result) {
        _byKey ??= this.ToDictionary(x => x.Key);
        return _byKey.TryGetValue(key, out result);
    }

    public bool TryGet(string value, [NotNullWhen(true)] out LegacyUser? result) {
        _byEmail ??= this.ToDictionary(x => x.Email);
        _byLogin ??= this.ToDictionary(x => x.Username);
        return _byEmail.TryGetValue(value, out result) || _byLogin.TryGetValue(value, out result);
    }

    public IEnumerator<LegacyUser> GetEnumerator() {
        return _list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

}