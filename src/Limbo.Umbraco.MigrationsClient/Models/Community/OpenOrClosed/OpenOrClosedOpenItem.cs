using System;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Community.OpenOrClosed;

public class OpenOrClosedOpenItem {

    public Guid Id { get; }

    public TimeSpan OpensAt { get; }

    public TimeSpan ClosesAt { get; }

    private OpenOrClosedOpenItem(Guid id, TimeSpan opensAt, TimeSpan closesAt) {
        Id = id;
        OpensAt = opensAt;
        ClosesAt = closesAt;
    }

    public static OpenOrClosedOpenItem Parse(JObject source) {

        Guid id = source.GetGuid("id");
        TimeSpan opensAt = source.GetString("opensAt", TimeSpan.Parse);
        TimeSpan closesAt = source.GetString("closesAt", TimeSpan.Parse);

        return new OpenOrClosedOpenItem(id, opensAt, closesAt);

    }

}