# Use the official .NET 8 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["api/src/legal-saas-api/legal-saas-api.csproj", "legal-saas-api/"]
RUN dotnet restore "legal-saas-api/legal-saas-api.csproj"

# Copy everything else and build
COPY api/src/legal-saas-api/ legal-saas-api/
WORKDIR "/src/legal-saas-api"
RUN dotnet build "legal-saas-api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "legal-saas-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the official .NET 8 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Copy the published application
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "legal-saas-api.dll"]
