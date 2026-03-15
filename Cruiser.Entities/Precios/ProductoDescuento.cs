using System;

namespace Cruiser.Entities.Precios
{
    /// <summary>
    /// Relación entre un Descuento y un Producto específico al que aplica.
    /// Permite limitar el descuento a productos concretos del catálogo.
    ///
    /// Si un Descuento no tiene ProductoDescuento asociados, aplica a todos los productos.
    /// Si tiene ProductoDescuento, solo aplica a los productos listados.
    ///
    /// NO hereda de EntidadBase: tabla de asociación simple.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.HasIndex(x => new { x.DescuentoId, x.ProductoId }).IsUnique();
    /// </remarks>
    public class ProductoDescuento
    {
        /// <summary>Identificador único de la relación.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>FK hacia el descuento que aplica al producto.</summary>
        public Guid DescuentoId { get; set; }

        /// <summary>FK hacia el producto al que aplica el descuento.</summary>
        public Guid ProductoId { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Descuento que aplica al producto.</summary>
        public virtual Descuento? Descuento { get; set; }

        /// <summary>Producto al que aplica el descuento.</summary>
        public virtual Catalogo.Producto? Producto { get; set; }
    }
}
