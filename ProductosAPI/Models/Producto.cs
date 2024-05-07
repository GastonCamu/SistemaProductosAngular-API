namespace ProductosAPI.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public int? Stock {  get; set; }
    }
}
