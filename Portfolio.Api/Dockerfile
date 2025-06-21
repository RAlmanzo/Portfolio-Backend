ARG DOTNET_VERSION=8.0

# 🛠️ Build Stage
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src

ARG BUILD_CONFIGURATION=Release

# 📦 Projectbestanden kopiëren
COPY ["Portfolio.Api/Portfolio.Api.csproj", "Portfolio.Api/"]
COPY ["Portfolio.Core/Portfolio.Core.csproj", "Portfolio.Core/"]
COPY ["Portfolio.Infrastructure/Portfolio.Infrastructure.csproj", "Portfolio.Infrastructure/"]

# 🔄 Dependencies restoren (gebruikt caching optimaal)
RUN dotnet restore "Portfolio.Api/Portfolio.Api.csproj"

# 📁 Alles kopiëren voor build
COPY . .
WORKDIR "/src/Portfolio.Api"

# 🏗️ Build en publish
RUN dotnet publish "Portfolio.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# 🚀 Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS final
WORKDIR /app

# 📤 Publicatie uit build-stage kopiëren
COPY --from=build /app/publish .

# 🌐 Expose poort
EXPOSE 8080

# 🏃‍♂️ Start de app
ENTRYPOINT ["dotnet", "Portfolio.Api.dll"]