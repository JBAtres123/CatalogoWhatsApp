using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CatalogoWhatsApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Producto> Productos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    public DbSet<ProductoImagen> ProductoImagenes { get; set; }

    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<DetallePedido> DetallePedidos { get; set; }

    public DbSet<EstadoPedido> EstadosPedido { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Le decimos que un Producto tiene muchas Imagenes
        modelBuilder.Entity<Producto>()
            .HasMany(p => p.Imagenes)
            .WithOne(i => i.Producto)
            .HasForeignKey(i => i.id_producto);
             modelBuilder.Entity<DetallePedido>()
            .HasOne(d => d.Pedido)
            .WithMany(p => p.Detalles)
            .HasForeignKey(d => d.id_pedido);// <--- Aquí le aclaramos el nombre exacto de la columna
    }

    
    

}