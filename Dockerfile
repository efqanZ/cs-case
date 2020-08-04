FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY CiSeCase.sln .
COPY src/CiSeCase.Api/CiSeCase.Api.csproj ./src/CiSeCase.Api/
COPY src/CiSeCase.Core/CiSeCase.Core.csproj ./src/CiSeCase.Core/
COPY src/CiSeCase.Infrastructure/CiSeCase.Infrastructure.csproj ./src/CiSeCase.Infrastructure/
COPY test/CiSeCase.IntegrationTest/CiSeCase.IntegrationTest.csproj ./test/CiSeCase.IntegrationTest/
COPY test/CiSeCase.UnitTest/CiSeCase.UnitTest.csproj ./test/CiSeCase.UnitTest/
RUN dotnet restore

# Copy everything else and build
COPY src/CiSeCase.Api/. ./CiSeCase.Api
COPY src/CiSeCase.Core/. ./CiSeCase.Core
COPY src/CiSeCase.Infrastructure/. ./CiSeCase.Infrastructure

WORKDIR /app/CiSeCase.Api
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as runtime
WORKDIR /app
COPY --from=build /app/CiSeCase.Api/out ./
ENTRYPOINT ["dotnet", "CiSeCase.Api.dll"]