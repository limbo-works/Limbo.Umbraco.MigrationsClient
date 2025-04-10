using System;
using System.Collections.Generic;
using Limbo.Umbraco.MigrationsClient.Models.Content;
using Limbo.Umbraco.MigrationsClient.Models.ContentTypes;
using Limbo.Umbraco.MigrationsClient.Models.DataTypes;
using Limbo.Umbraco.MigrationsClient.Models.Media;
using Limbo.Umbraco.MigrationsClient.Models.Members;
using Limbo.Umbraco.MigrationsClient.Models.Umbraco.Grid;

namespace Limbo.Umbraco.MigrationsClient;

public class MigrationsClient : IMigrationsClient {

    public MigrationsHttpClient HttpClient { get; }

    public MigrationsClient(MigrationsHttpClient httpClient) {
        HttpClient = httpClient;
    }

    public MigrationsClient(string scheme, string host, string apiKey) {
        HttpClient = new MigrationsHttpClient(scheme, host, apiKey);
    }

    #region Public member methods

    /// <summary>
    /// Returns a list of all data types.
    /// </summary>
    /// <returns>A list of <see cref="LegacyDataType"/>.</returns>
    public IReadOnlyList<LegacyDataType> GetDataTypes() {
        return HttpClient.GetDataTypes().Body;
    }

    /// <summary>
    /// Returns the data type with the specified <paramref name="id"/>.
    /// </summary>
    /// <param name="id">The ID of the data type.</param>
    /// <returns>An instance of <see cref="LegacyDataType"/>.</returns>
    public LegacyDataType GetDataTypeById(int id) {
        return HttpClient.GetDataTypeById(id).Body;
    }

    /// <summary>
    /// Returns the data type with the specified <paramref name="key"/>.
    /// </summary>
    /// <param name="key">The key of the data type.</param>
    /// <returns>An instance of <see cref="LegacyDataType"/>.</returns>
    public LegacyDataType GetDataTypeByKey(Guid key) {
        return HttpClient.GetDataTypeByKey(key).Body;
    }

    /// <summary>
    /// Returns a list of all content types.
    /// </summary>
    /// <returns>A list of <see cref="LegacyContentType"/>.</returns>
    public IReadOnlyList<LegacyContentType> GetContentTypes() {
        return HttpClient.GetContentTypes().Body;
    }

    public virtual LegacyContentType GetContentTypeById(int id) {
        return HttpClient.GetContentTypeById(id).Body;
    }

    public virtual LegacyContentType GetContentTypeByKey(Guid key) {
        return HttpClient.GetContentTypeByKey(key).Body;
    }

    public virtual LegacyContentType GetContentTypeByAlias(string alias) {
        return HttpClient.GetContentTypeByAlias(alias).Body;
    }

    public IReadOnlyList<LegacyContentItem> GetContentAtRoot(int? maxLevel = null) {
        return HttpClient.GetContentAtRoot(maxLevel).Body;
    }

    public LegacyContent GetContentById(int id, int? maxLevel = null) {
        return HttpClient.GetContentById(id, maxLevel).Body;
    }

    public TContent GetContentById<TContent>(int id, int? maxLevel = null) where TContent : ILegacyContent<TContent> {
        return HttpClient.GetContentById<TContent>(id, maxLevel).Body;
    }

    public LegacyContent GetContentByKey(Guid key, int? maxLevel = null) {
        return HttpClient.GetContentByKey(key, maxLevel).Body;
    }

    public TContent GetContentByKey<TContent>(Guid key, int? maxLevel = null) where TContent : ILegacyContent<TContent> {
        return HttpClient.GetContentByKey<TContent>(key, maxLevel).Body;
    }

    public IReadOnlyList<LegacyMediaItem> GetMediaAtRoot(int? maxLevel = null) {
        return HttpClient.GetMediaAtRoot(maxLevel).Body;
    }

    public LegacyMedia GetMediaById(int id, int? maxLevel = null) {
        return HttpClient.GetMediaById(id, maxLevel).Body;
    }

    public LegacyMedia GetMediaByKey(Guid key, int? maxLevel = null) {
        return HttpClient.GetMediaByKey(key, maxLevel).Body;
    }

    public LegacyMedia GetMediaByPath(string path, int? maxLevel = null) {
        return HttpClient.GetMediaByPath(path, maxLevel).Body;
    }

    public IReadOnlyList<LegacyMember> GetAllMembers() {
        return HttpClient.GetAllMembers().Body;
    }

    public virtual byte[] GetBytes(LegacyMedia media) {
        return HttpClient.GetBytes(media);
    }

    public virtual void DownloadBytes(LegacyMedia media, string path) {
        HttpClient.DownloadBytes(media, path);
    }

    public IReadOnlyList<LegacyGridEditor> GetGridEditors() {
        return HttpClient.GetGridEditors().Body;
    }

    #endregion

}