# Mapping The MBTA

This project is the server component of the [Mapping the MBTA](https://github.com/Kirpal/mapping-the-mbta) visualization project.

Created using the ASP.NET Core Web framework, this project provides all API endpoints used in Mapping the MBTA to generate live visualizations of the MBTA rapid transit network. The server requests and parses live data from the MBTA's APIs and provides it in a digestable format for the web visualization to receive and render for the user.

---

Steps to run it on your own:

1. Create the database (requires [EntityFramework Core](https://github.com/dotnet/efcore))
    1. `Add-Migration InitialCreate`
    2. `Update-Database`


2. Build and run (requres [.NET runtime](https://github.com/dotnet/runtime))
    1. `dotnet build "MappingTheMBTA.csproj" -c Release -o /app/build`
    2. `dotnet publish "MappingTheMBTA.csproj" -c Release -o /app/publish`
    3. `dotnet MappingTheMBTA.dll`

You'll also need an API key to be able to receive data from the MBTA. You can get one from the MBTA's [developer portal](https://api-v3.mbta.com/).
