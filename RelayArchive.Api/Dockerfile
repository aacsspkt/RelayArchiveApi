FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env


WORKDIR /source

COPY . .

RUN dotnet restore
RUN dotnet publish -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0 

WORKDIR /app
COPY --from=build-env /app .

ENTRYPOINT [ "dotnet", "RelayArchive.Api.dll"]
