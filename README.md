# Mapping The MBTA

This is the server component of the [Mapping the MBTA](https://github.com/Kirpal/mapping-the-mbta) project.

---

Steps to run:

1. Create the database (requires [EntityFramework Core](https://github.com/dotnet/efcore))
    1. `Add-Migration InitialCreate`
    2. `Update-Database`


2. Build and run (requres [.NET runtime](https://github.com/dotnet/runtime))
    1. `dotnet build "MappingTheMBTA.csproj" -c Release -o /app/build`
    2. `dotnet publish "MappingTheMBTA.csproj" -c Release -o /app/publish`
    3. `dotnet MappingTheMBTA.dll`
