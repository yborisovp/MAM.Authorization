FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MAM.Authorization/MAM.Authorization.csproj", "MAM.Authorization/"]
RUN dotnet restore "MAM.Authorization/MAM.Authorization.csproj"
COPY . .
WORKDIR "/src/MAM.Authorization"
RUN dotnet build "MAM.Authorization.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MAM.Authorization.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MAM.Authorization.dll"]
