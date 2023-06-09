using Skybrud.Essentials.Http.Exceptions;
using Skybrud.Essentials.Http;
using System.Net;
using System.Reflection;
using System;
using Limbo.Umbraco.MigrationsClient.Models;

namespace Limbo.Umbraco.MigrationsClient.Responses {

    public class MigrationsResponse<T> : HttpResponseBase, IMigrationsResponse<T> where T : IJsonParsable<T> {

        public T Body { get; }

        public MigrationsResponse(IHttpResponse response) : base(response) {

            if (response.StatusCode != HttpStatusCode.OK) throw new HttpException(response);

            var type = typeof(T);

            var m = type.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public);
            if (m == null) throw new Exception($"Type {type} doesn't specify a static 'Parse' method.");

            Body = ParseJsonObject(response.Body, x => (T)m.Invoke(null, new object[] { x })!);

        }

    }

}