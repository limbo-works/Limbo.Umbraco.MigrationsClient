using System;
using System.Collections.Generic;
using System.Linq;
using Limbo.Umbraco.MigrationsClient.Models.Properties;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;
using Skybrud.Essentials.Time;

namespace Limbo.Umbraco.MigrationsClient.Models {

    public class LegacyEntity : JsonObjectBase, ILegacyEntity {

        private readonly Dictionary<string, ILegacyProperty> _properties;

        #region Member methods

        public int Id { get; }

        public Guid Key { get; }

        public string Name { get; }

        public string Url { get; }

        public string ContentTypeAlias { get; }

        public EssentialsTime CreateDate { get; }

        public EssentialsTime UpdateDate { get; }

        public IReadOnlyList<ILegacyProperty> Properties { get; }

        #endregion

        #region Constructors

        protected LegacyEntity(JObject json) : base(json) {

            Id = json.GetInt32("id");
            Key = json.GetGuid("key");
            Name = json.GetString("name")!;
            Url = json.GetString("url")!;
            ContentTypeAlias = json.GetString("type")!;

            CreateDate = json.GetString("createDate", EssentialsTime.Parse)!;
            UpdateDate = json.GetString("updateDate", EssentialsTime.Parse)!;

            JObject jsonProperties = json.GetObject("properties")!;

            try {

                List<ILegacyProperty> properties = new();

                foreach (var property in jsonProperties.Properties()) {
                    LegacyProperty lp = jsonProperties.GetObject(property.Name, LegacyProperty.Parse)!;
                    if (lp.EditorAlias is null or "Umbraco.ListView") continue;
                    properties.Add(lp);
                }

                Properties = properties;
                _properties = Properties.ToDictionary(x => x.Alias);
            } catch (Exception ex) {
                throw new Exception($"Failed parsing entity properties from JSON.\r\n\r\n{json}", ex);
            }

        }

        #endregion

        #region Member methods

        public bool TryGetValue(string alias, out JToken? result) {
            if (_properties.TryGetValue(alias, out ILegacyProperty? property) && property.Value.Type != JTokenType.Null) {
                result = property.Value;
                return true;
            }
            result = null;
            return false;
        }

        #endregion

    }

}