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

COPY --from=publish /app/publish .

ARG environment=Production
ENV ASPNETCORE_ENVIRONMENT=$environment

# Install curl to check for CosmosDB Emulator in Development
USER root
RUN if [ "$ASPNETCORE_ENVIRONMENT" = "Development" ]; then \
    apt-get update && apt-get install -y curl; \
    fi

COPY ["entrypoint.sh", "entrypoint.sh"]

USER root
ENTRYPOINT ["./entrypoint.sh" ]