using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Umbraco.NestedContent {

    public class NestedContentItem {

        #region Properties

        public Guid Key { get; }

        public string Name { get; }

        public string ContentTypeAlias { get; }

        public IReadOnlyDictionary<string, JToken> Properties { get; }

        #endregion

        #region Constructors

        public NestedContentItem(JObject json) {

            Key = json.GetGuid("key");
            Name = json.GetString("name")!;
            ContentTypeAlias = json.GetString("ncContentTypeAlias")!;

            Dictionary<string, JToken> properties = new();

            foreach (JProperty property in json.Properties()) {
                if (property.Name == "key") continue;
                if (property.Name == "name") continue;
                if (property.Name == "ncContentTypeAlias") continue;
                properties.Add(property.Name, property.Value);
            }

            Properties = properties;

        }

        #endregion

    }

}