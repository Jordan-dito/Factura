namespace Api_Factura.Models
{
    public class Cliente
    {
        public long IdCliente { get; set; }  // Esta es la clave primaria
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }

      
    public ICollection<Factura> Facturas { get; set; }
    }

}
