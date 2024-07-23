# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official .NET SDK as a build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ConvertoApi.csproj", "ConvertoApi/"]
RUN dotnet restore "ConvertoApi/ConvertoApi.csproj"
COPY . .
WORKDIR "/src/ConvertoApi"
RUN dotnet build "ConvertoApi.csproj" -c Release -o /build

FROM build AS publish
RUN dotnet publish "ConvertoApi.csproj" -c Release -o /publish

FROM base AS final
WORKDIR /app
COPY --from=publish /publish .
ENTRYPOINT ["dotnet", "ConvertoApi.dll"]
