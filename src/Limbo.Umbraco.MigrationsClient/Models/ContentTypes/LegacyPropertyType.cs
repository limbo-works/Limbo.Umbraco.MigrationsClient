using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.ContentTypes {

    public class LegacyPropertyType : JsonObjectBase {

        #region Properties

        public int Id { get; }

        public Guid Key { get; }

        public string Alias { get; }

        public string Name { get; }

        public string EditorAlias { get; }

        public int DataTypeId { get; }

        #endregion

        #region Constructors

        public LegacyPropertyType(JObject json) : base(json) {
            Id = json.GetInt32("id");
            Key = json.GetGuid("key");
            Alias = json.GetString("alias")!;
            Name = json.GetString("name")!;
            EditorAlias = json.GetString("editorAlias")!;
            DataTypeId = json.GetInt32("dataTypeId");
        }

        #endregion

        #region Static methods

        [return: NotNullIfNotNull("json")]
        public static LegacyPropertyType? Parse(JObject? json) {
            return json == null ? null : new LegacyPropertyType(json);
        }

        #endregion

    }

}