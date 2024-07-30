FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ./HangfireDemo.Api/HangfireDemo.Api.csproj ./HangfireDemo.Api/
COPY ./HangfireDemo.Jobs/HangfireDemo.Jobs.csproj ./HangfireDemo.Jobs/

RUN dotnet restore ./HangfireDemo.Api/HangfireDemo.Api.csproj

COPY ./HangfireDemo.Api ./HangfireDemo.Api/
COPY ./HangfireDemo.Jobs ./HangfireDemo.Jobs/

RUN dotnet publish --no-restore -c Release -o /publish ./HangfireDemo.Api/HangfireDemo.Api.csproj

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

LABEL MAINTAINER=contact@lerps.de

ENV DOTNET_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080

WORKDIR /api
COPY --from=build /publish .

EXPOSE 8080
ENTRYPOINT [ "dotnet", "HangfireDemo.Api.dll" ]
