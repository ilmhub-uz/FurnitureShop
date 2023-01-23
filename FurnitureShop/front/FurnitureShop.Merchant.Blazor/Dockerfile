FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["front/FurnitureShop.Merchant.Blazor/FurnitureShop.Merchant.Blazor.csproj", "FurnitureShop.Merchant.Blazor/"]
COPY ["front/FurnitureShop.Common/FurnitureShop.Common.csproj", "FurnitureShop.Common/"]
RUN dotnet restore "FurnitureShop.Merchant.Blazor/FurnitureShop.Merchant.Blazor.csproj"
COPY . .
WORKDIR "/front/front/FurnitureShop.Merchant.Blazor"
RUN dotnet build "FurnitureShop.Merchant.Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FurnitureShop.Merchant.Blazor.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FurnitureShop.Merchant.Blazor.dll"] 