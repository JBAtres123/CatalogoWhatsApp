using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoWhatsApp.Data;

public class Categoria
{
    [Key]
    public int id_categoria { get; set; }
    public string nombre { get; set; } = string.Empty;
    public string? descripcion { get; set; }
}

[Table("ProductoImagenes")]
public class ProductoImagen
{
    [Key]
    public int id_imagen { get; set; }

    public int id_producto { get; set; } // Esta es la columna en MySQL

    public string imagen_url { get; set; } = string.Empty;
    public bool es_principal { get; set; }

    // Añade esta propiedad de navegación para que EF entienda la relación
    [ForeignKey("id_producto")]
    public Producto? Producto { get; set; }
}
public class Producto
{
    [Key]
    public int id_producto { get; set; }
    public string nombre { get; set; } = string.Empty;
    public string? descripcion { get; set; }
    public decimal precio { get; set; }
    public int stock { get; set; }

    // Mantenemos imagen_url por ahora para no romper nada, 
    // pero la galería vendrá de la lista "Imagenes"
    public string? imagen_url { get; set; }
    public int id_categoria { get; set; }

    [ForeignKey("id_categoria")]
    public Categoria? Categoria { get; set; }

    // RELACIÓN: Un producto tiene muchas imágenes
    public List<ProductoImagen> Imagenes { get; set; } = new();
}

public class Pedido
{
    [Key]
    public int id_pedido { get; set; }
    public DateTime fecha_creacion { get; set; } = DateTime.Now;
    public string nombre_cliente { get; set; } = string.Empty;
    public string apellido_cliente { get; set; } = string.Empty;
    public string telefono_cliente { get; set; } = string.Empty;
    public string direccion { get; set; } = string.Empty;
    public string? instrucciones_envio { get; set; }
    public decimal total_pedido { get; set; }
    public string metodo_pago { get; set; } = "Contra Entrega";
    public int id_estado { get; set; } = 1; // 1 = Pendiente

    // Relación con los detalles
    public List<DetallePedido> Detalles { get; set; } = new();
}

public class DetallePedido
{
    [Key]
    public int id_detalle { get; set; }

    // Esta es la clave foránea
    public int id_pedido { get; set; }

    public int id_producto { get; set; }
    public int cantidad { get; set; }
    public decimal precio_unitario { get; set; }

    // Agrega esta referencia para que EF no "invente" nombres de columnas
    [ForeignKey("id_pedido")]
    public Pedido? Pedido { get; set; }

    [ForeignKey("id_producto")]
    public Producto? Producto { get; set; }
}


[Table("EstadosPedido")]
public class EstadoPedido
{
    [Key]
    public int id_estado { get; set; }
    public string nombre_estado { get; set; } = string.Empty;
}