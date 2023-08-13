using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Limbo.Umbraco.MigrationsClient.Models.ContentTypes;
using Limbo.Umbraco.MigrationsClient.Models.Properties;

namespace Limbo.Umbraco.MigrationsClient.Models.Umbraco.NestedContent {

    public class NestedContentElement : ILegacyElement {

        public Guid Key { get; }

        public string Name { get; }

        public string ContentTypeAlias { get; }

        public IReadOnlyList<ILegacyProperty> Properties { get; }

        public NestedContentElement(NestedContentItem item, LegacyContentType contentType) {

            Key = item.Key;
            Name = item.Name;
            ContentTypeAlias = item.ContentTypeAlias;

            List<ILegacyProperty> properties = new();

            foreach (var property in item.Properties) {

                if (!contentType.TryGetPropertyType(property.Key, out LegacyPropertyType? propertyType)) {
                    throw new Exception($"Content type '{contentType.Alias}' does not contain a property type with the alias '{property.Key}'...");
                }

                properties.Add(new NestedContentProperty(property.Key, propertyType.EditorAlias, property.Value));

            }

            Properties = properties;

        }

        public bool HasProperty(string alias) {
            // TODO: Should we use a dictionary?
            return Properties.Any(x => x.Alias == alias);
        }

        public bool TryGetProperty(string alias, [NotNullWhen(true)] out ILegacyProperty? result) {
            // TODO: Should we use a dictionary?
            result = Properties.FirstOrDefault(x => x.Alias == alias);
            return result != null;
        }

    }

}