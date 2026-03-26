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