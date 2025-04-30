
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
## Instalación y ejecución

1. Clona el repositorio:
   ```bash
   git clone https://github.com/juanjose23/PruebaTecnica
   cd prueba-tecnica-banco
2. Restaura los paquetes y ejecuta el proyecto:
 ```bash
 dotnet restore
 dotnet run 
 ```
3. Accede a la documentación Swagger en:
```bash
https://localhost:5001/swagger
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
  GET /CuentasBancarias/transaciones/{numeroCuenta}
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
