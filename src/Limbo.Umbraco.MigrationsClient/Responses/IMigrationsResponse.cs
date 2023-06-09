namespace Limbo.Umbraco.MigrationsClient.Responses {

    public interface IMigrationsResponse<out T> {

        public T Body { get; }

    }

}