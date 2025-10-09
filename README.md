# properties-sale-backend
Bienvenido al README de properties-sale Backend! Este es un proyecto .NET desarrollado en C#, diseñado para ofrecer una API RESTful que facilita la recuperación de datos de propiedades inmobiliarias almacenados en MongoDB. Construido sobre una arquitectura hexagonal y buenas prácticas de desarrollo. Documentación opcional generada con IA: https://deepwiki.com/cristianmarind/properties-sale-backend

## Tabla de Contenidos

- [Requisitos](#requisitos)
- [Instalación](#instalación)
- [Ejecución del Proyecto](#ejecución-del-proyecto)
- [Persistencia de Datos en MongoDB](#persistencia-de-datos-en-mongodb)
- [Servicios Web o API](#servicios-web-o-api)
- [Arquitectura del Sistema](#arquitectura-del-sistema)
- [Pruebas](#pruebas)
- [Contribuciones](#contribuciones)
- [Licencia](#licencia)

## Requisitos

- [.NET SDK](https://dotnet.microsoft.com/download) versión 9
- [MongoDB](https://www.mongodb.com/try/download/community) acceso a una instancia en la nube en MongoDB Atlas
- [Git](https://git-scm.com/) para clonar el repositorio

## Instalación

1. Clona el repositorio:
```bash
git clone https://github.com/cristianmarind/properties-sale-backend
```

2. Navega al directorio del proyecto:
```bash
cd properties-sale-backend
```

3. Restaura las dependencias de NuGet:
```bash
dotnet restore
```

4. Las variables de entorno ya están disponibles en el archivo `appsettings.json` con la conexión a MongoDB con la 
intencion de facilitar el uso del proyecto. Nota: no se debe de hacer en entornos productivos.

## Ejecución del Proyecto

1. Inicia el proyecto
```bash
dotnet run --project .\properties-saleAPI\ 
```

2. Inicia el proyecto para continuar el desarrollo
```bash
dotnet watch run --project .\properties-saleAPI\ 
```

## Persistencia de Datos en MongoDB
La base de datos en MongoDB esta compuesta por las siguientes 2 colecciones:

1. Properties
- Ejemplo de documento
```json
{
  "_id": "153fa48c-527e-4844-a3bb-a89fb96fdf42",
  "Name": "PRODUCTOS RAMO CALDAS",
  "Address": {
    "Street": "Caldas Industrial Park",
    "City": "Manizales",
    "Country": "Colombia"
  },
  "Price": {
    "Amount": 750000,
    "Currency": "USD"
  },
  "CodeInternal": "PROP-2023-008",
  "Year": 2023,
  "OwnerId": "db7666fe-ab8d-4a68-8973-7d9d70fb7645",
  "PropertyImage": [
    {
      "ImageUrl": "url",
      "Enabled": true
    },
  ],
  "PropertyTrace": [
    {
      "Name": "Ampliación de la planta de producción para Ramo Caldas.",
      "Date": {
        "$date": "2016-09-01T00:00:00.000Z"
      },
      "Tax": 224.55176995469782,
      "Value": 6625.011192899204
    }
  ],
  "Location": {
    "type": "Point",
    "coordinates": [
      -75.524975,
      5.043889
    ]
  },
  "Category": 1
}
```

2. Owners
- Ejemplo de documento
```json
{
  "_id": "d88d4cc8-210d-4878-997f-cacffaef0c18",
  "Name": "Juan Pérez",
  "Address": "Calle Falsa 123, Bogotá, Colombia",
  "Birthday": {
    "$date": "1980-05-15T00:00:00.000Z"
  },
  "Properties": [
    "fdffcaa8-7477-43c5-ab68-b38695e63c76"
  ],
  "Photo": "img"
}
```

Se tomó la desición de agregar PropertyImage y PropertyTrace en la colección Properties 
para simplificar el modelo de datos y mejorar los tiempos de respuesta de las querys.
Asumiendo que estas 2 listas no son grandes por propiedad, y esta desicion no implica
problemas de tamaño de documento en MongoDB. Owners se optó por estar en una nueva 
colección asumiendo que un propietario puede tener cientos de propiedades, además
simplifica las querys hacia las propiedades.

Se agregaron los siguientes indices en la colección Properties para optimizar las consultas:
![Indices de MongoDB](Docs/MongoDbIndex.png)

## Servicios Web o API

1. GetProperties (POST api/properties/find)
Hace busqueda de propiedades basandose en una serie de parametros que son usados para formar una query a MongoDB
si se desea buscar por ubicación se emplea aggregate, de lo contrario un find basico.

2. GetOwners (POST api/properties/find) 
Hace busqueda de propietarios basandose en una serie de parametros

- El API tiene un estándar de respuesta donde se especifica el status, valor y error.
- El sistema tiene un error handling middleware para gestionar los errores, siguiendo el estandar de respuesta
- Se añade el patrón logger para almacenar los errores inesperados, al tener arquitectura hexagonal se abre
la posibilidad de añadir otros loggers tipo cloud
- La documentación swagger se encuentra disponible en /swagger/index.html

## Arquitectura del Sistema
Se emplea arquitectura hexagonal con la intension de establecer una base solida para el proyecto, los componentes 
principales son: properties-sale.Domain (Dominio), properties-sale.Application (Aplicación), properties-sale.properties-saleAPI (Infraestructura).

- Se emplea el patrón Repository otorgando atributos de calidad como: Mantenibilidad, Portabilidad, Testabilidad y Flexibilidad

## Pruebas
1. Ejecutar todos los tests
```bash
dotnet test
```

2. Ejecuta las pruebas y mira el estado del sistema de forma interactiva

Se instalo dotnet-reportgenerator-globaltool para lograr ver de una mejor forma
el resultado de los test.
```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
dotnet test --collect:"XPlat Code Coverage"
reportgenerator -reports:"properties-sale.Tests\TestResults\[ID del test generado]\coverage.cobertura.xml" -targetdir:"reporte-html" -reporttypes:Html
```
## Licencia

Este proyecto está licenciado bajo una variante de la MIT License.
