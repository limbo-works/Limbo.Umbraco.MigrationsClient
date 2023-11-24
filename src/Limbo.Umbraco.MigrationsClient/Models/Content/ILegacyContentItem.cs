using System.Collections.Generic;

namespace Limbo.Umbraco.MigrationsClient.Models.Content {

    public interface ILegacyContentItem : ILegacyEntityItem {

        string Url { get; }

        IReadOnlyList<ILegacyContentItem> Children { get; }

    }

}