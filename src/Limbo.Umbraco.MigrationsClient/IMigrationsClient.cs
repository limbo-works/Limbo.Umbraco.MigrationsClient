using System;
using System.Collections.Generic;
using Limbo.Umbraco.MigrationsClient.Models;
using Limbo.Umbraco.MigrationsClient.Responses;

namespace Limbo.Umbraco.MigrationsClient;

public interface IMigrationsClient {

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