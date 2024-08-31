# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set working directory
WORKDIR /src

# Copy the entire solution
COPY . .

# Restore dependencies
RUN dotnet restore OAK/OAK.WebReport/OAK.WebReport.csproj --disable-parallel --verbosity detailed

# Build the project
RUN dotnet build OAK/OAK.WebReport/OAK.WebReport.csproj -c Release -o /app/build

# Publish the application
RUN dotnet publish OAK/OAK.WebReport/OAK.WebReport.csproj -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# Copy the published output from the build image
COPY --from=build /app/publish .

# Copy the OAK/Documents directory into the runtime image
COPY OAK/Documents /app/Documents

# Expose the port your application will run on
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "OAK.WebReport.dll"]