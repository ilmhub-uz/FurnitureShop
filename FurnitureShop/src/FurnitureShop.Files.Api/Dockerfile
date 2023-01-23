FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/FurnitureShop.Files/FurnitureShop.Files.Api.csproj", "FurnitureShop.Files.Api/"]
COPY ["src/FurnitureShop.Common/FurnitureShop.Common.csproj", "FurnitureShop.Common/"]
COPY ["src/FurnitureShop.Data/FurnitureShop.Data.csproj", "FurnitureShop.Data/"]
RUN dotnet restore "FurnitureShop.Files.Api/FurnitureShop.Files.Api.csproj"
COPY . .
WORKDIR "/src/src/FurnitureShop.Files.Api"
RUN dotnet build "FurnitureShop.Files.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FurnitureShop.Files.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FurnitureShop.Files.Api.dll"] 