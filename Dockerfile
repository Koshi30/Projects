# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8085

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything to image
COPY . .

# Restore dependencies
RUN dotnet restore "sampleapi/SampleAPI.csproj"

# Build application
RUN dotnet build "sampleapi/SampleAPI.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "sampleapi/SampleAPI.csproj" -c Release -o /app/publish

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "SampleAPI.dll"]
