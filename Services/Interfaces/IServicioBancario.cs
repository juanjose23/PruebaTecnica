using PruebaTecnica.Enums;
using PruebaTecnica.Models;

namespace PruebaTecnica.Services.Interfaces
{
    public interface IServicioBancario
    {
       public CuentaBancaria CrearCuenta( decimal saldoInicial);
        public CuentaBancaria ConsultarCuenta(Guid numeroCuenta);
        public Transaccion RealizarTransaccion(Guid numeroCuenta, TipoTransaccion tipo, decimal monto);
        public List<Transaccion> ConsultarTransacciones(Guid numeroCuenta);

    }
}
