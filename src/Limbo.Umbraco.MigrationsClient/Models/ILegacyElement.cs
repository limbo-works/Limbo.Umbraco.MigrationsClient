using System;
using System.Collections.Generic;
using Limbo.Umbraco.MigrationsClient.Models.Properties;

namespace Limbo.Umbraco.MigrationsClient.Models {

    public interface ILegacyElement {

        Guid Key { get; }

        string Name { get; }

        string ContentTypeAlias { get; }

        IReadOnlyList<ILegacyProperty> Properties { get; }

    }

}