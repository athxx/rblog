FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["App/Cli/Cli.csproj", "App/Cli/"]
RUN dotnet restore "App/Cli/Cli.csproj"
COPY . .
WORKDIR "/src/App/Cli"
RUN dotnet build "Cli.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cli.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cli.dll"]
