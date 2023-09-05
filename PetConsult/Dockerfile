#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PetConsult/PetConsult.csproj", "PetConsult/"]
RUN dotnet restore "PetConsult/PetConsult.csproj"
COPY . .
WORKDIR "/src/PetConsult"
RUN dotnet build "PetConsult.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PetConsult.csproj" -c Release -o /app/publish /p:UseAppHost=false

EXPOSE 80
EXPOSE 443

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "PetConsult.dll"]