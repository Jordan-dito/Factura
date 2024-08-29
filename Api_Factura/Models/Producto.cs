namespace Api_Factura.Models
{
    public class Producto
    {
        public long IdProducto { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public decimal Precio { get; set; }

        // Nueva propiedad de navegación
        public ICollection<DetalleFactura> Detalles { get; set; }
    }

}
