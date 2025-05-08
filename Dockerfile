# -------- Build Stage --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything from the root (where your .csproj and code is)
COPY . .

# Restore dependencies
RUN dotnet restore "./QrCodeGeneratorApp.csproj"

# Build and publish
RUN dotnet publish "./QrCodeGeneratorApp.csproj" -c Release -o /app/publish

# -------- Runtime Stage --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the published output from build
COPY --from=build /app/publish .

# Run the app
ENTRYPOINT ["dotnet", "QrCodeGeneratorApp.dll"]
