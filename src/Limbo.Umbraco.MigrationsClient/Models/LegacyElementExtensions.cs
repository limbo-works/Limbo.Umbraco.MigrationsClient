using System;
using System.Collections.Generic;
using Limbo.Umbraco.MigrationsClient.Models.Content;
using Limbo.Umbraco.MigrationsClient.Models.Media;
using Limbo.Umbraco.MigrationsClient.Models.Properties;
using Limbo.Umbraco.MigrationsClient.Models.Skybrud.Elements;
using Limbo.Umbraco.MigrationsClient.Models.Skybrud.Grid;
using Limbo.Umbraco.MigrationsClient.Models.Skybrud.LinkPicker;
using Limbo.Umbraco.MigrationsClient.Models.Umbraco;
using Limbo.Umbraco.MigrationsClient.Parsers.Skybrud;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Strings;

namespace Limbo.Umbraco.MigrationsClient.Models;

public static class LegacyElementExtensions {

    private static SkybrudElementsParser? _elementsParser;
    private static SkybrudGridDataParser? _gridDataParser;

    public static bool GetBoolean(this ILegacyElement content, string propertyAlias) {
        if (!content.TryGetProperty(propertyAlias, out var property)) return false;
        return property.Value.Type switch {
            JTokenType.Boolean => property.Value.Value<bool>(),
            JTokenType.Integer => property.Value.Value<int>() == 1,
            JTokenType.String => StringUtils.ParseBoolean(property.Value.ToString()),
            _ => false
        };
    }

    public static int GetInt32(this ILegacyElement content, string propertyAlias) {
        if (!content.TryGetProperty(propertyAlias, out var property)) return default;
        return property.Value.Type switch {
            JTokenType.Integer => property.Value.Value<int>(),
            JTokenType.String => StringUtils.ParseInt32(property.Value.ToString()),
            _ => default
        };
    }

    public static int? GetInt32OrNull(this ILegacyElement content, string propertyAlias) {
        if (!content.TryGetProperty(propertyAlias, out var property)) return null;
        return property.Value.Type switch {
            JTokenType.Integer => property.Value.Value<int>(),
            JTokenType.String => StringUtils.ParseInt32OrNull(property.Value.ToString()),
            _ => null
        };
    }

    public static string? GetString(this ILegacyElement content, string propertyAlias) {
        if (!content.TryGetProperty(propertyAlias, out var property)) return null;
        return property.Value.Type == JTokenType.Null ? null : property.Value.ToString();
    }

    public static T? GetString<T>(this ILegacyElement content, string propertyAlias, Func<string, T> callback) {
        if (!content.TryGetProperty(propertyAlias, out var property)) return default;
        return property.Value.Type == JTokenType.Null ? default : callback(property.Value.ToString());
    }

    public static GuidUdi? GetGuidUdi(this ILegacyElement content, string propertyAlias) {
        if (!content.TryGetProperty(propertyAlias, out var property)) return null;
        return property.Value.Type switch {
            JTokenType.String => GuidUdi.Parse(property.Value.ToString()),
            _ => null
        };
    }

    public static GuidUdiList? GetGuidUdiList(this ILegacyElement content, string propertyAlias) {
        if (!content.TryGetProperty(propertyAlias, out var property)) return null;
        return property.Value.Type switch {
            JTokenType.String => GuidUdiList.Parse(property.Value.ToString())?.NullIfEmpty(),
            _ => null
        };
    }

    public static JArray? GetArray(this ILegacyElement content, string propertyAlias) {
        if (!content.TryGetProperty(propertyAlias, out var property)) return null;
        return property.Value switch {
            JArray array => array,
            _ => null
        };
    }

    public static T? GetArray<T>(this ILegacyElement content, string propertyAlias, Func<JArray, T> callback) {
        if (!content.TryGetProperty(propertyAlias, out var property)) return default;
        return property.Value switch {
            JArray array => callback(array),
            _ => default
        };
    }

    public static JObject? GetObject(this ILegacyElement content, string propertyAlias) {
        if (!content.TryGetProperty(propertyAlias, out var property)) return null;
        return property.Value switch {
            JObject obj => obj,
            _ => null
        };
    }

    public static T? GetObject<T>(this ILegacyElement content, string propertyAlias, Func<JObject, T> callback) {
        if (!content.TryGetProperty(propertyAlias, out var property)) return default;
        return property.Value switch {
            JObject obj => callback(obj),
            _ => default
        };
    }

    public static JToken GetToken(this ILegacyElement content, string propertyAlias) {
        return !content.TryGetProperty(propertyAlias, out var property) ? JToken.Parse("null") : property.Value;
    }

