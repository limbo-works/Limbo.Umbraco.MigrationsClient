using Limbo.Umbraco.MigrationsClient.Models;
using Limbo.Umbraco.MigrationsClient.Models.Properties;
using Limbo.Umbraco.MigrationsClient.Models.Umbraco.NestedContent;
using Newtonsoft.Json.Linq;

namespace Limbo.Umbraco.MigrationsClient.Parsers.Community;

public interface INestedContentParser {

    NestedContentModel ParseModel(JArray array, ILegacyElement? content, ILegacyProperty? property);

}