using CatalogoWhatsApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CatalogoWhatsApp.Services;

public class PedidoService
{
    private readonly AppDbContext _context;

    public PedidoService(AppDbContext context)
    {
        _context = context;
    }

    // Obtener todos los pedidos para el panel de administración
    public async Task<List<Pedido>> GetPedidosAsync()
    {
        return await _context.Pedidos
            .Include(p => p.Detalles)
            .ThenInclude(d => d.Producto)
            .OrderByDescending(p => p.fecha_creacion)
            .ToListAsync();
    }

    // Actualizar el estado del pedido (Ej: de Pendiente a Enviado a WhatsApp)
    public async Task ActualizarEstadoPedidoAsync(int idPedido, int nuevoEstado)
    {
        var pedido = await _context.Pedidos.FindAsync(idPedido);
        if (pedido != null)
        {
            pedido.id_estado = nuevoEstado;
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> GuardarPedidoAsync(Pedido pedido)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            pedido.id_estado = 1; // 1 = Pendiente por defecto al crear
            pedido.fecha_creacion = DateTime.Now;

            foreach (var detalle in pedido.Detalles)
            {
                var productoDB = await _context.Productos.FindAsync(detalle.id_producto);

                if (productoDB != null)
                {
                    if (productoDB.stock < detalle.cantidad)
                    {
                        throw new Exception($"Stock insuficiente para: {productoDB.nombre}");
                    }

                    // Restamos la cantidad del stock físicamente en la DB
                    productoDB.stock -= detalle.cantidad;
                    _context.Productos.Update(productoDB);
                }

                // Limpiamos la referencia para evitar inserciones duplicadas de productos
                detalle.Producto = null;
            }

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return pedido.id_pedido;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception(ex.Message);
        }
    }
}