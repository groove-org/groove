# === STAGE 1: Build ===
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set working directory
WORKDIR /src

# Copy everything (cleaned by .dockerignore)
COPY . .

# Restore NuGet packages
RUN dotnet restore ./GroovE.Api/GroovE.Api.csproj

# Build and publish release version
RUN dotnet publish ./GroovE.Api/GroovE.Api.csproj -c Release -o /api/publish

# === STAGE 2: Runtime ===
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

WORKDIR /api

# Copy published output from build stage
COPY --from=build /api/publish .

# Expose default ASP.NET Core port
EXPOSE 80

# Run the application
ENTRYPOINT ["dotnet", "GroovE.Api.dll"]
