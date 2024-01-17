#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CryptocurrencyData.csproj", "CryptocurrencyData/"]
RUN dotnet restore "CryptocurrencyData/CryptocurrencyData.csproj"
WORKDIR "/src/CryptocurrencyData"
COPY . .
RUN dotnet build "CryptocurrencyData.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CryptocurrencyData.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CryptocurrencyData.dll"]
