# 1. Base Image (เปลี่ยน Invariant เป็น false และติดตั้ง libicu รองรับภาษาไทย)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# ติดตั้ง libicu เพื่อรองรับการประมวลผลตัวอักษรและภาษาไทยบน Linux
RUN apt-get update && apt-get install -y libicu-dev && rm -rf /var/lib/apt/lists/*

# 2. Build Image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["LostAndFound.csproj", "./"]
RUN dotnet restore "LostAndFound.csproj"

COPY . .
RUN dotnet build "LostAndFound.csproj" -c Release -o /app/build

# 3. Publish Image
FROM build AS publish
RUN dotnet publish "LostAndFound.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 4. Final Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LostAndFound.dll"]