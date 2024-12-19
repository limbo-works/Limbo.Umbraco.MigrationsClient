using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

#pragma warning disable CS0109 // Member does not hide an inherited member; new keyword is not required

namespace Limbo.Umbraco.MigrationsClient.Models.DataTypes;

public class LegacyDataTypeList : ReadOnlyCollection<LegacyDataType> {

    private static LegacyDataTypeList? _empty;

    private Dictionary<int, LegacyDataType>? _byId;
    private Dictionary<Guid, LegacyDataType>? _byKey;

    public static new LegacyDataTypeList Empty => _empty ??= [];

    public LegacyDataTypeList() : base([]) { }

    public LegacyDataTypeList(IList<LegacyDataType> list) : base(list) { }

    public bool TryGet(int id, [NotNullWhen(true)] out LegacyDataType? result) {
        _byId ??= this.ToDictionary(x => x.Id);
        return _byId.TryGetValue(id, out result);
    }

    public bool TryGet(Guid key, [NotNullWhen(true)] out LegacyDataType? result) {
        _byKey ??= this.ToDictionary(x => x.Key);
        return _byKey.TryGetValue(key, out result);
    }

}