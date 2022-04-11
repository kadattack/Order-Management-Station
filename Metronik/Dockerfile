FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Metronik/Metronik.csproj", "Metronik/"]
RUN dotnet restore "Metronik/Metronik.csproj"
COPY . .
WORKDIR "/src/Metronik"
RUN dotnet build "Metronik.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Metronik.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Metronik.dll"]
