using Microsoft.EntityFrameworkCore;
using ProductosAPI2._0.Models;

namespace ProductosAPI2._0.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
    }


}
