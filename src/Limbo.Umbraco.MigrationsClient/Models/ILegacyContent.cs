namespace Limbo.Umbraco.MigrationsClient.Models {

    public interface ILegacyContent<out TContent> : ILegacyEntity, IJsonParsable<TContent> { }

}