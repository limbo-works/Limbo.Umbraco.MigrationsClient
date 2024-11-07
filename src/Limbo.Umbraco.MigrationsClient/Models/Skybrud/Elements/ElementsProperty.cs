using Newtonsoft.Json.Linq;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.Elements;

public class ElementsProperty {

    public string Alias { get; }

    public JToken Value { get; }

    public ElementsProperty(string alias, JToken value) {
        Alias = alias;
        Value = value;
    }

}