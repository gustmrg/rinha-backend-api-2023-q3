FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RinhaDeBackEnd.API/RinhaDeBackEnd.API.csproj", "RinhaDeBackEnd.API/"]
RUN dotnet restore "RinhaDeBackEnd.API/RinhaDeBackEnd.API.csproj"
COPY . .
WORKDIR "/src/RinhaDeBackEnd.API"
RUN dotnet build "RinhaDeBackEnd.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RinhaDeBackEnd.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RinhaDeBackEnd.API.dll"]