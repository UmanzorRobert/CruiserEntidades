using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Operaciones
{
    /// <summary>
    /// Línea de producto incluida en una PlantillaOrdenCompra.
    /// Define el producto, la cantidad habitual y las condiciones de compra
    /// que se copiarán automáticamente al generar una OrdenCompra desde la plantilla.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.CantidadHabitual).HasPrecision(18, 4);
    ///   builder.Property(x => x.PrecioReferencia).HasPrecision(18, 4);
    ///   builder.HasIndex(x => new { x.PlantillaOrdenCompraId, x.ProductoId }).IsUnique();
    ///   builder.HasIndex(x => x.NumeroLinea);
    /// </remarks>
    public class DetallePlantillaOrdenCompra : EntidadBase
    {
        /// <summary>FK hacia la plantilla de orden de compra a la que pertenece esta línea.</summary>
        public Guid PlantillaOrdenCompraId { get; set; }

        /// <summary>FK hacia el producto incluido en la plantilla.</summary>
        public Guid ProductoId { get; set; }

        /// <summary>Número de línea para mantener el orden de presentación. Correlativo desde 1.</summary>
        public int NumeroLinea { get; set; }

        /// <summary>
        /// Cantidad habitual que se pide al proveedor en cada orden generada desde esta plantilla.
        /// El usuario puede modificarla antes de confirmar la orden generada.
        /// </summary>
        public decimal CantidadHabitual { get; set; }

        /// <summary>
        /// Precio de referencia del producto en esta plantilla.
        /// Por defecto: ProveedorProducto.PrecioCompraPactado. Se actualiza al modificar condiciones.
        /// </summary>
        public decimal PrecioReferencia { get; set; }

        /// <summary>Notas específicas de esta línea para incluir en las órdenes generadas.</summary>
        public string? Notas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Plantilla de orden de compra a la que pertenece esta línea.</summary>
        public virtual PlantillaOrdenCompra? PlantillaOrdenCompra { get; set; }

        /// <summary>Producto incluido en esta línea de la plantilla.</summary>
        public virtual Catalogo.Producto? Producto { get; set; }
    }
}
