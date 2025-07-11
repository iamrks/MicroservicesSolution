# Stage 1: Build

# Use the official .NET SDK for build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy entire solution for full context
COPY . .

# Go into the InventoryService folder and restore/build
WORKDIR /src/Services/InventoryService
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish


# Stage 2: Runtime
# Use runtime image for final container
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .


# Expose port
EXPOSE 5600

ENTRYPOINT ["dotnet", "InventoryService.dll"]


