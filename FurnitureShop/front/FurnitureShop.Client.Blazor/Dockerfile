FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
COPY . /source/
RUN dotnet restore
RUN dotnet publish -c Release  -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM nginx:alpine as final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf

