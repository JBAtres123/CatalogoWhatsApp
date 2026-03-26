using Microsoft.EntityFrameworkCore;

namespace CatalogoWhatsApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Producto> Productos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    public DbSet<ProductoImagen> ProductoImagenes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Le decimos que un Producto tiene muchas Imagenes
        modelBuilder.Entity<Producto>()
            .HasMany(p => p.Imagenes)
            .WithOne(i => i.Producto)
            .HasForeignKey(i => i.id_producto); // <--- Aquí le aclaramos el nombre exacto de la columna
    }
    // Agregaremos Pedidos más adelante para no complicarnos ahora
}