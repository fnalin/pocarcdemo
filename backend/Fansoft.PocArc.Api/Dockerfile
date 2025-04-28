FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["backend/Fansoft.PocArc.Api/Fansoft.PocArc.Api.csproj", "backend/Fansoft.PocArc.Api/"]
RUN dotnet restore "backend/Fansoft.PocArc.Api/Fansoft.PocArc.Api.csproj"
COPY . .
WORKDIR "/src/backend/Fansoft.PocArc.Api"
RUN dotnet build "./Fansoft.PocArc.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Fansoft.PocArc.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fansoft.PocArc.Api.dll"]
