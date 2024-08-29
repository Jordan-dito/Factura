namespace Api_Factura.Models
{
    public class Factura
    {
        public long IdFactura { get; set; }
        public string Establecimiento { get; set; }
        public string PuntoEmision { get; set; }
        public string NumeroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public long IdCliente { get; set; }

        // Navegación
        public Cliente Cliente { get; set; }
        public ICollection<DetalleFactura> Detalles { get; set; }
    }

}
