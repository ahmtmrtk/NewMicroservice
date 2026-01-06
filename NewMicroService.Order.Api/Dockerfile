# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NewMicroService.Order.Api/NewMicroService.Order.Api.csproj", "NewMicroService.Order.Api/"]
COPY ["NewMicroService.Order.Application/NewMicroService.Order.Application.csproj", "NewMicroService.Order.Application/"]
COPY ["NewMicroService.Order.Domain/NewMicroService.Order.Domain.csproj", "NewMicroService.Order.Domain/"]
COPY ["NewMicroservice.Shared/NewMicroservice.Shared.csproj", "NewMicroservice.Shared/"]
COPY ["NewMicroservice.Bus/NewMicroservice.Bus.csproj", "NewMicroservice.Bus/"]
COPY ["NewMicroService.Order.Persistance/NewMicroService.Order.Persistance.csproj", "NewMicroService.Order.Persistance/"]
RUN dotnet restore "./NewMicroService.Order.Api/NewMicroService.Order.Api.csproj"
COPY . .
WORKDIR "/src/NewMicroService.Order.Api"
RUN dotnet build "./NewMicroService.Order.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NewMicroService.Order.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewMicroService.Order.Api.dll"]