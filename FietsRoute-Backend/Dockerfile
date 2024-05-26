# Stage 1: Build ASP.NET Core app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
#copy all projects files .csproj
COPY FietsRoute-Backend/*.csproj ./
COPY FietsRoute-Backend/Data/*.csproj ./Data/
COPY FietsRoute-Backend/Business/*.csproj ./Business/

RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Run ASP.NET Core app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FietsRoute-Backend.dll"]
