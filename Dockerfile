# ===== BUILD =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# copia só o csproj
COPY TaskPulse/*.csproj ./TaskPulse.API/
RUN dotnet restore ./TaskPulse/TaskPulse.API.csproj

# copia o resto
COPY . .
RUN dotnet publish ./TaskPulse/TaskPulse.API.csproj -c Release -o out

# ===== RUNTIME =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "TaskPulse.API.dll"]
