FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ./HangfireDemo.Server/HangfireDemo.Server.csproj ./HangfireDemo.Server/
COPY ./HangfireDemo.Jobs/HangfireDemo.Jobs.csproj ./HangfireDemo.Jobs/

RUN dotnet restore ./HangfireDemo.Server/HangfireDemo.Server.csproj

COPY ./HangfireDemo.Server ./HangfireDemo.Server/
COPY ./HangfireDemo.Jobs ./HangfireDemo.Jobs/

RUN dotnet publish --no-restore -c Release -o /publish ./HangfireDemo.Server/HangfireDemo.Server.csproj

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final

LABEL MAINTAINER=contact@lerps.de

ENV DOTNET_ENVIRONMENT=Production

WORKDIR /server
COPY --from=build /publish .

ENTRYPOINT [ "dotnet", "HangfireDemo.Server.dll" ]
