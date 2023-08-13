using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.ContentTypes {

    public class LegacyContentType : JsonObjectBase, IJsonParsable<LegacyContentType> {

        #region Properties

        public int Id { get; }

        public Guid Key { get; }

        public string Alias { get; }

        public string Name { get; }

        public string Icon { get; }

        public IReadOnlyList<LegacyPropertyGroup> Tabs { get; }

        public IReadOnlyList<LegacyPropertyType> Properties { get; }

        #endregion

        #region Constructors

        public LegacyContentType(JObject json) : base(json) {
            Id = json.GetInt32("id");
            Key = json.GetGuid("key");
            Alias = json.GetString("alias")!;
            Name = json.GetString("name")!;
            Icon = json.GetString("icon") ?? string.Empty;
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

        [return: NotNullIfNotNull("json")]
        public static LegacyContentType? Parse(JObject? json) {
            return json == null ? null : new LegacyContentType(json);
        }

        #endregion

    }

}