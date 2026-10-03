# 1. ใช้ Runtime Image ของ .NET 8 (หรือปรับเป็น 7.0 / 6.0 ตามเวอร์ชันโปรเจกต์ของคุณ)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# 2. ใช้ SDK Image สำหรับทำการ Build โปรเจกต์
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# คัดลอกไฟล์โปรเจกต์และ Restore Dependencies
COPY ["LostAndFound.csproj", "./"]
RUN dotnet restore "LostAndFound.csproj"

# คัดลอกโค้ดทั้งหมดและ Build
COPY . .
RUN dotnet build "LostAndFound.csproj" -c Release -o /app/build

# 3. Publish โปรเจกต์
FROM build AS publish
RUN dotnet publish "LostAndFound.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 4. นำผลลัพธ์ไปรันใน Container
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LostAndFound.dll"]