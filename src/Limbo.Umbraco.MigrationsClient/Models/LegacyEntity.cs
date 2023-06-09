using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models {

    public class LegacyEntity : JsonObjectBase, ILegacyEntity {

        private readonly Dictionary<string, LegacyProperty> _properties;

        public int Id { get; }

        public Guid Key { get; }

        public string Name { get; }

        public string Url { get; }

        public string Type => ContentTypeAlias;

        public string ContentTypeAlias { get; }

        public IReadOnlyList<LegacyProperty> Properties { get; }

        protected LegacyEntity(JObject json) : base(json) {

            Id = json.GetInt32("id");
            Key = json.GetGuid("key");
            Name = json.GetString("name")!;
            Url = json.GetString("url")!;
            ContentTypeAlias = json.GetString("type")!;

            JObject jsonProperties = json.GetObject("properties")!;

            try {

                List<LegacyProperty> properties = new();

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

        public bool TryGetValue(string alias, out JToken? result) {
            if (_properties.TryGetValue(alias, out LegacyProperty? property) && property.Value.Type != JTokenType.Null) {
                result = property.Value;
                return true;
            }
            result = null;
            return false;
        }

    }

}