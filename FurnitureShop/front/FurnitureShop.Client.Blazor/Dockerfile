FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["front/FurnitureShop.Client.Blazor/FurnitureShop.Client.Blazor.csproj", "FurnitureShop.Client.Blazor/"]
COPY ["front/FurnitureShop.Common/FurnitureShop.Common.csproj", "FurnitureShop.Common/"]
RUN dotnet restore "FurnitureShop.Client.Blazor/FurnitureShop.Client.Blazor.csproj"
COPY . .
WORKDIR "/front/front/FurnitureShop.Client.Blazor"
RUN dotnet build "FurnitureShop.Client.Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FurnitureShop.Client.Blazor.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FurnitureShop.Client.Blazor.dll"] 