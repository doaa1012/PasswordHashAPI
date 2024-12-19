# Use the official ASP.NET Core runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the .NET SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

# Restore dependencies
RUN dotnet restore "./PasswordHashAPI.csproj"

# Build the project
RUN dotnet build "./PasswordHashAPI.csproj" -c Release -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish "./PasswordHashAPI.csproj" -c Release -o /app/publish

# Use the runtime image to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PasswordHashAPI.dll"]
