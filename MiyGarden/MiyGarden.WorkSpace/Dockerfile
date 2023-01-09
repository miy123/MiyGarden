#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MiyGarden.WorkSpace/MiyGarden.WorkSpace.csproj", "MiyGarden.WorkSpace/"]
COPY ["MiyGarden.Service/MiyGarden.Service.csproj", "MiyGarden.Service/"]
COPY ["MiyGarden.Models/MiyGarden.Models.csproj", "MiyGarden.Models/"]
RUN dotnet restore "MiyGarden.WorkSpace/MiyGarden.WorkSpace.csproj"
COPY . .
WORKDIR "/src/MiyGarden.WorkSpace"
RUN dotnet build "MiyGarden.WorkSpace.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MiyGarden.WorkSpace.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MiyGarden.WorkSpace.dll"]