FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5062

ENV ASPNETCORE_URLS=http://+:5062

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["RentalCompany.Api/RentalCompany.Api.csproj", "RentalCompany.Api/"]
RUN dotnet restore "RentalCompany.Api/RentalCompany.Api.csproj"
COPY . .
WORKDIR "/src/RentalCompany.Api"
RUN dotnet build "RentalCompany.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "RentalCompany.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentalCompany.Api.dll"]
