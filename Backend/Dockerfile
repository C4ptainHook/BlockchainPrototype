FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Blockchain.Api/Blockchain.Api.csproj", "src/Blockchain.Api/"]
COPY ["src/Blockchain.Business/Blockchain.Business.csproj", "src/Blockchain.Business/"]
COPY ["src/Blockchain.Data/Blockchain.Data.csproj", "src/Blockchain.Data/"]
RUN dotnet restore "src/Blockchain.Api/Blockchain.Api.csproj"
COPY . .
WORKDIR "/src/src/Blockchain.Api"
RUN dotnet build "Blockchain.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Blockchain.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blockchain.Api.dll"]