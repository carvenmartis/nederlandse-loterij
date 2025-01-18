# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy project files
COPY ./NederlandseLoterij.Application/*.csproj ./NederlandseLoterij.Application/
COPY ./NederlandseLoterij.Infrastructure/*.csproj ./NederlandseLoterij.Infrastructure/
COPY ./NederlandseLoterij.Domain/*.csproj ./NederlandseLoterij.Domain/
COPY ./NederlandseLoterij.API/*.csproj ./NederlandseLoterij.API/

# Restore dependencies
WORKDIR /app/NederlandseLoterij.API
RUN dotnet restore

# Copy the entire source code
WORKDIR /app
COPY ./NederlandseLoterij.Application ./NederlandseLoterij.Application/
COPY ./NederlandseLoterij.Infrastructure ./NederlandseLoterij.Infrastructure/
COPY ./NederlandseLoterij.Domain ./NederlandseLoterij.Domain/
COPY ./NederlandseLoterij.API ./NederlandseLoterij.API/

# Build and publish the API
WORKDIR /app/NederlandseLoterij.API
RUN dotnet build -c Release
RUN dotnet publish -c Release -o /publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published files
COPY --from=build /publish .

ENV ASPNETCORE_URLS=http://+:80

# Expose ports
EXPOSE 80
EXPOSE 443

# Set the entry point
ENTRYPOINT ["dotnet", "NederlandseLoterij.API.dll"]
