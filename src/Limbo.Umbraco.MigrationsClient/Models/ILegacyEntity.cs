using System;

namespace Limbo.Umbraco.MigrationsClient.Models {

    public interface ILegacyEntity {

        int Id { get; }

        Guid Key { get; }

        string Name { get; }

        string Url { get; }

        string Type { get; }

    }

}