using System.Collections.Generic;

namespace Limbo.Umbraco.MigrationsClient.Models.Media {

    public interface ILegacyMediaItem : ILegacyEntityItem {

        string Url { get; }

        IReadOnlyList<ILegacyMediaItem> Children { get; }

    }

}
