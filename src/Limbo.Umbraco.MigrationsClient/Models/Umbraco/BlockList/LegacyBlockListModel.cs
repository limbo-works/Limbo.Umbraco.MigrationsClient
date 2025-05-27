using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Limbo.Umbraco.MigrationsClient.Exceptions;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Umbraco.BlockList;

public class LegacyBlockListModel : IReadOnlyList<LegacyBlockListItem> {

    public JObject Source { get; }

    public int Count => Layout.UmbracoBlockList.Count;

    public LegacyBlockListItem this[int index] => Layout.UmbracoBlockList[index];

    public LegacyBlockListLayout Layout { get; }

    public LegacyBlockListPartList ContentData { get; }

    public LegacyBlockListPartList SettingsData { get; }

    public LegacyBlockListModel(JObject source, IReadOnlyList<LegacyBlockListItem> blocks, IReadOnlyList<LegacyBlockListPart> content, IReadOnlyList<LegacyBlockListPart> settings) {
        Source = source;
        Layout = new LegacyBlockListLayout(blocks);
        ContentData = new LegacyBlockListPartList(content);
        SettingsData = new LegacyBlockListPartList(settings);
    }

    public bool TryGetContentData(GuidUdi udi, [NotNullWhen(true)] out LegacyBlockListPart? result) {
        result = ContentData.FirstOrDefault(x => x.Udi == udi);
        return result is not null;
    }

    public bool TryGetSettingsData(GuidUdi? udi, [NotNullWhen(true)] out LegacyBlockListPart? result) {
        result = udi is null ? null : SettingsData.FirstOrDefault(x => x.Udi == udi);
        return result is not null;
    }

    public IEnumerator<LegacyBlockListItem> GetEnumerator() {
        return Layout.UmbracoBlockList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    [return: NotNullIfNotNull(nameof(source))]
    public static LegacyBlockListModel? Parse(JObject? source) {

        if (source is null) return null;

        List<LegacyBlockListItem> blocks = [];
        List<LegacyBlockListPart> contentData = [];
        List<LegacyBlockListPart> settingsData = [];

        foreach (JObject block in source.GetObjectArray("contentData")) {
            Guid contentTypeKey = block.GetGuid("contentTypeKey");
            GuidUdi udi = block.GetString("udi", GuidUdi.Parse)!;
            contentData.Add(new LegacyBlockListPart(contentTypeKey, udi, block));
        }

        foreach (JObject block in source.GetObjectArray("settingsData")) {
            Guid contentTypeKey = block.GetGuid("contentTypeKey");
            GuidUdi udi = block.GetString("udi", GuidUdi.Parse)!;
            settingsData.Add(new LegacyBlockListPart(contentTypeKey, udi, block));
        }

        foreach (JObject block in source.GetObject("layout")!.GetObjectArray("Umbraco.BlockList")) {

            GuidUdi contentUdi = block.GetString("contentUdi", GuidUdi.Parse)!;
            GuidUdi? settingsUdi = block.GetString("settingsUdi", GuidUdi.Parse);

            try {

                LegacyBlockListPart contentData2 = contentData.Single(x => x.Udi == contentUdi);

                LegacyBlockListPart? settingsData2 = null;

                blocks.Add(new LegacyBlockListItem(contentUdi, settingsUdi, contentData2, settingsData2));

            } catch (Exception ex) {

                throw new MigrationsParseException($"Failed parsing block:\r\n\r\nContent UDI: {contentUdi}\r\nSettings UDI: {settingsUdi}\r\n\r\n{block}\r\n\r\n{source}", ex);

            }

        }

        return new LegacyBlockListModel(source, blocks, contentData, settingsData);

    }

}