namespace Api_Factura.Models
{
    public class DetalleFactura
    {
        public long IdDetalle { get; set; }
        public long IdFactura { get; set; }
        public long IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public string UnidadMedida { get; set; }
        public decimal Precio { get; set; }
        public decimal IVA { get; set; }
        public decimal Subtotal { get; set; }

        // Propiedades de navegación
        public Factura Factura { get; set; }
        public Producto Producto { get; set; }
    }

}
