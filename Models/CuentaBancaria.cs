namespace PruebaTecnica.Models
{
    public class CuentaBancaria
    {
        public Guid NumeroCuenta { get; set; }
        public decimal Saldo { get; set; }
        public List<Transaccion> Transacciones { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    }
}
