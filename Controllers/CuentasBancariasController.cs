using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Dtos;

using PruebaTecnica.Services.Interfaces;

namespace PruebaTecnica.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CuentasBancariasController : ControllerBase
    {
        private readonly IServicioBancario _servicioBancario;

        public CuentasBancariasController(IServicioBancario servicioBancario)
        {
            _servicioBancario = servicioBancario;
        }

        [HttpPost("crear")]
        public IActionResult CrearCuenta([FromBody] CrearCuentaDto dto)
        {
            try
            {

                var cuenta = _servicioBancario.CrearCuenta(dto.SaldoInicial);
                return Ok(cuenta);
            }
            catch(InvalidCastException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor.");
            }
        }

        [HttpGet("consultar/{numeroCuenta}")]
        public IActionResult ConsultarCuenta(Guid numeroCuenta)
        {
            var cuenta = _servicioBancario.ConsultarCuenta(numeroCuenta);
            if (cuenta == null)
                return NotFound("Cuenta no encontrada.");
            return Ok(cuenta.Saldo);
        }

        [HttpPost("transaccion/{numeroCuenta}")]
        public IActionResult RealizarTransaccion([FromBody] TransaccionDto dto)
        {
            try
            {
                var transaccion = _servicioBancario.RealizarTransaccion(dto.NumeroCuenta, dto.Tipo, dto.Monto);
                return Ok(transaccion);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor.");
            }
        }

        [HttpGet("transacciones/{numeroCuenta}")]
        public IActionResult ObtenerTransaciones(Guid numeroCuenta) 
        {
            var cuenta = _servicioBancario.ConsultarTransacciones(numeroCuenta);
            if (cuenta == null)
                return NotFound("Cuenta no encontrada.");
            return Ok(cuenta);

        }







    }
}
