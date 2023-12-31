#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UserManagement/UserManagement.csproj", "UserManagement/"]
COPY ["JwtAuthenticationManager/JwtAuthenticationManager.csproj", "JwtAuthenticationManager/"]
RUN dotnet restore "UserManagement/UserManagement.csproj"
COPY . .
WORKDIR "/src/UserManagement"
RUN dotnet build "UserManagement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserManagement.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserManagement.dll"]
