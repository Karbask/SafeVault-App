# Validación del proyecto

Este proyecto está preparado para abrirse y ejecutarse desde VS Code con .NET SDK 8 o superior.

## Comandos previstos

Arrancar la API:

```bash
dotnet run --project SafeVault.Api/SafeVault.Api.csproj
```

Ejecutar pruebas:

```bash
dotnet test SafeVault.Tests/SafeVault.Tests.csproj
```

## Estado de validación en este entorno

En este entorno no está instalado el SDK de .NET, por lo que no se ha podido ejecutar `dotnet test` directamente aquí.

Aun así, se ha revisado la estructura del proyecto y se han incluido todos los archivos necesarios para ejecutarlo en local:

- Proyecto API Minimal API en `SafeVault.Api`.
- Proyecto de pruebas NUnit en `SafeVault.Tests`.
- Archivo `request.http` para probar los endpoints desde VS Code.
- Scripts `run-api.bat` y `run-tests.bat` para Windows.
- README con instrucciones de ejecución.

Antes de subirlo a GitHub, recomiendo ejecutar en tu PC:

```bash
dotnet restore
dotnet test SafeVault.Tests/SafeVault.Tests.csproj
dotnet run --project SafeVault.Api/SafeVault.Api.csproj
```

Después, abrir `request.http` en VS Code y lanzar las peticiones en orden.
