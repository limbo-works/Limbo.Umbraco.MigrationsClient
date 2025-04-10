using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

// ReSharper disable MemberHidesStaticFromOuterClass

namespace Limbo.Umbraco.MigrationsClient.Models.Umbraco.Grid;

public class LegacyGridEditorElementsConfig {

    public IReadOnlyList<AllowedType> AllowedTypes { get; }

    public int MaxItems { get; }

    public bool SinglePicker { get; }

    public LegacyGridEditorElementsConfig(JObject json) {
        AllowedTypes = json.GetArrayItems("allowedTypes", AllowedType.Parse);
        MaxItems = json.GetInt32("maxItems");
        SinglePicker = json.GetBoolean("singlePicker");
    }

    public class AllowedType {

        public Guid Key { get; }

        private AllowedType(JObject json) {
            Key = json.GetGuid("key");
        }

        public static AllowedType Parse(JObject json) {
            return new AllowedType(json);
        }

    }

    public static LegacyGridEditorElementsConfig Parse(JObject json) {
        return new LegacyGridEditorElementsConfig(json);
    }

}
