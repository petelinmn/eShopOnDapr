#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
ARG NET_IMAGE=6.0-bullseye-slim
FROM mcr.microsoft.com/dotnet/aspnet:${NET_IMAGE} AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:${NET_IMAGE} AS build
WORKDIR /src

COPY ["src/Web/BlastAce.Api/BlastAce.Api.csproj", "src/Web/BlastAce.Api/"]
COPY ["src/Lib/BlastAce.Model/BlastAce.Model.csproj", "src/Lib/BlastAce.Model/"]

RUN dotnet restore "src/Web/BlastAce.Api/BlastAce.Api.csproj"
COPY . .
WORKDIR "/src/src/Web/BlastAce.Api"
RUN dotnet build "BlastAce.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore "BlastAce.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish/ .
ENTRYPOINT ["dotnet", "BlastAce.Api.dll"]

