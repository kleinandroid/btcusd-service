#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 80
EXPOSE 443
EXPOSE 5164


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./btcusd-agregator-service/btcusd-agregator-service.csproj", "btcusd-agregator-service/"]
RUN dotnet restore "./btcusd-agregator-service/btcusd-agregator-service.csproj"
COPY . .
WORKDIR "/src/btcusd-agregator-service"
RUN dotnet build "btcusd-agregator-service.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "btcusd-agregator-service.csproj" -c Debug -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "btcusd-agregator-service.dll"]