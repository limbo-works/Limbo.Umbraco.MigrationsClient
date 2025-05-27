using System.Collections.Generic;

namespace Limbo.Umbraco.MigrationsClient.Models.Umbraco.BlockList;

public class LegacyBlockListLayout {

    public IReadOnlyList<LegacyBlockListItem> UmbracoBlockList { get; }

    public LegacyBlockListLayout(IReadOnlyList<LegacyBlockListItem> blocks) {
        UmbracoBlockList = blocks;
    }

}