# SafeVault - Aplicación de código seguro

Este proyecto es una versión sencilla de **SafeVault**, preparada para poder abrirse y ejecutarse fácilmente desde **Visual Studio Code**.

La idea del proyecto es demostrar varias prácticas básicas de codificación segura en una aplicación web/API:

- Validación de datos introducidos por el usuario.
- Prevención de inyección SQL mediante consultas parametrizadas.
- Protección frente a XSS mediante codificación de salida.
- Autenticación de usuarios.
- Hash seguro de contraseñas.
- Autorización basada en roles, también llamada RBAC.
- Pruebas unitarias para comprobar los casos principales de seguridad.

La aplicación usa almacenamiento en memoria para que se pueda probar rápido sin instalar MySQL ni configurar una base de datos. Aun así, se incluye un archivo `database.sql` y un ejemplo de consulta parametrizada para dejar claro cómo se aplicaría en una base de datos real.

## Cómo he usado Copilot

Durante el desarrollo he usado Microsoft Copilot como ayuda, no como sustituto del trabajo. Me ha servido sobre todo para plantear una primera estructura, recordar buenas prácticas de seguridad y proponer casos de prueba.

Después he revisado el código, lo he adaptado al escenario de SafeVault y he simplificado algunas partes para que el proyecto pueda ejecutarse de forma cómoda desde VS Code.

Copilot me ayudó especialmente en:

- Plantear la clase de validación de entrada.
- Revisar ejemplos de intentos de SQL Injection y XSS.
- Recordar que las contraseñas no deben guardarse en texto plano.
- Crear una base para el control de acceso por roles.
- Proponer pruebas unitarias para comprobar usuarios normales, administradores y entradas maliciosas.

## Requisitos

Para ejecutarlo necesitas:

- Visual Studio Code.
- .NET SDK 8 o superior.
- Extensión de VS Code: **C# Dev Kit** o **C#**.
- Extensión recomendada para probar `request.http`: **REST Client**.

El repositorio incluye `.vscode/extensions.json`, por lo que VS Code debería sugerir las extensiones recomendadas al abrir la carpeta.

## Cómo ejecutar la API

Desde la raíz del proyecto:

```bash
dotnet run --project SafeVault.Api/SafeVault.Api.csproj
```

En Windows también se puede usar:

```bash
run-api.bat
```

La API se levanta en:

```text
http://localhost:5157
```

Para comprobar que funciona:

```http
GET http://localhost:5157/health
```

## Cómo probar las request

El proyecto incluye el archivo:

```text
request.http
```

Ahí están preparadas las peticiones principales:

- Comprobar estado de la API.
- Login con usuario normal.
- Login con administrador.
- Acceso permitido al panel de administración.
- Acceso denegado para usuario normal.
- Registro de usuario válido.
- Prueba de SQL Injection.
- Prueba de XSS.
- Prueba de salida HTML segura.

Usuarios de prueba incluidos al arrancar la aplicación:

| Usuario | Contraseña | Rol |
|---|---|---|
| `admin` | `Admin123!` | `admin` |
| `usuario` | `User1234!` | `user` |

## Cómo ejecutar los tests

Desde la raíz del proyecto:

```bash
dotnet test SafeVault.Tests/SafeVault.Tests.csproj
```

En Windows también se puede usar:

```bash
run-tests.bat
```

Las pruebas revisan:

- Validación de usuario correcto.
- Rechazo de SQL Injection.
- Rechazo de XSS en nombre de usuario.
- Validación de email.
- Hash y verificación de contraseñas.
- Acceso permitido para administradores.
- Acceso bloqueado para usuarios normales.
- Codificación segura de salida HTML.

## Estructura del proyecto

```text
SafeVault-VSCode/
│
├── request.http
├── database.sql
├── README.md
├── run-api.bat
├── run-tests.bat
│
├── SafeVault.Api/
│   ├── Program.cs
│   ├── Models/
│   ├── Security/
│   └── Data/
│
└── SafeVault.Tests/
    ├── InputValidatorTests.cs
    ├── PasswordServiceTests.cs
    ├── AuthorizationServiceTests.cs
    ├── OutputEncoderTests.cs
    └── AuthServiceTests.cs
```

## Resumen de vulnerabilidades y correcciones

| Vulnerabilidad revisada | Riesgo | Corrección aplicada |
|---|---|---|
| Entrada de usuario sin validar | Datos inesperados o maliciosos | Validación estricta de username, email y password |
| Concatenación de cadenas SQL | SQL Injection | Uso de consultas parametrizadas como referencia |
| Contraseñas en texto plano | Robo de credenciales | Hash con PBKDF2 y sal aleatoria |
| Salida HTML sin codificar | XSS | Codificación con `HtmlEncode` |
| Acceso admin sin control de rol | Acceso no autorizado | RBAC con roles `admin` y `user` |
| Usuario no autenticado | Acceso indebido | Token de sesión simple para la demo |

## Nota sobre la base de datos

Para que el proyecto sea fácil de ejecutar en VS Code, la API usa un repositorio en memoria (`InMemoryUserRepository`). Esto evita tener que instalar MySQL o configurar cadenas de conexión.

Aun así, se incluye `SafeSqlExamples.cs`, donde se deja documentada la diferencia entre una consulta insegura y una consulta parametrizada. También se incluye `database.sql` por si se quiere migrar el ejemplo a una base de datos real.

## Respuestas de la entrega

**¿Creaste un repositorio GitHub para tu proyecto?**  
Sí.

**¿Has utilizado Copilot para generar código seguro para la validación de entrada y la prevención de inyección SQL?**  
Sí.

**¿Has utilizado Copilot para implementar mecanismos de autenticación y autorización, incluido RBAC?**  
Sí.

**¿Has depurado y resuelto vulnerabilidades como SQL Injection y XSS?**  
Sí.

**¿Generaste y ejecutaste pruebas para verificar la seguridad de la aplicación?**  
Sí.

**¿Incluiste un resumen de vulnerabilidades, correcciones y uso de Copilot?**  
Sí.
