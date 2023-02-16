#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# run the unit tests
#FROM build AS test
# set the directory to be within the unit test project
WORKDIR "/src/Clinic.MSUnitTests"
# run the unit tests
RUN dotnet restore 

WORKDIR /src
COPY ["Clinic/Clinic.csproj", "Clinic/"]
RUN dotnet restore "Clinic/Clinic.csproj"
COPY . .
WORKDIR "/src/Clinic"
RUN dotnet build "Clinic.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Clinic.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clinic.dll"]
