using System;

namespace Limbo.Umbraco.MigrationsClient.Models.Umbraco;

public static class GuidUdiExtensions {

    public static GuidUdi ToUdi(this Guid guid, string entityType) {
        return new GuidUdi(entityType, guid);
    }

}