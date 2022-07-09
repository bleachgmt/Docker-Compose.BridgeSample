#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Sample.Client/Sample.Client.csproj", "Sample.Client/"]
RUN dotnet restore "Sample.Client/Sample.Client.csproj"
COPY . .
WORKDIR "/src/Sample.Client"
RUN dotnet build "Sample.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sample.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.Client.dll"]