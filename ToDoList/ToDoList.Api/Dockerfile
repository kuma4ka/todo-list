# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the csproj files and restore dependencies
COPY ["ToDoList.Api/ToDoList.Api.csproj", "ToDoList.Api/"]
COPY ["ToDoList.Application/ToDoList.Application.csproj", "ToDoList.Application/"]
COPY ["ToDoList.Domain/ToDoList.Domain.csproj", "ToDoList.Domain/"]
COPY ["ToDoList.Infrastructure/ToDoList.Infrastructure.csproj", "ToDoList.Infrastructure/"]

RUN dotnet restore "ToDoList.Api/ToDoList.Api.csproj"

# Copy all source files
COPY . .

# Build the application
WORKDIR "/src/ToDoList.Api"
RUN dotnet build "ToDoList.Api.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "ToDoList.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Create a non-root user for security
RUN addgroup --system appgroup && adduser --system --ingroup appgroup appuser
USER appuser

# Copy the published files
COPY --from=publish /app/publish .

# Set the environment to production
ENV ASPNETCORE_ENVIRONMENT=Production

# Expose the necessary port
EXPOSE 8080

# Use the read-only filesystem option in production
ENTRYPOINT ["dotnet", "ToDoList.Api.dll"]
