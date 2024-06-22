FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

LABEL author="datagriff"

USER app

#RUN chmod +x entrypoint.sh

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /dog_adopter_console
COPY ["dog_adopter.csproj", "dog_adopter_console/"]
RUN dotnet restore "dog_adopter_console/dog_adopter.csproj"
COPY . .
WORKDIR "/dog_adopter_console"
RUN dotnet build "dog_adopter.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "dog_adopter.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

# Install curl
USER root
RUN apt-get update && apt-get install -y curl

COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "dog_adopter.dll"]

# WORKDIR /dog_adopter_console
COPY ["entrypoint.sh", "entrypoint.sh"]

# convert line endings to unix
# RUN dos2unix ./entrypoint.sh 

USER app
ENTRYPOINT ["./entrypoint.sh", "azurecosmosemulator", "8081"]
