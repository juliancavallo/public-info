#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PublicInfo.API/PublicInfo.API.csproj", "PublicInfo.API/"]
COPY ["PublicInfo.Domain/PublicInfo.Domain.csproj", "PublicInfo.Domain/"]
COPY ["PublicInfo.Services/PublicInfo.Services.csproj", "PublicInfo.Services/"]
RUN dotnet restore "PublicInfo.API/PublicInfo.API.csproj"
COPY . .
WORKDIR "/src/PublicInfo.API"
RUN dotnet build "PublicInfo.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PublicInfo.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet PublicInfo.API.dll