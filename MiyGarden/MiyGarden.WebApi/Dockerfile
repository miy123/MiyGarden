#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MiyGarden.WebApi/MiyGarden.WebApi.csproj", "MiyGarden.WebApi/"]
COPY ["MiyGarden.Service/MiyGarden.Service.csproj", "MiyGarden.Service/"]
COPY ["MiyGarden.Models/MiyGarden.Models.csproj", "MiyGarden.Models/"]
RUN dotnet restore "MiyGarden.WebApi/MiyGarden.WebApi.csproj"
COPY . .
WORKDIR "/src/MiyGarden.WebApi"
RUN dotnet build "MiyGarden.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MiyGarden.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MiyGarden.WebApi.dll"]