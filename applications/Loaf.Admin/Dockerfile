FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["applications/Loaf.Admin/Loaf.Admin.csproj", "Loaf.Admin/"]
RUN dotnet restore "applications/Loaf.Admin/Loaf.Admin.csproj"
COPY . .
WORKDIR "/src/Loaf.Admin"
RUN dotnet build "Loaf.Admin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Loaf.Admin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Loaf.Admin.dll"]
