FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /source
COPY . .
RUN dotnet restore
RUN	dotnet publish -c Release -o /app/build

FROM build as publish
RUN dotnet publish -c Release -o /app/publish

FROM nginx:alpine as final
WORKDIR /usr/share/nginx/html 
COPY --from=publish /app/publish/wwwroot
COPY nginx.conf /etc/nginx/nginx.conf