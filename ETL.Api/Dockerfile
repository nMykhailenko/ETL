FROM mcr.microsoft.com/dotnet/aspnet:6.0.2 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["ETL.Api/ETL.Api.csproj", "ETL.Api/"]
COPY ["ETL.Shared/ETL.Shared.csproj", "ETL.Shared/"]


RUN dotnet restore "ETL.Api/ETL.Api.csproj"

COPY . .
WORKDIR "/src/ETL.Api"
RUN dotnet build "ETL.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ETL.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ETL.Api.dll"]