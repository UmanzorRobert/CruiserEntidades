using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Precios
{
    /// <summary>
    /// Precio especial de un producto específico dentro de una lista de precios.
    /// Define el precio base, el precio final aplicado y el margen de beneficio calculado.
    ///
    /// Al facturar, el sistema busca si existe un DetalleListaPrecio para el producto
    /// en la lista aplicable al cliente. Si existe, usa PrecioFinal; si no, usa Producto.PrecioVenta.
    ///
    /// El PorcentajeDescuento es informativo (calculado automáticamente desde
    /// PrecioBase y PrecioFinal) y no se aplica directamente en la factura.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.PrecioBase).HasPrecision(18, 4);
    ///   builder.Property(x => x.PrecioFinal).HasPrecision(18, 4);
    ///   builder.Property(x => x.MargenBeneficio).HasPrecision(5, 2);
    ///   builder.Property(x => x.PorcentajeDescuento).HasPrecision(5, 2);
    ///   builder.HasIndex(x => new { x.ListaPrecioId, x.ProductoId }).IsUnique();
    ///   builder.HasIndex(x => x.ProductoId);
    /// </remarks>
    public class DetalleListaPrecio : EntidadBase
    {
        /// <summary>FK hacia la lista de precios a la que pertenece este detalle.</summary>
        public Guid ListaPrecioId { get; set; }

        /// <summary>FK hacia el producto al que aplica este precio especial.</summary>
        public Guid ProductoId { get; set; }

        /// <summary>
        /// Precio base de referencia (habitualmente Producto.PrecioVenta o el de la lista padre).
        /// Usado para calcular el descuento informativo y el margen.
        /// </summary>
        public decimal PrecioBase { get; set; }

        /// <summary>
        /// Precio especial final aplicado al facturar con esta lista.
        /// Este es el precio que se usa en cotizaciones y facturas cuando aplica la lista.
        /// </summary>
        public decimal PrecioFinal { get; set; }

        /// <summary>
        /// Porcentaje de descuento respecto al PrecioBase.
        /// Calculado automáticamente: ((PrecioBase - PrecioFinal) / PrecioBase) × 100.
        /// Informativo, no afecta directamente a la facturación.
        /// </summary>
        public decimal PorcentajeDescuento { get; set; } = 0m;

        /// <summary>
        /// Margen de beneficio sobre el costo del producto.
        /// Calculado: ((PrecioFinal - Producto.PrecioCompra) / Producto.PrecioCompra) × 100.
        /// Actualizado automáticamente cuando cambia el precio de compra.
        /// </summary>
        public decimal MargenBeneficio { get; set; } = 0m;

        /// <summary>Cantidad mínima de compra para que aplique este precio especial.</summary>
        public decimal? CantidadMinima { get; set; }

        /// <summary>Notas sobre las condiciones especiales de este precio.</summary>
        public string? Notas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Lista de precios a la que pertenece este detalle.</summary>
        public virtual ListaPrecio? ListaPrecio { get; set; }

        /// <summary>Producto al que aplica este precio especial.</summary>
        public virtual Catalogo.Producto? Producto { get; set; }
    }
}
