using Newtonsoft.Json.Linq;
using System.Diagnostics.CodeAnalysis;

namespace Limbo.Umbraco.MigrationsClient.Models {

    public class LegacyMember : LegacyEntity, ILegacyContent<LegacyMember> {

        private LegacyMember(JObject json) : base(json) { }

        [return: NotNullIfNotNull("json")]
        public static LegacyMember? Parse(JObject? json) {
            return json == null ? null : new LegacyMember(json);
        }

    }

}