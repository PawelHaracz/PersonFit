FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PersonFit.Frontend.Web.Wasm/PersonFit.Frontend.Web.Wasm.csproj", "PersonFit.Frontend.Web.Wasm/"]
RUN dotnet restore "PersonFit.Frontend.Web.Wasm/PersonFit.Frontend.Web.Wasm.csproj"
COPY . .
WORKDIR "/src/PersonFit.Frontend.Web.Wasm"
RUN dotnet build "PersonFit.Frontend.Web.Wasm.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PersonFit.Frontend.Web.Wasm.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonFit.Frontend.Web.Wasm.dll"]
