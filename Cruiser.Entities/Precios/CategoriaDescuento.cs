using System;

namespace Cruiser.Entities.Precios
{
    /// <summary>
    /// Relación entre un Descuento y una Categoría de productos a la que aplica.
    /// El descuento aplica automáticamente a todos los productos de la categoría
    /// (y sus subcategorías en el árbol jerárquico) cuando se encuentra esta relación.
    ///
    /// Si un Descuento no tiene CategoriaDescuento asociadas, aplica a todas las categorías.
    ///
    /// NO hereda de EntidadBase: tabla de asociación simple.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.HasIndex(x => new { x.DescuentoId, x.CategoriaId }).IsUnique();
    /// </remarks>
    public class CategoriaDescuento
    {
        /// <summary>Identificador único de la relación.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>FK hacia el descuento que aplica a la categoría.</summary>
        public Guid DescuentoId { get; set; }

        /// <summary>FK hacia la categoría de productos a la que aplica el descuento.</summary>
        public Guid CategoriaId { get; set; }

        /// <summary>
        /// Indica si el descuento aplica recursivamente a todas las subcategorías hijas.
        /// True = aplica a la categoría y a toda su descendencia en el árbol.
        /// False = solo aplica a los productos directos de esta categoría.
        /// </summary>
        public bool AplicaASubcategorias { get; set; } = true;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Descuento que aplica a la categoría.</summary>
        public virtual Descuento? Descuento { get; set; }

        /// <summary>Categoría de productos a la que aplica el descuento.</summary>
        public virtual Catalogo.Categoria? Categoria { get; set; }
    }
}
