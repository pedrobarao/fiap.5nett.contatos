FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Contatos.Atualizacao.Api/Contatos.Atualizacao.Api.csproj", "src/Services/Contatos.Atualizacao.Api/"]
RUN dotnet restore "src/Services/Contatos.Atualizacao.Api/Contatos.Atualizacao.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Contatos.Atualizacao.Api"
RUN dotnet build "Contatos.Atualizacao.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Contatos.Atualizacao.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contatos.Atualizacao.Api.dll"]
