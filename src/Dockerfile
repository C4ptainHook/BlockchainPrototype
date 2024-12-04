FROM mcr/microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY . .

RUN dotnet restore

EXPOSE 5000

CMD ["dotnet", "watch", "run"]



