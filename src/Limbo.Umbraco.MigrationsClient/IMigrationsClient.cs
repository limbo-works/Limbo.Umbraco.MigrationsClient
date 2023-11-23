using System;
using System.Collections.Generic;
using Limbo.Umbraco.MigrationsClient.Models;
using Limbo.Umbraco.MigrationsClient.Models.Content;
using Limbo.Umbraco.MigrationsClient.Models.ContentTypes;
using Limbo.Umbraco.MigrationsClient.Models.Media;
using Limbo.Umbraco.MigrationsClient.Models.Members;

namespace Limbo.Umbraco.MigrationsClient;

public interface IMigrationsClient {

    /// <summary>
    /// Returns the content type with the specified <paramref name="id"/>.
    /// </summary>
    /// <param name="id">The ID of the content type.</param>
    /// <returns>An instance of <see cref="LegacyContentType"/>.</returns>
    LegacyContentType GetContentTypeById(int id);

    /// <summary>
    /// Returns the content type with the specified <paramref name="key"/>.
    /// </summary>
    /// <param name="key">The key of the content type.</param>
    /// <returns>An instance of <see cref="LegacyContentType"/>.</returns>
    LegacyContentType GetContentTypeByKey(Guid key);

    /// <summary>
    /// Returns the content type with the specified <paramref name="alias"/>.
    /// </summary>
    /// <param name="alias">The alias of the content type.</param>
    /// <returns>An instance of <see cref="LegacyContentType"/>.</returns>
    LegacyContentType GetContentTypeByAlias(string alias);

    IReadOnlyList<LegacyContentItem> GetContentAtRoot(int? maxLevel = null);

    LegacyContent GetContentById(int id, int? maxLevel = null);

    TContent GetContentById<TContent>(int id, int? maxLevel = null) where TContent : ILegacyContent<TContent>;

    LegacyContent GetContentByKey(Guid key, int? maxLevel = null);

    TContent GetContentByKey<TContent>(Guid key, int? maxLevel = null) where TContent : ILegacyContent<TContent>;

    IReadOnlyList<LegacyMediaItem> GetMediaAtRoot(int? maxLevel = null);

    LegacyMedia GetMediaById(int id, int? maxLevel = null);

    LegacyMedia GetMediaByKey(Guid key, int? maxLevel = null);

    IReadOnlyList<LegacyMember> GetAllMembers();

    byte[] GetBytes(LegacyMedia media);

    void DownloadBytes(LegacyMedia media, string path);

}