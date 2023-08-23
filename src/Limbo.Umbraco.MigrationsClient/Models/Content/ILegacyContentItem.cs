using System.Collections.Generic;

namespace Limbo.Umbraco.MigrationsClient.Models.Content {

    public interface ILegacyContentItem : ILegacyEntityItem {

        IReadOnlyList<ILegacyContentItem> Children { get; }

    }

}