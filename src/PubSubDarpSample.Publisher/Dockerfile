#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PubSubDarpSample.Publisher/PubSubDarpSample.Publisher.csproj", "PubSubDarpSample.Publisher/"]
RUN dotnet restore "PubSubDarpSample.Publisher/PubSubDarpSample.Publisher.csproj"
COPY . .
WORKDIR "/src/PubSubDarpSample.Publisher"
RUN dotnet build "PubSubDarpSample.Publisher.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PubSubDarpSample.Publisher.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PubSubDarpSample.Publisher.dll"]