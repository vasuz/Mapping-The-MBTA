# Mapping The MBTA

This project is the server component of [Mapping the MBTA](https://github.com/Kirpal/mapping-the-mbta).

Created using the ASP.NET Core Web framework, this project provides all API endpoints used in the Mapping the MBTA project to generate live visualizations of the MBTA rapid transit network. The server requests and parses live data from the MBTA's APIs and provides it in a digestable format for the web visualization to receive and render for the user.

---

Steps to run it on your own:

1. Create the database (requires [EntityFramework Core](https://github.com/dotnet/efcore))
    1. `Add-Migration InitialCreate`
    2. `Update-Database`


2. Build and run (requres [.NET runtime](https://github.com/dotnet/runtime))
    1. `dotnet build "MappingTheMBTA.csproj" -c Release -o /app/build`
    2. `dotnet publish "MappingTheMBTA.csproj" -c Release -o /app/publish`
    3. `dotnet MappingTheMBTA.dll`
