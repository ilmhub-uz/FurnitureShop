FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["front/FurnitureShop.Blazor/FurnitureShop.Blazor.csproj", "FurnitureShop.Blazor/"]
COPY ["front/FurnitureShop.Common/FurnitureShop.Common.csproj", "FurnitureShop.Common/"]
RUN dotnet restore "FurnitureShop.Blazor/FurnitureShop.Blazor.csproj"
COPY . .
WORKDIR "/front/front/FurnitureShop.Blazor"
RUN dotnet build "FurnitureShop.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FurnitureShop.Blazor.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FurnitureShop.Blazor.dll"] 