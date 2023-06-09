@echo off
dotnet build src/Limbo.Umbraco.MigrationsClient --configuration Debug /t:rebuild /t:pack -p:PackageOutputPath=c:/nuget