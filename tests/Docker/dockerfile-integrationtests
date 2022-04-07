ARG BuildConfiguration=Debug

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build-env
ARG BuildConfiguration
WORKDIR /app

COPY src/Application/*.csproj src/Application/
COPY src/Domain/*.csproj src/Domain/
COPY src/Adapters/Out/EventsDispatching/*.csproj src/Adapters/Out/EventsDispatching/
COPY src/Adapters/Out/Persistence/*.csproj src/Adapters/Out/Persistence/
COPY tests/IntegrationTests/*.csproj tests/IntegrationTests/
RUN dotnet restore tests/IntegrationTests/NetCoreHexagonal.IntegrationTests.csproj

COPY src src/
COPY tests/IntegrationTests tests/IntegrationTests/
RUN dotnet publish tests/IntegrationTests/NetCoreHexagonal.IntegrationTests.csproj -c $BuildConfiguration -o out --no-restore

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine
WORKDIR /app
COPY --from=build-env /app/out .
