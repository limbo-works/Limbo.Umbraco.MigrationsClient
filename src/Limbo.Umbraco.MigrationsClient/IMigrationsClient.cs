using System;
using System.Collections.Generic;
using Limbo.Umbraco.MigrationsClient.Models;
using Limbo.Umbraco.MigrationsClient.Models.ContentTypes;

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

    IReadOnlyList<LegacyContentItem> GetContentAtRoot();

    LegacyContent GetContentById(int id);

    TContent GetContentById<TContent>(int id) where TContent : ILegacyContent<TContent>;

    LegacyContent GetContentByKey(Guid key);

    TContent GetContentByKey<TContent>(Guid key) where TContent : ILegacyContent<TContent>;

    IReadOnlyList<LegacyContentItem> GetMediaAtRoot();

    LegacyMedia GetMediaById(int id);

    LegacyMedia GetMediaByKey(Guid key);

    IReadOnlyList<LegacyMember> GetAllMembers();

    byte[] GetBytes(LegacyMedia media);

    void DownloadBytes(LegacyMedia media, string path);

}