using PruebaTecnica.Services.Interfaces;
using PruebaTecnica.Models;
using PruebaTecnica.Enums;
namespace PruebaTecnica.Services
{
    public class ServicioBancario : IServicioBancario
    {
        private readonly List<CuentaBancaria> _cuentas = new();

        public CuentaBancaria CrearCuenta( decimal saldoInicial)
        {
            var cuenta = new CuentaBancaria
            {
                NumeroCuenta = Guid.NewGuid(),
                Saldo = saldoInicial,
                FechaCreacion = DateTime.UtcNow,
                Transacciones = new List<Transaccion>
                {
                    new Transaccion
                    {
                        Id = Guid.NewGuid(),
                        Tipo = "inicial",
                        Monto = saldoInicial,
                        SaldoDespues = saldoInicial,
                        Fecha = DateTime.UtcNow
                    }
                }
            };

            _cuentas.Add(cuenta);
            return cuenta;
        }

        public CuentaBancaria ConsultarCuenta(Guid numeroCuenta)
        {
            return _cuentas.FirstOrDefault(c => c.NumeroCuenta == numeroCuenta);
        }

        public Transaccion RealizarTransaccion(Guid numeroCuenta, TipoTransaccion tipo, decimal monto)
        {
            var cuenta = ConsultarCuenta(numeroCuenta);
            if (cuenta == null)
                throw new InvalidOperationException("Cuenta no encontrada.");

            // Validar si hay fondos suficientes antes de retirar
            if (tipo == TipoTransaccion.Retiro && cuenta.Saldo < monto)
                throw new InvalidOperationException("Fondos insuficientes.");

            // Aplicar la transacción según el tipo
            switch (tipo)
            {
                case TipoTransaccion.Deposito:
                    cuenta.Saldo += monto;
                    break;
                case TipoTransaccion.Retiro:
                    cuenta.Saldo -= monto;
                    break;
                default:
                    throw new ArgumentException("Tipo de transacción inválido.");
            }

            // Crear la transacción
            var transaccion = new Transaccion
            {
                Id = Guid.NewGuid(),
                Tipo = tipo.ToString().ToLower(), 
                Monto = monto,
                SaldoDespues = cuenta.Saldo,
                Fecha = DateTime.UtcNow
            };

            // Guardar la transacción en la cuenta
            cuenta.Transacciones.Add(transaccion);
            return transaccion;
        }


        public List<Transaccion> ConsultarTransacciones(Guid numeroCuenta)
        {
            var cuenta = ConsultarCuenta(numeroCuenta);
            return cuenta?.Transacciones ?? new List<Transaccion>();
        }
    }
        
}
