using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Archetype {

    public class ArchetypeFieldsetModel {

        public string Alias { get; }

        public bool IsDisabled { get; }

        public Guid Id { get; }

        public IReadOnlyList<ArchetypePropertyModel> Properties { get; }

        private ArchetypeFieldsetModel(JObject json) {

            Alias = json.GetString("alias")!;
            Id = json.GetGuid("id");
            IsDisabled = json.GetBoolean("disabled");
            Properties = json.GetArrayItems("properties", ArchetypePropertyModel.Parse);

            JToken? releaseDate = json.GetValue("releaseDate");
            JToken? expireDate = json.GetValue("expireDate");

            if (releaseDate is not null && releaseDate.Type != JTokenType.Null) {
                throw new Exception($"Archetype fieldset property 'releaseDate' has a value: {releaseDate}...");
            }

            if (expireDate is not null && expireDate.Type != JTokenType.Null) {
                throw new Exception($"Archetype fieldset property 'expireDate' has a value: {expireDate}...");
            }

        }

        [return: NotNullIfNotNull("json")]
        public static ArchetypeFieldsetModel? Parse(JObject? json) {
            return json is null ? null : new ArchetypeFieldsetModel(json);
        }

    }

}