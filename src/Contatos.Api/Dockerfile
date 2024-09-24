FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Contatos.Api/Contatos.Api.csproj", "src/Contatos.Api/"]
COPY ["src/Contatos.Application/Contatos.Application.csproj", "src/Contatos.Application/"]
COPY ["src/Contatos.Domain/Contatos.Domain.csproj", "src/Contatos.Domain/"]
COPY ["src/Infra.CrossCutting/Infra.CrossCutting.csproj", "src/Infra.CrossCutting/"]
COPY ["src/Commons.Domain/Commons.Domain.csproj", "src/Commons.Domain/"]
COPY ["src/Contatos.Infra.Data/Contatos.Infra.Data.csproj", "src/Contatos.Infra.Data/"]
RUN dotnet restore "src/Contatos.Api/Contatos.Api.csproj"
COPY . .
WORKDIR "/src/src/Contatos.Api"
RUN dotnet build "Contatos.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Contatos.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contatos.Api.dll"]
