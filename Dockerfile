# Runtime base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "sampleapi/SampleAPI.csproj"
RUN dotnet build "sampleapi/SampleAPI.csproj" -c Release -o /app/build
RUN dotnet publish "sampleapi/SampleAPI.csproj" -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SampleAPI.dll"]
