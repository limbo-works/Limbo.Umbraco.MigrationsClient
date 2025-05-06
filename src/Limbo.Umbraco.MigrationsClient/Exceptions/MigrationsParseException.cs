using System;

namespace Limbo.Umbraco.MigrationsClient.Exceptions;

public class MigrationsParseException : Exception {

    public MigrationsParseException(string message) : base(message) { }

    public MigrationsParseException(string message, Exception? innerException) : base(message, innerException) { }

}