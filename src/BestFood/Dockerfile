FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/BestFood/BestFood.csproj", "src/BestFood/"]
COPY ["src/modules/users/BestFood.Modules.Users.Infrastructure/BestFood.Modules.Users.Infrastructure.csproj", "src/modules/users/BestFood.Modules.Users.Infrastructure/"]
COPY ["src/modules/users/BestFood.Modules.Users.Domain/BestFood.Modules.Users.Domain.csproj", "src/modules/users/BestFood.Modules.Users.Domain/"]
COPY ["src/modules/shared/BestFood.Modules.Shared.Domain/BestFood.Modules.Shared.Domain.csproj", "src/modules/shared/BestFood.Modules.Shared.Domain/"]
COPY ["src/modules/shared/BestFood.Modules.Shared.Presentation/BestFood.Modules.Shared.Presentation.csproj", "src/modules/shared/BestFood.Modules.Shared.Presentation/"]
COPY ["src/modules/users/BestFood.Modules.Users.Presentation/BestFood.Modules.Users.Presentation.csproj", "src/modules/users/BestFood.Modules.Users.Presentation/"]
COPY ["src/modules/users/BestFood.Modules.Users.Application/BestFood.Modules.Users.Application.csproj", "src/modules/users/BestFood.Modules.Users.Application/"]
COPY ["src/modules/shared/BestFood.Modules.Shared.Application/BestFood.Modules.Shared.Application.csproj", "src/modules/shared/BestFood.Modules.Shared.Application/"]
COPY ["src/modules/shared/BestFood.Modules.Shared.Infrastructure/BestFood.Modules.Shared.Infrastructure.csproj", "src/modules/shared/BestFood.Modules.Shared.Infrastructure/"]
RUN dotnet restore "src/BestFood/BestFood.csproj"
COPY . .
WORKDIR "/src/src/BestFood"
RUN dotnet build "BestFood.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BestFood.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BestFood.dll"]
