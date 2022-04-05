ARG BuildConfiguration=Debug

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build-env
ARG BuildConfiguration
WORKDIR /app

COPY src/Application/*.csproj src/Application/
COPY src/Domain/*.csproj src/Domain/
COPY src/Adapters/Out/EventsDispatching/*.csproj src/Adapters/Out/EventsDispatching/
COPY src/Adapters/Out/Persistence/*.csproj src/Adapters/Out/Persistence/
COPY src/Adapters/In/Presentation.WebApi/*.csproj src/Adapters/In/Presentation.WebApi/
RUN dotnet restore src/Adapters/In/Presentation.WebApi/NetCoreHexagonal.WebApi.csproj

COPY src src
RUN dotnet publish src/Adapters/In/Presentation.WebApi/NetCoreHexagonal.WebApi.csproj -c $BuildConfiguration -o out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "NetCoreHexagonal.WebApi.dll"]
