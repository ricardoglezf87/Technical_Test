#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 8
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Technical_Test/Technical_Test.csproj", "Technical_Test/"]
RUN dotnet restore "Technical_Test/Technical_Test.csproj"
COPY . .
WORKDIR "/src/Technical_Test"
RUN dotnet build "Technical_Test.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Technical_Test.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Technical_Test.dll"]
