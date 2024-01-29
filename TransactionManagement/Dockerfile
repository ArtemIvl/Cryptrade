FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TransactionManagement/TransactionManagement.csproj", "TransactionManagement/"]
RUN dotnet restore "TransactionManagement/TransactionManagement.csproj"
WORKDIR "/CryptradeBack/TransactionManagement"
COPY . .
RUN dotnet build "TransactionManagement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "/TransactionManagement.csproj/" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TransactionManagement.dll"]
