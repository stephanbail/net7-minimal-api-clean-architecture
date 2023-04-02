# Set the base image to use for containers
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Set the working directory to /app
WORKDIR /app

# Copy the csproj files to the container
COPY ./*.csproj ./

# Restore dependencies
RUN dotnet restore

# Copy the remaining files to the container
COPY . ./

# Build the app
RUN dotnet publish -c Release -o out

# Set the base image to use for containers
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Set the working directory to /app
WORKDIR /app

# Copy the published output from the build environment
COPY --from=build-env /app/out .

# Expose the port used by the app
EXPOSE 80

# Set the entry point for the container
ENTRYPOINT ["dotnet", "Movies.Api.dll"]