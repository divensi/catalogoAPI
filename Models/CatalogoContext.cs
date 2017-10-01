using Microsoft.EntityFrameworkCore;

namespace CatalogoAPI.Models
{
    public class CatalogoContext : DbContext
    {
        public CatalogoContext()
        {
        }

        public CatalogoContext(DbContextOptions<CatalogoContext> options)
            : base(options)
        {
        }

        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
    }
}
