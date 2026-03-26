using CatalogoWhatsApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CatalogoWhatsApp.Services;

public class CategoriaService
{
    private readonly AppDbContext _context;

    public CategoriaService(AppDbContext context)
    {
        _context = context;
    }

    // Este es el método que tu página de AdminProductos está buscando
    public async Task<List<Categoria>> GetCategoriasAsync()
    {
        return await _context.Categorias.ToListAsync();
    }

    // Método opcional por si quieres agregar categorías desde código
    public async Task<bool> InsertarCategoriaAsync(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
        var resultado = await _context.SaveChangesAsync();
        return resultado > 0;
    }
}