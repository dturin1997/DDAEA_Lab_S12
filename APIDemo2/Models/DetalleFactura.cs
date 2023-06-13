namespace APIDemo2.Models
{
    public class DetalleFactura
    {
        public int DetalleFacturaID { get; set; }
        public int ProductID { get; set; }
        public Product? Product { get; set; }
        public int FacturaID { get; set; }
        public virtual Factura? Factura { get; set; }
    }
}
