using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Operaciones
{
    /// <summary>
    /// Línea de detalle de una orden de compra que especifica un producto,
    /// la cantidad solicitada, el precio acordado y la cantidad recibida.
    ///
    /// La recepción parcial actualiza CantidadRecibida sin cerrar el pedido,
    /// permitiendo múltiples recepciones hasta alcanzar la CantidadSolicitada.
    ///
    /// Cada recepción genera un MovimientoInventario y actualiza un LoteProducto.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.CantidadSolicitada).HasPrecision(18, 4);
    ///   builder.Property(x => x.CantidadRecibida).HasPrecision(18, 4);
    ///   builder.Property(x => x.PrecioUnitario).HasPrecision(18, 4);
    ///   builder.Property(x => x.PorcentajeDescuento).HasPrecision(5, 2);
    ///   builder.Property(x => x.PorcentajeIVA).HasPrecision(5, 2);
    ///   builder.Property(x => x.ImporteTotal).HasPrecision(18, 2);
    ///   builder.HasIndex(x => new { x.OrdenCompraId, x.NumeroLinea });
    ///   builder.HasIndex(x => x.ProductoId);
    /// </remarks>
    public class DetalleOrdenCompra : EntidadBase
    {
        /// <summary>FK hacia la orden de compra a la que pertenece esta línea.</summary>
        public Guid OrdenCompraId { get; set; }

        /// <summary>FK hacia el producto solicitado en esta línea.</summary>
        public Guid ProductoId { get; set; }

        /// <summary>
        /// Número de línea dentro de la orden para mantener el orden de presentación.
        /// Correlativo comenzando en 1.
        /// </summary>
        public int NumeroLinea { get; set; }

        /// <summary>
        /// Descripción de la línea en la orden. Por defecto: Producto.Nombre.
        /// Puede modificarse para incluir especificaciones adicionales para el proveedor.
        /// </summary>
        public string Descripcion { get; set; } = string.Empty;

        // ── Cantidades ───────────────────────────────────────────────────────

        /// <summary>Cantidad de unidades solicitadas al proveedor en la unidad del producto.</summary>
        public decimal CantidadSolicitada { get; set; }

        /// <summary>
        /// Cantidad total recibida hasta el momento en todas las recepciones.
        /// Actualizada incrementalmente con cada recepción parcial.
        /// CantidadRecibida == CantidadSolicitada → línea completamente recibida.
        /// </summary>
        public decimal CantidadRecibida { get; set; } = 0m;

        /// <summary>
        /// Cantidad pendiente de recibir: CantidadSolicitada - CantidadRecibida.
        /// Campo calculado, actualizado automáticamente.
        /// </summary>
        public decimal CantidadPendiente { get; set; } = 0m;

        // ── Precios e impuestos ──────────────────────────────────────────────

        /// <summary>
        /// Precio de compra unitario acordado con el proveedor para esta línea.
        /// Por defecto: ProveedorProducto.PrecioCompraPactado.
        /// </summary>
        public decimal PrecioUnitario { get; set; }

        /// <summary>Porcentaje de descuento de línea acordado con el proveedor.</summary>
        public decimal PorcentajeDescuento { get; set; } = 0m;

        /// <summary>Importe del descuento de línea: (PrecioUnitario × CantidadSolicitada) × PorcentajeDescuento / 100.</summary>
        public decimal ImporteDescuento { get; set; } = 0m;

        /// <summary>Porcentaje de IVA aplicable a este producto según su categoría fiscal.</summary>
        public decimal PorcentajeIVA { get; set; } = 21m;

        /// <summary>
        /// Importe total de la línea: (PrecioUnitario × CantidadSolicitada) - ImporteDescuento.
        /// Sin IVA. Calculado automáticamente.
        /// </summary>
        public decimal ImporteTotal { get; set; }

        // ── Referencia proveedor ─────────────────────────────────────────────

        /// <summary>
        /// Referencia del producto en el catálogo del proveedor (ProveedorProducto.CodigoProductoProveedor).
        /// Se incluye en el PDF de la orden para facilitar la identificación por el proveedor.
        /// </summary>
        public string? ReferenciaProveedor { get; set; }

        /// <summary>Notas específicas de esta línea para el proveedor.</summary>
        public string? Notas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Orden de compra a la que pertenece esta línea.</summary>
        public virtual OrdenCompra? OrdenCompra { get; set; }

        /// <summary>Producto solicitado en esta línea.</summary>
        public virtual Catalogo.Producto? Producto { get; set; }
    }
}
