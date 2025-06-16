
# Build stage
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

WORKDIR /src

# Kopieer alleen solution en project files eerst om caching te benutten
COPY *.sln .
COPY Portfolio.Api/*.csproj ./Portfolio.Api/
COPY Portfolio.Core/*.csproj ./Portfolio.Core/
COPY Portfolio.Infrastructure/*.csproj ./Portfolio.Infrastructure/

# Restore dependencies (gecached tenzij projectfiles wijzigen)
RUN dotnet restore

# Kopieer daarna de rest van de broncode
COPY . .

ARG TARGETARCH

# Publish in Release mode, architecture-specifiek
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish -c Release -a ${TARGETARCH/amd64/x64} --use-current-runtime --self-contained false -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final

WORKDIR /app

# Kopieer gepubliceerde app vanuit build stage
COPY --from=build /app .

# Poort waar de app op luistert (optioneel maar duidelijk)
EXPOSE 5000

# Run als niet-root gebruiker voor security
USER $APP_UID

# Start de app
ENTRYPOINT ["dotnet", "Portfolio.Api.dll"]

