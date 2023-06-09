@echo off
dotnet build src/Limbo.Umbraco.MigrationsClient --configuration Release /t:rebuild /t:pack -p:PackageOutputPath=../../releases/nuget