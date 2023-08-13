using System;
using System.Collections.Generic;
using System.IO;
using Limbo.Umbraco.MigrationsClient.Models;
using Limbo.Umbraco.MigrationsClient.Models.ContentTypes;
using Limbo.Umbraco.MigrationsClient.Responses;
using Skybrud.Essentials.Http;
using Skybrud.Essentials.Http.Client;
using Skybrud.Essentials.Http.Collections;

namespace Limbo.Umbraco.MigrationsClient {

    public class MigrationsHttpClient : HttpClient {

        public string Scheme { get; }

        public string Host { get; }

        protected string ApiKey { get; }

        public MigrationsHttpClient(string scheme, string host, string apiKey) {
            Scheme = scheme;
            Host = host;
            ApiKey = apiKey;
        }

        #region Public member methods

        public virtual IMigrationsResponse<LegacyContentType> GetContentTypeById(int id) {
            HttpQueryString query = new() { { "id", id } };
            return new MigrationsResponse<LegacyContentType>(Get("/umbraco/Limbo/Migrations/GetContentTypeById", query));
        }

        public virtual IMigrationsResponse<LegacyContentType> GetContentTypeByKey(Guid key) {
            return new MigrationsResponse<LegacyContentType>(Get($"/umbraco/Limbo/Migrations/GetContentTypeByKey?key={key}"));
        }

        public virtual IMigrationsResponse<LegacyContentType> GetContentTypeByAlias(string alias) {
            return new MigrationsResponse<LegacyContentType>(Get($"/umbraco/Limbo/Migrations/GetContentTypeByAlias?alias={alias}"));
        }

        public IMigrationsResponse<IReadOnlyList<LegacyContentItem>> GetContentAtRoot(int? maxLevel = null) {
            HttpQueryString query = new();
            if (maxLevel is not null) query.Add("maxLevel", maxLevel);
            return new MigrationsListResponse<LegacyContentItem>(Get("/umbraco/Limbo/Migrations/GetContentAtRoot", query));
        }

        public IMigrationsResponse<LegacyContent> GetContentById(int id, int? maxLevel = null) {
            HttpQueryString query = new() { { "id", id } };
            if (maxLevel is not null) query.Add("maxLevel", maxLevel);
            return new MigrationsResponse<LegacyContent>(Get("/umbraco/Limbo/Migrations/GetContentById", query));
        }

        public IMigrationsResponse<TContent> GetContentById<TContent>(int id, int? maxLevel = null) where TContent : ILegacyContent<TContent> {
            HttpQueryString query = new() { { "id", id } };
            if (maxLevel is not null) query.Add("maxLevel", maxLevel);
            return new MigrationsResponse<TContent>(Get("/umbraco/Limbo/Migrations/GetContentById", query));
        }

        public IMigrationsResponse<LegacyContent> GetContentByKey(Guid key, int? maxLevel = null) {
            HttpQueryString query = new() { { "key", key } };
            if (maxLevel is not null) query.Add("maxLevel", maxLevel);
            return new MigrationsResponse<LegacyContent>(Get("/umbraco/Limbo/Migrations/GetContentByKey", query));
        }

        public IMigrationsResponse<TContent> GetContentByKey<TContent>(Guid key, int? maxLevel = null) where TContent : ILegacyContent<TContent> {
            HttpQueryString query = new() { { "key", key } };
            if (maxLevel is not null) query.Add("maxLevel", maxLevel);
            return new MigrationsResponse<TContent>(Get("/umbraco/Limbo/Migrations/GetContentById", query));
        }

        public virtual IMigrationsResponse<IReadOnlyList<LegacyContentItem>> GetMediaAtRoot(int? maxLevel = null) {
            HttpQueryString query = new();
            if (maxLevel is not null) query.Add("maxLevel", maxLevel);
            return new MigrationsListResponse<LegacyContentItem>(Get("/umbraco/Limbo/Migrations/GetMediaAtRoot", query));
        }

        public virtual IMigrationsResponse<LegacyMedia> GetMediaById(int id, int? maxLevel = null) {

            HttpQueryString query = new() { { "id", id } };
            if (maxLevel is not null) query.Add("maxLevel", maxLevel);

            const string url = "/umbraco/Limbo/Migrations/GetMediaById";

            try {
                return new MigrationsResponse<LegacyMedia>(Get(url, query));
            } catch (Exception ex) {
                throw new Exception($"Failed getting media with ID {id} -> {url}?{query}", ex);
            }

        }

        public virtual IMigrationsResponse<LegacyMedia> GetMediaByKey(Guid key, int? maxLevel = null) {

            HttpQueryString query = new() { { "key", key } };
            if (maxLevel is not null) query.Add("maxLevel", maxLevel);

            const string url = "/umbraco/Limbo/Migrations/GetMediaByKey";

            try {
                return new MigrationsResponse<LegacyMedia>(Get(url, query));
            } catch (Exception ex) {
                throw new Exception($"Failed getting media with key {key} -> {url}?{query}", ex);
            }

        }

        public IMigrationsResponse<IReadOnlyList<LegacyMember>> GetAllMembers() {
            return new MigrationsListResponse<LegacyMember>(Get("/umbraco/Limbo/Migrations/GetAllMembers"));
        }

        protected virtual string GetMediaUrl(LegacyMedia media) {
            return $"{Scheme}://{Host}{media.Url}";
        }

        public virtual byte[] GetBytes(LegacyMedia media) {
            string url = GetMediaUrl(media);
            try {
                // TODO: Use HTTP factory
                using System.Net.Http.HttpClient client = new();
                return client.GetByteArrayAsync(url).Result;
            } catch (Exception ex) {
                throw new Exception($"Failed downloading media ID {media.Id} from URL: {url}", ex);
            }
        }

        public virtual void DownloadBytes(LegacyMedia media, string path) {
            string dir = Path.GetDirectoryName(path)!;
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            File.WriteAllBytes(path, GetBytes(media));
        }

        #endregion

        #region Private member methods

        protected override void PrepareHttpRequest(IHttpRequest request) {
            request.SetAuthorizationBasic("api", ApiKey);
            if (request.Url.StartsWith("/")) request.Url = $"{Scheme}://{Host}{request.Url}";
        }

        #endregion

    }

}