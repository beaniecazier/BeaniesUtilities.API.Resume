FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["API/Resume.API.csproj", "Resume.API/API/"]
RUN dotnet restore "Resume.API/API/Resume.API.csproj"
COPY . .
WORKDIR "./API"
RUN dotnet publish "Resume.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 8080
EXPOSE 443
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Resume.API.dll"]
