using System.ComponentModel.DataAnnotations;

namespace ProductosAPI2._0.Models
{
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        [Required]
        public string? Descripcion { get; set; }
        [Required]
        public decimal? Precio { get; set; }
        [Required]
        public int? Stock { get; set; }
    }
}
