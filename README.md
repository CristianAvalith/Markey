# Markey

Aplicación web desarrollada en .NET 8 con SQL Server como base de datos.

# Requisitos previos

Antes de ejecutar el proyecto, asegurate de tener instalado:

✅ .NET SDK 8.0

✅ SQL Server


# Configuración inicial

Abrí el archivo:

Markey.Server/appsettings.Development.json


Modificá la cadena de conexión para que apunte a tu instancia local de SQL Server.
Ejemplo:

"ConnectionStrings": {
  "MarkeyDatabase": "Server=localhost\\SQLEXPRESS;Database=MarkeyDB;Trusted_Connection=True;TrustServerCertificate=True;"
}

# Ejecución del proyecto

Abrí una terminal o CMD en la carpeta del servidor:

cd Markey.Server

Ejecutá la aplicación:

dotnet run

El sistema creará automáticamente la base de datos (si no existe), aplicará las migraciones y cargará los datos iniciales (seeds).

# DOCUMENTACION BACKEND
http://localhost:5172/swagger/index.html