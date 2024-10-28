# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy everything and restore as distinct layers
COPY . ./
RUN dotnet restore

# Build the Blazor WASM app with the specified output directory
RUN dotnet publish -c Release -o Client/bin/Release/net8.0/publish

# Use an Nginx image to serve the app
FROM nginx:alpine
COPY --from=build-env /app/Client/bin/Release/net8.0/publish /usr/share/nginx/html

# Expose port 80 to serve the app
EXPOSE 80

# Start Nginx server
CMD ["nginx", "-g", "daemon off;"]
