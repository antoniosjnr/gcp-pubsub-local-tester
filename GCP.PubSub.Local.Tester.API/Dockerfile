#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GCP.Local.Tester.API/GCP.Local.Tester.API.csproj", "GCP.Local.Tester.API/"]
RUN dotnet restore "GCP.Local.Tester.API/GCP.Local.Tester.API.csproj"
COPY . .
WORKDIR "/src/GCP.Local.Tester.API"
RUN dotnet build "GCP.Local.Tester.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GCP.Local.Tester.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GCP.Local.Tester.API.dll"]

FROM messagebird/gcloud-pubsub-emulator:latest AS gcp-pub-sub