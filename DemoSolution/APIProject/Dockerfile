#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["APIProject/APIProject.csproj", "APIProject/"]
RUN dotnet restore "APIProject/APIProject.csproj"
COPY . .
WORKDIR "/src/APIProject"
RUN dotnet build "APIProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "APIProject.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APIProject.dll"]