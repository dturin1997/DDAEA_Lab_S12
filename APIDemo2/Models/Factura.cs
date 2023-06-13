namespace APIDemo2.Models
{
    public class Factura
    {
        public int FacturaID { get; set; }
        public DateTime Date { get; set; }
        public List<DetalleFactura>? DetalleFacturas { get; set; }
        public int ClienteID { get; set; }
        public Cliente? Cliente { get; set; }
    }
}
