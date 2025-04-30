using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PruebaTecnica.Enums;
using PruebaTecnica.Services;

namespace TestServicioBancario
{
    /**
     * Clase de pruebas unitarias para el servicio bancario.
     * Utiliza xUnit como framework de pruebas.
     * Segun la documentacion de xUnit, las pruebas deben ser independientes y no deben depender del orden de ejecucion.
     * Convencion  Arrange(Preparar) - Act (Actuar) - Assert (Afirmar)
     */
    public class ServicioBancarioTests
    {
        private readonly ServicioBancario _servicio;

        public ServicioBancarioTests()
        {
            _servicio = new ServicioBancario();
        }

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
    }
}
