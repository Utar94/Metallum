#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Metallum.Web/Metallum.Web.csproj", "src/Metallum.Web/"]
RUN dotnet restore "src/Metallum.Web/Metallum.Web.csproj"
COPY . .
WORKDIR "/src/src/Metallum.Web"
RUN dotnet build "Metallum.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Metallum.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Metallum.Web.dll"]