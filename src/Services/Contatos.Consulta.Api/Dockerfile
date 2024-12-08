FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Contatos.Consulta.Api/Contatos.Consulta.Api.csproj", "src/Services/Contatos.Consulta.Api/"]
COPY ["src/BuildingBlocks/Contatos.ServiceDefaults/Contatos.ServiceDefaults.csproj", "src/BuildingBlocks/Contatos.ServiceDefaults/"]
COPY ["src/BuildingBlocks/Commons.Domain/Commons.Domain.csproj", "src/BuildingBlocks/Commons.Domain/"]
COPY ["src/BuildingBlocks/MessageBus/MessageBus.csproj", "src/BuildingBlocks/MessageBus/"]
COPY ["src/BuildingBlocks/Utils/Utils.csproj", "src/BuildingBlocks/Utils/"]
RUN dotnet restore "src/Services/Contatos.Consulta.Api/Contatos.Consulta.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Contatos.Consulta.Api"
RUN dotnet build "Contatos.Consulta.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Contatos.Consulta.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contatos.Consulta.Api.dll"]
