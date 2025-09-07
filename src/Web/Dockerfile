FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Web/Web.csproj", "src/Web/"]
COPY ["src/Accounts/Accounts.Infrastructure/Accounts.Infrastructure.csproj", "src/Accounts/Accounts.Infrastructure/"]
COPY ["src/Shared/Framework/Framework.csproj", "src/Shared/Framework/"]
COPY ["src/Accounts/Accounts.Contracts/Accounts.Contracts.csproj", "src/Accounts/Accounts.Contracts/"]
COPY ["src/Shared/Core/Core.csproj", "src/Shared/Core/"]
COPY ["src/Shared/SharedKernel/SharedKernel.csproj", "src/Shared/SharedKernel/"]
COPY ["src/Accounts/Accounts.Application/Accounts.Application.csproj", "src/Accounts/Accounts.Application/"]
COPY ["src/Accounts/Accounts.Domain/Accounts.Domain.csproj", "src/Accounts/Accounts.Domain/"]
COPY ["src/Accounts/Accounts.Presentation/Accounts.Presentation.csproj", "src/Accounts/Accounts.Presentation/"]
RUN dotnet restore "src/Web/Web.csproj"
COPY . .
WORKDIR "/src/src/Web"
RUN dotnet build "./Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]
