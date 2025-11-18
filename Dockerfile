# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the project folder
COPY sampleapi/ .

# Restore using the correct project path
RUN dotnet restore "sampleapi/SampleAPI.csproj"

# Build & publish
RUN dotnet publish "sampleapi/SampleAPI.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8082

ENTRYPOINT ["dotnet", "SampleAPI.dll"]
