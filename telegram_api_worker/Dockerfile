FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY TelegramApiWorker.csproj TelegramApiWorker.csproj
RUN dotnet restore TelegramApiWorker.csproj
COPY . .
WORKDIR /src
RUN dotnet build TelegramApiWorker.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -p:PublishReadyToRun=true --self-contained=true -r linux-x64 -o /app

# RUN apt update && apt install -qqq libicu70
# WORKDIR /opt/TelegramApiWorker

# COPY --from=publish /app .

CMD "/app/TelegramApiWorker"

# FROM ubuntu:22.04 AS final

