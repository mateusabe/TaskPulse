# ===== BUILD =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY TaskPulse/TaskPulse.API.csproj TaskPulse/
RUN dotnet restore TaskPulse/TaskPulse.API.csproj

COPY . .
RUN dotnet publish TaskPulse/TaskPulse.API.csproj -c Release -o out

# ===== RUNTIME =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://0.0.0.0:$PORT

ENTRYPOINT ["dotnet", "TaskPulse.API.dll"]
