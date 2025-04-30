using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Dtos
{
    public class CrearCuentaDto
    {
        [RegularExpression(@"^\d+$", ErrorMessage = "El campo debe contener solo números.")]
        [Range(0, double.MaxValue, ErrorMessage = "El saldo inicial no puede ser negativo.")]
        public decimal SaldoInicial { get; set; }
    }
}
