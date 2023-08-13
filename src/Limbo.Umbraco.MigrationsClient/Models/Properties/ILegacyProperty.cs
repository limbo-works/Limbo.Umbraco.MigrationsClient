using Newtonsoft.Json.Linq;

namespace Limbo.Umbraco.MigrationsClient.Models.Properties {

    public interface ILegacyProperty {

        string Alias { get; }

        string EditorAlias { get; }

        JToken Value { get; }

    }

}