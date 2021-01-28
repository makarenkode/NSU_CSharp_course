FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY ./*/*.csproj ./
#RUN ls
#RUN dotnet restore BookShop.Test.csproj
#RUN dotnet restore BookShop.Web.csproj
#RUN dotnet restore BookShopSecond.Data.csproj

COPY . ./
RUN dotnet publish BookShopSecond.sln -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "BookShop.Web.dll"]