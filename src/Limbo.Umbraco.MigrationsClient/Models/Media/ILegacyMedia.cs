// ReSharper disable PossibleInterfaceMemberAmbiguity

using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Limbo.Umbraco.MigrationsClient.Models.Media {

    public interface ILegacyMedia : ILegacyEntity, ILegacyMediaItem {

        int Width { get; }

        int Height { get; }

        int Size { get; }

        JToken? UmbracoFile { get; }

        IReadOnlyList<ILegacyMediaItem> Path { get; }

    }

}