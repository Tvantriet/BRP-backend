# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
#copy all projects files .csproj
COPY ["FietsRoute-Backend/FietsRoute-Backend/FietsRoute-Backend.csproj", "FietsRoute-Backend/"]
COPY ["FietsRoute-Backend/Business/Business.csproj", "Business/"]
#COPY ["FietsRoute-Backend/Data/Data.csproj", "Data/"]

# Copy the entire source code
COPY . .

# Build the application
RUN dotnet publish "FietsRoute-Backend/FietsRoute-Backend.csproj" -c Release -o /app/publish

# Stage 2: Serve the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FietsRoute-Backend.dll"]