    #region Skybrud

    public static GridDataModel? GetGridData(this ILegacyElement content, string propertyAlias) {
        _gridDataParser = new SkybrudGridDataParser();
        return GetGridData(content, propertyAlias, _gridDataParser);
    }

    public static GridDataModel? GetGridData(this ILegacyElement content, string propertyAlias, SkybrudGridDataParser parser) {
        if (!content.TryGetProperty(propertyAlias, out ILegacyProperty? property)) return null;
        return property.Value switch {
            JObject obj => parser.ParseGridModel(obj, content, property),
            _ => null
        };
    }

    public static LinkPickerItem? GetLinkPickerItem(this ILegacyElement content, string propertyAlias) {
        if (!content.TryGetProperty(propertyAlias, out var property)) return null;
        return property.Value switch {
            JObject obj => LinkPickerItem.Parse(obj),
            _ => null
        };
    }

    public static ElementsModel? GetElements(this ILegacyElement content, string propertyAlias) {
        _elementsParser ??= new SkybrudElementsParser();
        return GetElements(content, propertyAlias, _elementsParser);
    }

    public static ElementsModel? GetElements(this ILegacyElement content, string propertyAlias, SkybrudElementsParser parser) {
        if (!content.TryGetProperty(propertyAlias, out ILegacyProperty? property)) return null;
        return property.Value switch {
            JArray array => parser.ParseElements(array, content, property),
            _ => null
        };
    }

    #endregion

    public static List<ILegacyContentItem> GetDescendants(this ILegacyContent content) {

        List<ILegacyContentItem> descendants = [];

        foreach (ILegacyContentItem child in content.Children) {
            descendants.Add(child);
            GetDescendants(child, descendants);
        }

        return descendants;

    }

    public static List<ILegacyContentItem> GetDescendants(this ILegacyContentItem content) {

        List<ILegacyContentItem> descendants = [];

        GetDescendants(content, descendants);

        return descendants;

    }

    public static List<ILegacyContentItem> GetDescendants(this IEnumerable<ILegacyContentItem> content) {

        List<ILegacyContentItem> descendants = [];

        foreach (ILegacyContentItem item in content) {
            GetDescendants(item, descendants);
        }

        return descendants;

    }

    public static List<ILegacyContentItem> GetDescendantsAndSelf(this IEnumerable<ILegacyContentItem> content) {

        List<ILegacyContentItem> descendants = [];

        foreach (ILegacyContentItem item in content) {
            descendants.Add(item);
            GetDescendants(item, descendants);
        }

        return descendants;

    }

    public static List<ILegacyContentItem> GetDescendantsAndSelf(this ILegacyContent content) {

        List<ILegacyContentItem> descendants = [content];

        foreach (ILegacyContentItem child in content.Children) {
            descendants.Add(child);
            GetDescendants(child, descendants);
        }

        return descendants;

    }

    private static void GetDescendants(ILegacyContentItem content, List<ILegacyContentItem> list) {

        foreach (ILegacyContentItem child in content.Children) {
            list.Add(child);
            GetDescendants(child, list);
        }

    }


    public static List<ILegacyMediaItem> GetDescendants(this ILegacyMedia media) {

        List<ILegacyMediaItem> descendants = [];

        foreach (ILegacyMediaItem child in media.Children) {
            descendants.Add(child);
            GetDescendants(child, descendants);
        }

        return descendants;

    }

    public static List<ILegacyMediaItem> GetDescendants(this ILegacyMediaItem media) {

        List<ILegacyMediaItem> descendants = [];

        GetDescendants(media, descendants);

        return descendants;

    }

    public static List<ILegacyMediaItem> GetDescendantsAndSelf(this IEnumerable<ILegacyMediaItem> content) {

        List<ILegacyMediaItem> descendants = [];

        foreach (ILegacyMediaItem item in content) {
            descendants.Add(item);
            GetDescendants(item, descendants);
        }

        return descendants;

    }

    public static List<ILegacyMediaItem> GetDescendantsAndSelf(this ILegacyMedia media) {

        List<ILegacyMediaItem> descendants = [media];

        foreach (ILegacyMediaItem child in media.Children) {
            descendants.Add(child);
            GetDescendants(child, descendants);
        }

        return descendants;

    }

    private static void GetDescendants(ILegacyMediaItem media, List<ILegacyMediaItem> list) {

        foreach (ILegacyMediaItem child in media.Children) {
            list.Add(child);
            GetDescendants(child, list);
        }

    }

}