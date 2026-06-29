FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/VetSysCli.Api/VetSysCli.Api.csproj", "src/VetSysCli.Api/"]
COPY ["src/VetSysCli.Application/VetSysCli.Application.csproj", "src/VetSysCli.Application/"]
COPY ["src/VetSysCli.Core/VetSysCli.Core.csproj", "src/VetSysCli.Core/"]
COPY ["src/VetSysCli.Infrastructure/VetSysCli.Infrastructure.csproj", "src/VetSysCli.Infrastructure/"]
COPY ["VetSysCli.sln", "./"]
RUN dotnet restore VetSysCli.sln

COPY . .
RUN dotnet publish src/VetSysCli.Api/VetSysCli.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "VetSysCli.Api.dll"]
