using CatalogoWhatsApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CatalogoWhatsApp.Services;

public class ProductoService
{
    private readonly AppDbContext _context;

    public ProductoService(AppDbContext context)
    {
        _context = context;
    }

    // Método para obtener todos los productos con sus imágenes y categoría
    public async Task<List<Producto>> GetProductosAsync()
    {
        return await _context.Productos
            .Include(p => p.Categoria)
            .Include(p => p.Imagenes) // <-- Trae la galería de fotos
            .ToListAsync();
    }

    // Método para agregar un producto
    public async Task<bool> InsertarProductoAsync(Producto producto)
    {
        _context.Productos.Add(producto);
        var resultado = await _context.SaveChangesAsync();
        return resultado > 0;
    }

    // Método para eliminar (Gracias al ON DELETE CASCADE de MySQL, borrará las fotos también)
    public async Task<bool> EliminarProductoAsync(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto != null)
        {
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    // NUEVO: Método para insertar una imagen individual a la galería
    public async Task<bool> InsertarImagenProductoAsync(ProductoImagen imagen)
    {
        _context.ProductoImagenes.Add(imagen);
        var resultado = await _context.SaveChangesAsync();
        return resultado > 0;
    }
}