using System;
using System.Collections.Generic;
using System.Linq;
using Limbo.Umbraco.MigrationsClient.Exceptions;
using Limbo.Umbraco.MigrationsClient.Models.Content;
using Limbo.Umbraco.MigrationsClient.Models.Media;
using Limbo.Umbraco.MigrationsClient.Models.Properties;
using Limbo.Umbraco.MigrationsClient.Models.Skybrud.Elements;
using Limbo.Umbraco.MigrationsClient.Models.Skybrud.Grid;
using Limbo.Umbraco.MigrationsClient.Models.Skybrud.LinkPicker;
using Limbo.Umbraco.MigrationsClient.Models.Umbraco;
using Limbo.Umbraco.MigrationsClient.Models.Umbraco.NestedContent;
using Limbo.Umbraco.MigrationsClient.Parsers.Community;
using Limbo.Umbraco.MigrationsClient.Parsers.Skybrud;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Collections.Extensions;
using Skybrud.Essentials.Strings;

namespace Limbo.Umbraco.MigrationsClient.Models;

public static class LegacyElementExtensions {

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
        if (!content.TryGetProperty(propertyAlias, out var property)) return 0;
        return property.Value.Type switch {
            JTokenType.Integer => property.Value.Value<int>(),
            JTokenType.String => StringUtils.ParseInt32(property.Value.ToString()),
            _ => 0
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

    public static string[] GetStringArray(this ILegacyElement content, string propertyAlias) {

        if (!content.TryGetProperty(propertyAlias, out ILegacyProperty? property)) return [];

        switch (property.Value.Type) {

            case JTokenType.Array:
                return property.Value.Value<JArray>().SelectArray(x => x.ToString());

            case JTokenType.String:
                return StringUtils.ParseStringArray(property.Value.Value<string>());

            default:
                return [];

        }

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

    public static ElementsModel? GetElements(this ILegacyElement content, string propertyAlias, SkybrudElementsParser parser) {
        if (!content.TryGetProperty(propertyAlias, out ILegacyProperty? property)) return null;
        return property.Value switch {
            JArray array => parser.ParseElements(array, content, property),
            _ => null
        };
    }

    /// <summary>
    /// Returns the first <see cref="ElementsItem"/> of the elements of the property with the specified <paramref name="propertyAlias"/>, or <see langword="null"/> if not found.
    /// </summary>
    /// <param name="content">The page or element that holds the property.</param>
    /// <param name="propertyAlias">The alias of the property.</param>
    /// <param name="parser">The elements parser.</param>
    /// <returns>An instance <see cref="ElementsItem"/>.</returns>
    public static ElementsItem? GetElementItem(this ILegacyElement content, string propertyAlias, SkybrudElementsParser parser) {
        return GetElements(content, propertyAlias, parser)?.Items.FirstOrDefault();
    }

    /// <summary>
    /// Returns the first element of the elements of the property with the specified <paramref name="propertyAlias"/>.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="content">The page or element that holds the property.</param>
    /// <param name="propertyAlias">The alias of the property.</param>
    /// <param name="parser">The elements parser.</param>
    /// <returns>An instance of <typeparamref name="T"/>.</returns>
    public static T? GetElementItem<T>(this ILegacyElement content, string propertyAlias, SkybrudElementsParser parser) where T : ElementsItem {

        ElementsItem? item = GetElements(content, propertyAlias, parser)?.Items.FirstOrDefault();
        if (item is null) return null;

        if (item is not T t) throw new MigrationsParseException($"An item is not of expected type '{typeof(T)}', got '{item.GetType()}' instead...\r\n\r\n{JObject.FromObject(item)}");

        return t;

    }

    /// <summary>
    /// Returns a list of <see cref="ElementsItem"/> representing the elements of the property with the specified <paramref name="propertyAlias"/>.
    /// </summary>
    /// <param name="content">The page or element that holds the property.</param>
    /// <param name="propertyAlias">The alias of the property.</param>
    /// <param name="parser">The elements parser.</param>
    /// <returns>A list of <see cref="ElementsItem"/>.</returns>
    public static IReadOnlyList<ElementsItem> GetElementItems(this ILegacyElement content, string propertyAlias, SkybrudElementsParser parser) {
        return GetElements(content, propertyAlias, parser)?.Items ?? [];
    }

    /// <summary>
    /// Returns a list of <typeparamref name="T"/> representing the elements of the property with the specified <paramref name="propertyAlias"/>.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <param name="content">The page or element that holds the property.</param>
    /// <param name="propertyAlias">The alias of the property.</param>
    /// <param name="parser">The elements parser.</param>
    /// <returns>A list of <typeparamref name="T"/>.</returns>
    public static IReadOnlyList<T> GetElementItems<T>(this ILegacyElement content, string propertyAlias, SkybrudElementsParser parser) where T : ElementsItem {

        IReadOnlyList<ElementsItem> source = GetElementItems(content, propertyAlias, parser);

        if (source.Count == 0) return [];

        List<T> temp = [];
        foreach (ElementsItem item in source) {
            if (item is not T t) throw new MigrationsParseException($"An item is not of expected type '{typeof(T)}', got '{item.GetType()}' instead...\r\n\r\n{JObject.FromObject(item)}");
            temp.Add(t);
        }

        return temp;

    }

    #endregion

    /// <summary>
    /// Returns whether the <paramref name="content"/> is a descendant of the content with the specified <paramref name="id"/>.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <param name="id">The ID of the ancestor.</param>
    /// <returns><see langword="true"/> if <paramref name="content"/> is a descendant; otherwise, <see langword="false"/>.</returns>
    public static bool IsDescendant(this ILegacyContent content, int id) {
        return content.Path.Any(x => x.Id == id);
    }

    /// <summary>
    /// Returns whether the <paramref name="content"/> is either a descendant or exact match to the content with the specified <paramref name="id"/>.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <param name="id">The ID of the ancestor.</param>
    /// <returns><see langword="true"/> if <paramref name="content"/> is a descendant or matches <paramref name="content"/>; otherwise, <see langword="false"/>.</returns>
    public static bool IsDescendantOrSelf(this ILegacyContent content, int id) {
        return content.Id == id || content.Path.Any(x => x.Id == id);
    }

    /// <summary>
    /// Returns whether the <paramref name="media"/> is a descendant of the media with the specified <paramref name="id"/>.
    /// </summary>
    /// <param name="media">The media.</param>
    /// <param name="id">The ID of the ancestor.</param>
    /// <returns><see langword="true"/> if <paramref name="media"/> is a descendant; otherwise, <see langword="false"/>.</returns>
    public static bool IsDescendant(this ILegacyMedia media, int id) {
        return media.Path.Any(x => x.Id == id);
    }

    /// <summary>
    /// Returns whether the <paramref name="media"/> is either a descendant or exact match to the media with the specified <paramref name="id"/>.
    /// </summary>
    /// <param name="media">The media.</param>
    /// <param name="id">The ID of the ancestor.</param>
    /// <returns><see langword="true"/> if <paramref name="media"/> is a descendant or matches <paramref name="media"/>; otherwise, <see langword="false"/>.</returns>
    public static bool IsDescendantOrSelf(this ILegacyMedia media, int id) {
        return media.Id == id || media.Path.Any(x => x.Id == id);
    }

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

    /// <summary>
    /// Returns a <see cref="NestedContentModel"/> representing the value of the property with the specified <paramref name="propertyAlias"/>.
    /// </summary>
    /// <param name="content">The legacy content/page.</param>
    /// <param name="propertyAlias">The alias of the property.</param>
    /// <param name="parser">An instance of <see cref="INestedContentParser"/> for parsing the JSON value.</param>
    /// <returns>An instance of <see cref="NestedContentModel"/> if successful; otherwise, <see langword="null"/>.</returns>
    public static NestedContentModel? GetNestedContentModel(this LegacyContent content, string propertyAlias, INestedContentParser parser) {
        if (!content.TryGetProperty(propertyAlias, out ILegacyProperty? property)) return null;
        if (property.Value.Type == JTokenType.Null) return null;
        if (property.Value is not JArray array) throw new MigrationsParseException("Property value must be an instance of 'JArray'.");
        return parser.ParseModel(array, content, property);
    }

    /// <summary>
    /// Gets a list of <typeparamref name="TItem"/> representing the value of the property with the specified <paramref name="propertyAlias"/>.
    /// </summary>
    /// <typeparam name="TItem">The type of the items.</typeparam>
    /// <param name="content">The legacy content/page.</param>
    /// <param name="propertyAlias">The alias of the property.</param>
    /// <param name="parser">An instance of <see cref="INestedContentParser"/> for parsing the JSON value.</param>
    /// <returns>A list of <typeparamref name="TItem"/>.</returns>
    public static IReadOnlyList<TItem> GetNestedContentItems<TItem>(this LegacyContent content, string propertyAlias, INestedContentParser parser) where TItem : NestedContentItem {

        NestedContentModel? model = GetNestedContentModel(content, propertyAlias, parser);
        if (model is null) return [];

        List<TItem> temp = [];
        foreach (NestedContentItem item in model) {

            if (item is not TItem t) throw new MigrationsParseException($"An item is not of expected type '{typeof(TItem)}', got '{item.GetType()}' instead...\r\n\r\n{JObject.FromObject(item)}");
            temp.Add(t);

        }

        return temp;

    }

}