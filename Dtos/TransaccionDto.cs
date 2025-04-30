using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using PruebaTecnica.Enums;
using PruebaTecnica.Validations;

namespace PruebaTecnica.Dtos
{
    public class TransaccionDto
    {
        /// Propiedades
        [Required(ErrorMessage = "El número de cuenta es obligatorio.")]
        public Guid NumeroCuenta { get; set; }
        [EnumValido(ErrorMessage = "El tipo de transacción es inválido.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipoTransaccion Tipo { get; set; }


        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que cero.")]
        public decimal Monto { get; set; }
    }
}
