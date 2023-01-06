FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/FurnitureShop.Admin.Api/FurnitureShop.Admin.Api.csproj", "FurnitureShop.Admin.Api/"]
COPY ["src/FurnitureShop.Common/FurnitureShop.Common.csproj", "FurnitureShop.Common/"]
COPY ["src/FurnitureShop.Data/FurnitureShop.Data.csproj", "FurnitureShop.Data/"]
RUN dotnet restore "FurnitureShop.Admin.Api/FurnitureShop.Admin.Api.csproj"
COPY . .
WORKDIR "/src/src/FurnitureShop.Admin.Api"
RUN dotnet build "FurnitureShop.Admin.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FurnitureShop.Admin.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FurnitureShop.Admin.Api.dll"]