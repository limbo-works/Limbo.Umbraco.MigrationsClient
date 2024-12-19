using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.Redirects;

/// <summary>
/// Class representing the model for an outbound redirect in our <strong>Skybrud.Umbraco.Redirects</strong> package.
/// </summary>
public class OutboundRedirect : LegacyObjectBase {

    /// <summary>
    /// Gets whether the redirect is permanent.
    /// </summary>
    public bool IsPermanent { get; }

    /// <summary>
    /// Gets the destination of the redirect.
    /// </summary>
    public RedirectDestination Destination { get; }

    public OutboundRedirect(RedirectDestination destination, bool permanent) : base(null) {
        Destination = destination;
        IsPermanent = permanent;
    }

    private OutboundRedirect(JObject json, RedirectDestination destination) : base(json) {
        IsPermanent = json.GetBoolean("permanent");
        Destination = destination;
    }

    public static OutboundRedirect? Parse(JObject json) {
        RedirectDestination? destination = json.GetObject("destination", RedirectDestination.Parse);
        return destination == null ? null : new OutboundRedirect(json, destination);
    }

}