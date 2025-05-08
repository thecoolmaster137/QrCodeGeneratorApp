# Use the official .NET SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY QrCodeGeneratorApp/*.csproj ./QrCodeGeneratorApp/
RUN dotnet restore

# Copy the rest of the source code
COPY QrCodeGeneratorApp/. ./QrCodeGeneratorApp/
WORKDIR /app/QrCodeGeneratorApp
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/QrCodeGeneratorApp/out ./

# Expose port
EXPOSE 80

# Start the app
ENTRYPOINT ["dotnet", "QrCodeGeneratorApp.dll"]
