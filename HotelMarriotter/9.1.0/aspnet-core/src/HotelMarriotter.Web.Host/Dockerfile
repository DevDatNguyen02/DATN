FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/HotelMarriotter.Web.Host/HotelMarriotter.Web.Host.csproj", "src/HotelMarriotter.Web.Host/"]
COPY ["src/HotelMarriotter.Web.Core/HotelMarriotter.Web.Core.csproj", "src/HotelMarriotter.Web.Core/"]
COPY ["src/HotelMarriotter.Application/HotelMarriotter.Application.csproj", "src/HotelMarriotter.Application/"]
COPY ["src/HotelMarriotter.Core/HotelMarriotter.Core.csproj", "src/HotelMarriotter.Core/"]
COPY ["src/HotelMarriotter.EntityFrameworkCore/HotelMarriotter.EntityFrameworkCore.csproj", "src/HotelMarriotter.EntityFrameworkCore/"]
WORKDIR "/src/src/HotelMarriotter.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/HotelMarriotter.Web.Host", "src/HotelMarriotter.Web.Host"]
COPY ["src/HotelMarriotter.Web.Core", "src/HotelMarriotter.Web.Core"]
COPY ["src/HotelMarriotter.Application", "src/HotelMarriotter.Application"]
COPY ["src/HotelMarriotter.Core", "src/HotelMarriotter.Core"]
COPY ["src/HotelMarriotter.EntityFrameworkCore", "src/HotelMarriotter.EntityFrameworkCore"]
WORKDIR "/src/src/HotelMarriotter.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "HotelMarriotter.Web.Host.dll"]
