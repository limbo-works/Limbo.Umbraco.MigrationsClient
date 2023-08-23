using System;

namespace Limbo.Umbraco.MigrationsClient.Models {

    public interface ILegacyEntityItem {

        int Id { get; }

        Guid Key { get; }

        string Name { get; }

    }

}