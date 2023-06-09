using System;
using System.Collections.Generic;
using Limbo.Umbraco.MigrationsClient.Models;
using Limbo.Umbraco.MigrationsClient.Responses;

namespace Limbo.Umbraco.MigrationsClient;

public interface IMigrationsHttpClient {

    IMigrationsResponse<IReadOnlyList<LegacyContentItem>> GetContentAtRoot();

    IMigrationsResponse<LegacyContent> GetContentById(int id);

    IMigrationsResponse<TContent> GetContentById<TContent>(int id) where TContent : ILegacyContent<TContent>;

    IMigrationsResponse<LegacyContent> GetContentByKey(Guid key);

    IMigrationsResponse<TContent> GetContentByKey<TContent>(Guid key) where TContent : ILegacyContent<TContent>;

    IMigrationsResponse<IReadOnlyList<LegacyContentItem>> GetMediaAtRoot();

    IMigrationsResponse<LegacyMedia> GetMediaById(int id);

    IMigrationsResponse<LegacyMedia> GetMediaByKey(Guid key);

    IMigrationsResponse<IReadOnlyList<LegacyMember>> GetAllMembers();

    byte[] GetBytes(LegacyMedia media);

    void DownloadBytes(LegacyMedia media, string path);

}