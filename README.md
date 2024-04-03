# collectionsApi
RESTful API about Collections

#### Como ejecutar en local

1. Para trabajar en local, generaremos un fichero llamado **appsettings.local.json** en el directorio ra�z del proyecto. El archivo deber� estar compuesto de la siguiente manera:
    ~~~
    {
    "ConnectionStrings": {
    "DefaultConnection": "Cadena de conexi�n a la BBDD de SQLServer",
    },
    "JwtSettings": {
    "SecretKey":Poner aqu� el HASH"
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
3. Ejecutar y utilizar la api que contiene la documentaci�n en la direcci�n: *urlapi/**swagger***

   - Si utilizamos Visual Studio podemos darle al bot�n verde de play.
   - Si lo hacemos desde un terminal linux/macOs:
    ~~~
    dotnet run
    ~~~