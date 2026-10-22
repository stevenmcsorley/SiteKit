FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY SiteKit.sln global.json ./
COPY src/SiteKit.Core/SiteKit.Core.csproj src/SiteKit.Core/
COPY src/SiteKit.Studio/SiteKit.Studio.csproj src/SiteKit.Studio/
RUN dotnet restore src/SiteKit.Studio/SiteKit.Studio.csproj
COPY src/SiteKit.Core/ src/SiteKit.Core/
COPY src/SiteKit.Studio/ src/SiteKit.Studio/
RUN dotnet publish src/SiteKit.Studio/SiteKit.Studio.csproj -c Debug -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
ENV ASPNETCORE_HTTP_PORTS=8080
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "SiteKit.Studio.dll"]
