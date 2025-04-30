
#  API de Gestión de Cuentas Bancarias

Este proyecto es una API desarrollada en ASP.NET Core 8.0 para la gestión de cuentas bancarias. Permite a los usuarios:

- Crear una cuenta bancaria con un saldo inicial.
- Consultar el saldo actual de una cuenta.
- Realizar depósitos y retiros con validación de fondos.
- Obtener un resumen de transacciones y calcular el saldo final.

## Tecnologías utilizadas

- ASP.NET Core 8.0
- C#
- Swagger (Swashbuckle.AspNetCore 8.1.1)
- **xUnit** para las pruebas unitarias (2.9.3)
- **xunit.runner.visualstudio** (2.8.2)
- **Microsoft.NET.Test.Sdk** (17.13.0)
## Instalación y ejecución

### 1. Clona el repositorio:
   ```bash
   git clone https://github.com/juanjose23/PruebaTecnica 
 cd prueba-tecnica-banco
```
### 2. Restaura los paquetes y ejecuta el proyecto:
 ```bash
 dotnet restore
 dotnet run 
 ```

3. Accede a la documentación Swagger en:
```bash
https://localhost:5001/swagger
```
### Instrucciones  para pruebas unitarias

### 1. Restaurar las dependencias
Navega al directorio del proyecto y ejecuta el siguiente comando para restaurar las dependencias:
```bash
cd PruebaTecnica/TestServicioBancario
dotnet restore 
```
### 2. Construir el proyecto
Construye el proyecto para asegurarte de que todo está configurado correctamente (Esta Instruccion en la raiz del proyecto):

```bash
dotnet build
```
Esto compilará todos los proyectos en la solución y preparará el entorno para la ejecución (Esta Instruccion en la raiz del proyecto):.

### 3. Ejecutar las pruebas
Para ejecutar las pruebas unitarias:
```bash
dotnet test
```

## API Reference

### Todos los métodos GET
#### Consultar Cuenta

```http
  GET /CuentasBancarias/consultar/{numeroCuenta}
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `numeroCuenta` | `string($uuid)` | **Requerido**.  |

#### Consultar todas las transaciones

```http
  GET /CuentasBancarias/consultar/{numeroCuenta}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `numeroCuenta`      | `string($uuid)` | **Requerido**.  |

### Todos los métodos POST
#### Crear Cuenta

```http
  POST /CuentasBancarias/crear
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `saldoInicial` | `number` | **Requirido**, **Solo Numeros positivos**  |


#### Crear transaciones

```http
  POST /CuentasBancarias/transaccion/{numeroCuenta}
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `numeroCuenta` | `string($uuid)` | **Requirido**  |
| `tipo` | `string` | **Requirido**, **"deposito"** **O** **"retiro"**  |
| `monto` | `numerico` | **Requirido** |


## Pruebas realizadas
### 1. CrearCuenta_DeberiaCrearCuentaConSaldoInicial

**Objetivo**: Verificar que una cuenta se crea correctamente con un saldo inicial.

- Se crea una cuenta con un saldo inicial de **1000**.
- Se asegura que la cuenta tiene una transacción inicial con tipo "inicial".
- Se valida que el saldo de la cuenta coincida con el saldo inicial.

```bash
[Fact]
public void CrearCuenta_DeberiaCrearCuentaConSaldoInicial()
{
    // Arrange
    decimal saldoInicial = 1000;

    // Act
    var cuenta = _servicio.CrearCuenta(saldoInicial);

    // Assert
    Assert.NotNull(cuenta);
    Assert.Equal(saldoInicial, cuenta.Saldo);
    Assert.Single(cuenta.Transacciones);
    Assert.Equal("inicial", cuenta.Transacciones[0].Tipo);
    Assert.Equal(saldoInicial, cuenta.Transacciones[0].SaldoDespues);
}
 ```

### 2. RealizarDeposito_DeberiaIncrementarSaldo
Objetivo: Verificar que un depósito aumenta correctamente el saldo de la cuenta.
- Se crea una cuenta con un saldo de 500.
- Se realiza un depósito de 300.
- Se valida que el saldo después del depósito sea 800.
```bash
[Fact]
public void RealizarDeposito_DeberiaIncrementarSaldo()
    {
        // Arrange
        var cuenta = _servicio.CrearCuenta(500);
        decimal deposito = 300;

        // Act
        var transaccion = _servicio.RealizarTransaccion(cuenta.NumeroCuenta, TipoTransaccion.Deposito, deposito);

        // Assert
        Assert.Equal(800, cuenta.Saldo);
        Assert.Equal("deposito", transaccion.Tipo);
        Assert.Equal(800, transaccion.SaldoDespues);
    }
 
 ```

### 3. RealizarRetiro_DeberiaDisminuirSaldo
Objetivo: Verificar que un retiro disminuye correctamente el saldo de la cuenta.

- Se crea una cuenta con un saldo de 500.

- Se realiza un retiro de 200.

- Se valida que el saldo después del retiro sea 300.

```bash
[Fact]
public void RealizarRetiro_DeberiaDisminuirSaldo()
  {
      // Arrange
      var cuenta = _servicio.CrearCuenta(500);
      decimal retiro = 200;

      // Act
      var transaccion = _servicio.RealizarTransaccion(cuenta.NumeroCuenta, TipoTransaccion.Retiro, retiro);

      // Assert
      Assert.Equal(300, cuenta.Saldo);
      Assert.Equal("retiro", transaccion.Tipo);
      Assert.Equal(300, transaccion.SaldoDespues);
  }
```
### 4. RealizarRetiroConFondosInsuficientes_DeberiaLanzarExcepcion
Objetivo: Verificar que se lanza una excepción cuando se intenta realizar un retiro con fondos insuficientes.

- Se crea una cuenta con un saldo de 100.

- Se intenta hacer un retiro de 200.

- Se valida que se lance una excepción de tipo InvalidOperationException.
```bash
[Fact]
public void RealizarRetiroConFondosInsuficientes_DeberiaLanzarExcepcion()
  {
      // Arrange
      var cuenta = _servicio.CrearCuenta(100);
      decimal retiro = 200;

      // Act & Assert
      Assert.Throws<InvalidOperationException>(() =>
          _servicio.RealizarTransaccion(cuenta.NumeroCuenta, TipoTransaccion.Retiro, retiro));
  }
```
### 5. ConsultarTransacciones_DeberiaRetornarTodasLasTransacciones
Objetivo: Verificar que todas las transacciones se registren correctamente y se devuelvan al consultar las transacciones de una cuenta.

- Se crea una cuenta con un saldo de 500.

- Se realizan dos transacciones: un depósito de 200 y un retiro de 100.

- Se valida que se registren tres transacciones (la inicial + las dos realizadas).

```bash
[Fact]
public void ConsultarTransacciones_DeberiaRetornarTodasLasTransacciones()
  {
      // Arrange
      var cuenta = _servicio.CrearCuenta(500);
      _servicio.RealizarTransaccion(cuenta.NumeroCuenta, TipoTransaccion.Deposito, 200);
      _servicio.RealizarTransaccion(cuenta.NumeroCuenta, TipoTransaccion.Retiro, 100);

      // Act
      var transacciones = _servicio.ConsultarTransacciones(cuenta.NumeroCuenta);

      // Assert
      Assert.Equal(3, transacciones.Count); // incluye la transacción inicial
  }
  ```
