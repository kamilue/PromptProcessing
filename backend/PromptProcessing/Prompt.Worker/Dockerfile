FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Prompt.Worker/Prompt.Worker.csproj", "Prompt.Worker/"]
COPY ["Prompt.Infrastructure/Prompt.Infrastructure.csproj", "Prompt.Infrastructure/"]
COPY ["Prompt.Application/Prompt.Application.csproj", "Prompt.Application/"]
COPY ["Prompt.Domain/Prompt.Domain.csproj", "Prompt.Domain/"]

RUN dotnet restore "Prompt.Worker/Prompt.Worker.csproj"

COPY . .
RUN dotnet publish "Prompt.Worker/Prompt.Worker.csproj" -c Release -o /app/publish /p:PublishTrimmed=false

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Prompt.Worker.dll"]
