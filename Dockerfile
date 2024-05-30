# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files and restore dependencies
RUN echo "$PWD"

COPY ["FietsRoute-Backend/FietsRoute-Backend.csproj", "FietsRoute-Backend/"]
COPY ["C:/Users/Timvt/Source/Repos/Business/Business.csproj", "Business/"]
COPY ["C:/Users/Timvt/Source/Repos/FietsRoute-Backend/Data", "Data/"]
RUN dotnet restore "FietsRoute-Backend/FietsRoute-Backend.csproj"

# Copy the entire source code
COPY . .

# Build the application
RUN dotnet publish "FietsRoute-Backend/FietsRoute-Backend.csproj" -c Release -o /app/publish

# Stage 2: Serve the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FietsRoute-Backend.dll"]
