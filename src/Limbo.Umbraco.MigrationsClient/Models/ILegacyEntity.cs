namespace Limbo.Umbraco.MigrationsClient.Models {

    public interface ILegacyEntity : ILegacyElement {

        int Id { get; }

        string Url { get; }

    }

}