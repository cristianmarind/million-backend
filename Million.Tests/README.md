# Tests Unitarios - Proyecto Million

Este proyecto contiene las pruebas unitarias para el proyecto Million, implementadas usando **NUnit** y **Moq**.

## 📁 Estructura de Tests

```
Million.Tests/
├── Domain/
│   ├── Entities/
│   │   ├── PropertyTests.cs          # Tests para la entidad Property
│   │   └── OwnerTests.cs             # Tests para la entidad Owner
│   └── ValueObjects/
│       ├── AddressTests.cs           # Tests para el Value Object Address
│       ├── PriceTests.cs             # Tests para el Value Object Price
│       ├── PropertyTraceTests.cs     # Tests para el Value Object PropertyTrace
│       ├── PropertyImageTests.cs     # Tests para el Value Object PropertyImage
│       └── PresentationConfigTests.cs # Tests para el Value Object PresentationConfig
├── Application/
│   └── Services/
│       ├── PropertyServiceTests.cs   # Tests para PropertyService
│       └── OwnerServiceTests.cs      # Tests para OwnerService
└── API/
    └── Controllers/
        ├── PropertiesControllerTests.cs # Tests para PropertiesController
        └── OwnerControllerTests.cs      # Tests para OwnerController
```

## 🧪 Tipos de Tests Implementados

### **1. Tests de Entidades del Dominio**
- **Property**: Validaciones del constructor, reglas de negocio
- **Owner**: Validaciones del constructor, reglas de negocio

### **2. Tests de Value Objects**
- **Address**: Validaciones de campos requeridos
- **Price**: Validaciones de montos negativos, moneda por defecto
- **PropertyTrace**: Validaciones de fechas, valores positivos, impuestos
- **PropertyImage**: Validaciones de archivo requerido
- **PresentationConfig**: Validaciones de índice de imagen, clase requerida

### **3. Tests de Servicios de Aplicación**
- **PropertyService**: Lógica de mapeo de entidades a DTOs, transformaciones
- **OwnerService**: Lógica de mapeo de entidades a DTOs, transformaciones

### **4. Tests de Controladores**
- **PropertiesController**: Validación de entrada, manejo de respuestas HTTP
- **OwnerController**: Validación de entrada, manejo de respuestas HTTP

## 🚀 Cómo Ejecutar los Tests

### **Desde la Línea de Comandos**

```bash
# Navegar al directorio del proyecto
cd Million

# Ejecutar todos los tests
dotnet test

# Ejecutar tests con información detallada
dotnet test --verbosity normal

# Ejecutar tests con cobertura de código
dotnet test --collect:"XPlat Code Coverage"

# Ejecutar tests específicos
dotnet test --filter "ClassName=PropertyTests"

# Ejecutar generador de visor de resultados de tests
reportgenerator -reports:"Million.Tests\TestResults\f97828f2-6c59-4eaa-bfd9-32aa857dab15\coverage.cobertura.xml" -targetdir:"reporte-html" -reporttypes:Html
```

### **Desde Visual Studio**
1. Abrir el **Test Explorer** (Test → Test Explorer)
2. Hacer clic en **Run All** para ejecutar todos los tests
3. O ejecutar tests individuales haciendo clic derecho en el test específico

### **Desde Visual Studio Code**
1. Instalar la extensión **.NET Core Test Explorer**
2. Abrir el panel de tests (Ctrl+Shift+P → "Test: Focus on Test Explorer View")
3. Ejecutar los tests desde el panel

## 📊 Cobertura de Tests

Los tests cubren:

- ✅ **Validaciones de entrada** en constructores
- ✅ **Reglas de negocio** en entidades
- ✅ **Mapeo de datos** en servicios
- ✅ **Respuestas HTTP** en controladores
- ✅ **Casos edge** y valores límite
- ✅ **Manejo de errores** y excepciones

## 🔧 Dependencias Utilizadas

- **NUnit 4.2.2**: Framework de testing
- **Moq 4.20.72**: Framework de mocking
- **Microsoft.AspNetCore.Mvc.Testing 9.0.0**: Testing de controladores
- **coverlet.collector 6.0.2**: Cobertura de código

## 📝 Ejemplos de Tests

### **Test de Validación de Entidad**
```csharp
[Test]
public void Property_Constructor_WithInvalidName_ThrowsArgumentException()
{
    // Arrange
    var name = "";
    
    // Act & Assert
    var ex = Assert.Throws<ArgumentException>(() => new Property(name, ...));
    Assert.That(ex.Message, Does.Contain("Property name is required"));
}
```

### **Test de Servicio con Mock**
```csharp
[Test]
public async Task GetPropertiesByFilterAsync_WithValidFilter_ReturnsMappedPropertyDtos()
{
    // Arrange
    _mockRepository.Setup(r => r.GetByFilterAsync(filter))
                  .ReturnsAsync(properties);

    // Act
    var result = await _service.GetPropertiesByFilterAsync(filter);

    // Assert
    Assert.That(result, Is.Not.Null);
    Assert.That(result.Count(), Is.EqualTo(2));
}
```

## 🎯 Mejores Prácticas Implementadas

1. **Arrange-Act-Assert**: Estructura clara en todos los tests
2. **Nombres descriptivos**: Los nombres de los tests explican qué se está probando
3. **Mocks apropiados**: Uso de Moq para aislar dependencias
4. **Casos edge**: Tests para valores límite y casos especiales
5. **Validaciones completas**: Tests para casos de éxito y error
6. **Setup y Teardown**: Uso de `[SetUp]` para preparar datos de prueba

## 🔍 Troubleshooting

### **Error: "No test assemblies found"**
```bash
# Restaurar paquetes NuGet
dotnet restore

# Reconstruir el proyecto
dotnet build
```

### **Error: "Test discovery failed"**
```bash
# Limpiar y reconstruir
dotnet clean
dotnet build
dotnet test
```

### **Error de dependencias**
```bash
# Actualizar paquetes
dotnet add package NUnit --version 4.2.2
dotnet add package Moq --version 4.20.72
```
