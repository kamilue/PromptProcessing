FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Prompt.Api/Prompt.Api.csproj", "Prompt.Api/"]
COPY ["Prompt.Infrastructure/Prompt.Infrastructure.csproj", "Prompt.Infrastructure/"]
COPY ["Prompt.Application/Prompt.Application.csproj", "Prompt.Application/"]
COPY ["Prompt.Domain/Prompt.Domain.csproj", "Prompt.Domain/"]

RUN dotnet --version && \
	major=$(dotnet --version | cut -d'.' -f1) && \
	if [ "$major" -lt 10 ]; then echo "Required .NET SDK >= 10.0 but found $(dotnet --version)" && exit 1; fi && \
	dotnet restore "Prompt.Api/Prompt.Api.csproj"

COPY . .
RUN dotnet publish "Prompt.Api/Prompt.Api.csproj" -c Release -o /app/publish /p:PublishTrimmed=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS="http://+:80"
EXPOSE 80
ENTRYPOINT ["dotnet", "Prompt.Api.dll"]
