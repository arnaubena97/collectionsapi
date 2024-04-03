# collectionsApi
RESTful API about Collections

#### Como ejecutar en local

1. Para trabajar en local, generaremos un fichero llamado **appsettings.local.json** en el directorio raíz del proyecto. El archivo deberá estar compuesto de la siguiente manera:
    ~~~
    {
    "ConnectionStrings": {
    "DefaultConnection": "Cadena de conexión a la BBDD de SQLServer",
    },
    "JwtSettings": {
    "SecretKey":Poner aquí el HASH"
    }
    }
    ~~~
2. Una vez tenemos el archivo, abriremos la consola administradora de paquetes y aplicaremos las migraciones con el siguiente comando:
    ~~~
    update-database
    ~~~
    Si lo hacemos desde un terminal linux/macOs:
    ~~~
    dotnet ef database update
    ~~~
3. Ejecutar y utilizar la api que contiene la documentación en la dirección: *urlapi/**swagger***

   - Si utilizamos Visual Studio podemos darle al botón verde de play.
   - Si lo hacemos desde un terminal linux/macOs:
    ~~~
    dotnet run
    ~~~