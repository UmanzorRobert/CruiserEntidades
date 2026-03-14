using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Catalogo
{
    /// <summary>
    /// Valor concreto de un atributo asignado a un producto específico.
    /// Relaciona un Producto con un AtributoProducto y su valor específico.
    ///
    /// Ejemplo:
    /// Producto "Lejía desinfectante" → AtributoProducto "Capacidad" → ValorAtributo "5000" (ml)
    /// Producto "Guante nitrilo" → AtributoProducto "Color" → ValorAtributo "Azul" (#0000FF)
    /// Producto "Guante nitrilo" → AtributoProducto "Talla" → ValorAtributo "L"
    ///
    /// Un producto puede tener múltiples atributos con sus respectivos valores.
    /// NO hereda de EntidadBase: es una tabla de relación con campos propios simples.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.Property(x => x.Valor).IsRequired().HasMaxLength(200);
    ///   builder.Property(x => x.CodigoHex).HasMaxLength(7);
    ///   builder.HasIndex(x => new { x.ProductoId, x.AtributoProductoId }).IsUnique();
    ///
    ///   Relaciones:
    ///   builder.HasOne(v => v.Producto).WithMany(p => p.ValoresAtributos)
    ///          .HasForeignKey(v => v.ProductoId).OnDelete(DeleteBehavior.Cascade);
    ///   builder.HasOne(v => v.AtributoProducto).WithMany(a => a.Valores)
    ///          .HasForeignKey(v => v.AtributoProductoId).OnDelete(DeleteBehavior.Restrict);
    /// </remarks>
    public class ValorAtributoProducto
    {
        /// <summary>Identificador único del valor de atributo.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Identificador del producto al que se asigna este valor de atributo.
        /// FK hacia Productos.
        /// </summary>
        public Guid ProductoId { get; set; }

        /// <summary>
        /// Identificador del atributo al que pertenece este valor.
        /// FK hacia AtributosProducto.
        /// </summary>
        public Guid AtributoProductoId { get; set; }

        /// <summary>
        /// Valor concreto del atributo para este producto.
        /// Ejemplos: "L" (talla), "5000" (capacidad en ml), "Azul" (color), "Nitrilo" (material).
        /// </summary>
        public string Valor { get; set; } = string.Empty;

        /// <summary>
        /// Código hexadecimal del color cuando TipoAtributo = Color.
        /// Formato: "#RRGGBB". Ejemplo: "#0000FF" (azul), "#FFFFFF" (blanco).
        /// Nulo para atributos que no son de tipo Color.
        /// </summary>
        public string? CodigoHex { get; set; }

        /// <summary>
        /// Posición de ordenación de este valor en el listado de variantes del producto.
        /// Los valores con menor Orden se muestran primero en la ficha de producto.
        /// </summary>
        public int Orden { get; set; } = 0;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Producto al que pertenece este valor de atributo.</summary>
        public virtual Producto? Producto { get; set; }

        /// <summary>Definición del atributo al que corresponde este valor.</summary>
        public virtual AtributoProducto? AtributoProducto { get; set; }
    }
}
