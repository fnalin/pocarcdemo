FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["frontend/Fansoft.PocArc.Front/Fansoft.PocArc.Front.csproj", "frontend/Fansoft.PocArc.Front/"]
RUN dotnet restore "frontend/Fansoft.PocArc.Front/Fansoft.PocArc.Front.csproj"
COPY . .
WORKDIR "/src/frontend/Fansoft.PocArc.Front"
RUN dotnet build "./Fansoft.PocArc.Front.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Fansoft.PocArc.Front.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fansoft.PocArc.Front.dll"]
