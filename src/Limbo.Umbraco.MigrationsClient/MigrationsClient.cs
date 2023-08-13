using System;
using System.Collections.Generic;
using Limbo.Umbraco.MigrationsClient.Models;
using Limbo.Umbraco.MigrationsClient.Models.ContentTypes;

namespace Limbo.Umbraco.MigrationsClient {

    public class MigrationsClient : IMigrationsClient {

        public MigrationsHttpClient HttpClient { get; }

        public MigrationsClient(MigrationsHttpClient httpClient) {
            HttpClient = httpClient;
        }

        public MigrationsClient(string scheme, string host, string apiKey) {
            HttpClient = new MigrationsHttpClient(scheme, host, apiKey);
        }

        #region Public member methods
        
        public virtual LegacyContentType GetContentTypeById(int id) {
            return HttpClient.GetContentTypeById(id).Body;
        }

        public virtual LegacyContentType GetContentTypeByKey(Guid key) {
            return HttpClient.GetContentTypeByKey(key).Body;
        }

        public virtual LegacyContentType GetContentTypeByAlias(string alias) {
            return HttpClient.GetContentTypeByAlias(alias).Body;
        }

        public IReadOnlyList<LegacyContentItem> GetContentAtRoot() {
            return HttpClient.GetContentAtRoot().Body;
        }

        public LegacyContent GetContentById(int id) {
            return HttpClient.GetContentById(id).Body;
        }

        public TContent GetContentById<TContent>(int id) where TContent : ILegacyContent<TContent> {
            return HttpClient.GetContentById<TContent>(id).Body;
        }

        public LegacyContent GetContentByKey(Guid key) {
            return HttpClient.GetContentByKey(key).Body;
        }

        public TContent GetContentByKey<TContent>(Guid key) where TContent : ILegacyContent<TContent> {
            return HttpClient.GetContentByKey<TContent>(key).Body;
        }

        public IReadOnlyList<LegacyContentItem> GetMediaAtRoot() {
            return HttpClient.GetMediaAtRoot().Body;
        }

        public LegacyMedia GetMediaById(int id) {
            return HttpClient.GetMediaById(id).Body;
        }

        public LegacyMedia GetMediaByKey(Guid key) {
            return HttpClient.GetMediaByKey(key).Body;
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

        #endregion

    }

}