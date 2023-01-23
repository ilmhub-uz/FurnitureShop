FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["front/FurnitureShop.Dashboard.Blazor/FurnitureShop.Dashboard.Blazor.csproj", "FurnitureShop.Dashboard.Blazor/"]
COPY ["front/FurnitureShop.Common/FurnitureShop.Common.csproj", "FurnitureShop.Common/"]
RUN dotnet restore "FurnitureShop.Dashboard.Blazor/FurnitureShop.Dashboard.Blazor.csproj"
COPY . .
WORKDIR "/front/front/FurnitureShop.Dashboard.Blazor"
RUN dotnet build "FurnitureShop.Dashboard.Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FurnitureShop.Dashboard.Blazor.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FurnitureShop.Dashboard.Blazor.dll"] 