# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Print the current directory to verify the context
RUN echo "Current directory in build stage:" && pwd

# Copy the project files and restore dependencies
COPY ["FietsRoute-Backend/FietsRoute-Backend.csproj", "FietsRoute-Backend/"]
COPY ["Business/Business.csproj", "Business/"]
COPY ["Data/Data.csproj", "Data/"]

# List contents of /src to verify files were copied
RUN echo "Contents of /src:" && ls -la /src

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
