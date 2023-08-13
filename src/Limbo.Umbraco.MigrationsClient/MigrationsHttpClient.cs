using System;
using System.Collections.Generic;
using System.IO;
using Limbo.Umbraco.MigrationsClient.Models;
using Limbo.Umbraco.MigrationsClient.Responses;
using Skybrud.Essentials.Http;
using Skybrud.Essentials.Http.Client;

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

        public IMigrationsResponse<IReadOnlyList<LegacyContentItem>> GetContentAtRoot() {
            return new MigrationsListResponse<LegacyContentItem>(Get("/umbraco/Limbo/Migrations/GetContentAtRoot"));
        }

        public IMigrationsResponse<LegacyContent> GetContentById(int id) {
            return new MigrationsResponse<LegacyContent>(Get($"/umbraco/Limbo/Migrations/GetContentById?id={id}"));
        }

        public IMigrationsResponse<TContent> GetContentById<TContent>(int id) where TContent : ILegacyContent<TContent> {
            return new MigrationsResponse<TContent>(Get($"/umbraco/Limbo/Migrations/GetContentById?id={id}"));
        }

        public IMigrationsResponse<LegacyContent> GetContentByKey(Guid key) {
            return new MigrationsResponse<LegacyContent>(Get($"/umbraco/Limbo/Migrations/GetContentByKey?key={key}"));
        }

        public IMigrationsResponse<TContent> GetContentByKey<TContent>(Guid key) where TContent : ILegacyContent<TContent> {
            return new MigrationsResponse<TContent>(Get($"/umbraco/Limbo/Migrations/GetContentById?key={key}"));
        }

        public IMigrationsResponse<IReadOnlyList<LegacyContentItem>> GetMediaAtRoot() {
            return new MigrationsListResponse<LegacyContentItem>(Get("/umbraco/Limbo/Migrations/GetContentAtRoot"));
        }

        public IMigrationsResponse<LegacyMedia> GetMediaById(int id) {
            string url = $"/umbraco/Limbo/Migrations/GetMediaById?id={id}";
            try {
                return new MigrationsResponse<LegacyMedia>(Get(url));
            } catch (Exception ex) {
                throw new Exception($"Failed getting media with ID {id} -> {url}", ex);
            }
        }

        public IMigrationsResponse<LegacyMedia> GetMediaByKey(Guid key) {
            string url = $"/umbraco/Limbo/Migrations/GetMediaByKey?key={key}";
            try {
                return new MigrationsResponse<LegacyMedia>(Get(url));
            } catch (Exception ex) {
                throw new Exception($"Failed getting media with key {key} -> {url}", ex);
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