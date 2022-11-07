FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY . .
RUN dotnet restore "PersonFit/PersonFit.csproj"

#WORKDIR /src/PersonFit
#RUN dotnet build "PersonFit.csproj" -c Release -o /app --no-restore

FROM build AS publish
WORKDIR /src/PersonFit
RUN dotnet publish "PersonFit.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonFit.dll"]
