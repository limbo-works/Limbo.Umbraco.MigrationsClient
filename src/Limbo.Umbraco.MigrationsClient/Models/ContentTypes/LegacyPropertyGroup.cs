using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.ContentTypes {

    public class LegacyPropertyGroup : JsonObjectBase {

        #region Properties

        public int Id { get; }

        public Guid Key { get; }

        public string Name { get; }

        public IReadOnlyList<LegacyPropertyType> Properties { get; }

        #endregion

        #region Constructors

        public LegacyPropertyGroup(JObject json) : base(json) {
            Id = json.GetInt32("id");
            Key = json.GetGuid("key");
            Name = json.GetString("name")!;
            Properties = json.GetArrayItems("properties", LegacyPropertyType.Parse);
        }

        #endregion

        #region Static methods

        [return: NotNullIfNotNull("json")]
        public static LegacyPropertyGroup? Parse(JObject? json) {
            return json == null ? null : new LegacyPropertyGroup(json);
        }

        #endregion

    }

}