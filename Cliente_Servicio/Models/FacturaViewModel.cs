namespace Cliente_Servicio.Models
{
    public class FacturaViewModel
    {


        public long IdFactura { get; set; }
        public string Establecimiento { get; set; }
        public string PuntoEmision { get; set; }
        public string NumeroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public long IdCliente { get; set; }


    }

}

