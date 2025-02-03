# Usa la imagen base de .NET SDK para compilar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia los archivos del proyecto y restaura las dependencias
COPY *.csproj ./
RUN dotnet restore

# Copia todos los archivos del proyecto y construye la aplicación
COPY . ./
RUN dotnet publish -c Release -o out

# Usa una imagen más ligera para ejecutar la aplicación (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Exponer el puerto en el contenedor
EXPOSE 80
EXPOSE 443

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "WebApplication1.dll"]
