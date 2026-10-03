FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["LostAndFound.csproj", "./"]
RUN dotnet restore "LostAndFound.csproj"

COPY . .
RUN dotnet build "LostAndFound.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LostAndFound.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LostAndFound.dll"]