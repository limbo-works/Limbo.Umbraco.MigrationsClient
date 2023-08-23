// ReSharper disable PossibleInterfaceMemberAmbiguity

namespace Limbo.Umbraco.MigrationsClient.Models.Members {

    public interface ILegacyMember : ILegacyEntity { }

    public interface ILegacyMember<out TMember> : ILegacyMember, IJsonParsable<TMember> { }

}