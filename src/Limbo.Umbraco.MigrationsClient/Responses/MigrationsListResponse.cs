using System.Collections.Generic;
using System.Net;
using Skybrud.Essentials.Http.Exceptions;
using Skybrud.Essentials.Http;
using System.Reflection;
using System;

namespace Limbo.Umbraco.MigrationsClient.Responses {

    public class MigrationsListResponse<T> : HttpResponseBase, IMigrationsResponse<IReadOnlyList<T>> {

        public IReadOnlyList<T> Body { get; }

        public MigrationsListResponse(IHttpResponse response) : base(response) {

            if (response.StatusCode != HttpStatusCode.OK) throw new HttpException(response);

            var type = typeof(T);

            var m = type.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public);
            if (m == null) throw new Exception($"Type {type} doesn't specify a static 'Parse' method.");

            Body = ParseJsonArray(response.Body, x => (T) m.Invoke(null, new object[] { x })!);

        }

    }

}