using System;

namespace Limbo.Umbraco.MigrationsClient.Exceptions;

public class MigrationsParseExcetion : Exception {

    public MigrationsParseExcetion(string message) : base(message) { }

    public MigrationsParseExcetion(string message, Exception? innerException) : base(message, innerException) { }

}