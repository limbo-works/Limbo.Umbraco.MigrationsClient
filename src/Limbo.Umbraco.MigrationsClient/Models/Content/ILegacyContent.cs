// ReSharper disable PossibleInterfaceMemberAmbiguity

using System.Collections.Generic;

namespace Limbo.Umbraco.MigrationsClient.Models.Content {

    public interface ILegacyContent : ILegacyEntity, ILegacyContentItem {

        IReadOnlyList<ILegacyContentItem> Path { get; }

    }

    public interface ILegacyContent<out TContent> : ILegacyContent, IJsonParsable<TContent> { }

}