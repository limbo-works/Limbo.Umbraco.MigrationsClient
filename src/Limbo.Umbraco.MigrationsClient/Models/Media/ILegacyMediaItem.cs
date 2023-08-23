using System.Collections.Generic;

namespace Limbo.Umbraco.MigrationsClient.Models.Media {

    public interface ILegacyMediaItem : ILegacyEntityItem {

        IReadOnlyList<ILegacyMediaItem> Children { get; }

    }

}
