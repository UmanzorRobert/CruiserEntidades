using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Comercial
{
    /// <summary>
    /// Condiciones comerciales específicas pactadas con un proveedor
    /// para el suministro de un producto concreto.
    ///
    /// Permite que el mismo producto tenga condiciones diferentes
    /// (precio, descuento, tiempo de entrega) según el proveedor.
    ///
    /// Al crear una orden de compra, el sistema autocompleta el precio
    /// y descuento desde ProveedorProducto del proveedor seleccionado,
    /// usando como prioridad el proveedor marcado como EsProveedorPreferido.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.PrecioCompraPactado).HasPrecision(18, 4);
    ///   builder.Property(x => x.PorcentajeDescuentoPactado).HasPrecision(5, 2);
    ///   builder.HasIndex(x => new { x.ProveedorId, x.ProductoId }).IsUnique();
    ///   builder.HasIndex(x => new { x.ProductoId, x.EsProveedorPreferido });
    /// </remarks>
    public class ProveedorProducto : EntidadBase
    {
        /// <summary>FK hacia el proveedor que suministra el producto.</summary>
        public Guid ProveedorId { get; set; }

        /// <summary>FK hacia el producto suministrado por el proveedor.</summary>
        public Guid ProductoId { get; set; }

        /// <summary>
        /// Código o referencia del producto en el catálogo interno del proveedor.
        /// Usado al emitir órdenes de compra para que el proveedor identifique el producto.
        /// </summary>
        public string? CodigoProductoProveedor { get; set; }

        /// <summary>Nombre del producto según el catálogo del proveedor. Puede diferir del nombre interno.</summary>
        public string? NombreProductoProveedor { get; set; }

        /// <summary>
        /// Precio de compra unitario pactado con el proveedor en la moneda del proveedor.
        /// Es el precio base antes de aplicar el descuento pactado.
        /// </summary>
        public decimal PrecioCompraPactado { get; set; }

        /// <summary>
        /// Porcentaje de descuento comercial pactado sobre el precio de compra.
        /// Precio efectivo = PrecioCompraPactado × (1 - PorcentajeDescuento / 100).
        /// </summary>
        public decimal PorcentajeDescuentoPactado { get; set; } = 0m;

        /// <summary>
        /// Días de plazo de entrega habitual desde la confirmación del pedido.
        /// Usado en el cálculo de fechas estimadas de recepción en la orden de compra.
        /// </summary>
        public int TiempoEntregaDias { get; set; } = 7;

        /// <summary>
        /// Cantidad mínima de pedido que acepta el proveedor para este producto.
        /// El sistema advierte si la cantidad de la orden de compra es inferior.
        /// </summary>
        public decimal? CantidadMinimaPedido { get; set; }

        /// <summary>
        /// Múltiplo de pedido (lote mínimo de incremento).
        /// Ejemplo: si es 12, solo se puede pedir 12, 24, 36... unidades.
        /// </summary>
        public decimal? MultiploPedido { get; set; }

        /// <summary>
        /// Indica si este proveedor es el preferido para el suministro de este producto.
        /// El sistema selecciona automáticamente el proveedor preferido al crear una orden.
        /// Solo puede haber un proveedor preferido por producto.
        /// </summary>
        public bool EsProveedorPreferido { get; set; } = false;

        /// <summary>Fecha de inicio de validez de las condiciones pactadas.</summary>
        public DateOnly? FechaInicioVigencia { get; set; }

        /// <summary>Fecha de fin de vigencia de las condiciones pactadas. Nulo = sin fecha de vencimiento.</summary>
        public DateOnly? FechaFinVigencia { get; set; }

        /// <summary>Observaciones sobre las condiciones pactadas o restricciones especiales.</summary>
        public string? Observaciones { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Proveedor que suministra el producto.</summary>
        public virtual Proveedor? Proveedor { get; set; }

        /// <summary>Producto suministrado por el proveedor.</summary>
        public virtual Catalogo.Producto? Producto { get; set; }
    }
}
