using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Operaciones
{
    /// <summary>
    /// Línea de producto de una asignación de productos.
    /// Detalla exactamente qué producto, de qué lote, en qué cantidad
    /// y a qué precio se incluye en la asignación.
    ///
    /// Al despachar la asignación, cada DetalleAsignacion genera un
    /// MovimientoInventario de salida del almacén correspondiente.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Cantidad).HasPrecision(18, 4);
    ///   builder.Property(x => x.PrecioVentaUnitario).HasPrecision(18, 4);
    ///   builder.Property(x => x.CostoUnitario).HasPrecision(18, 4);
    ///   builder.Property(x => x.PorcentajeDescuento).HasPrecision(5, 2);
    ///   builder.Property(x => x.PorcentajeIVA).HasPrecision(5, 2);
    ///   builder.Property(x => x.ImporteTotal).HasPrecision(18, 2);
    ///   builder.HasIndex(x => new { x.AsignacionProductoId, x.NumeroLinea });
    ///   builder.HasIndex(x => x.ProductoId);
    /// </remarks>
    public class DetalleAsignacion : EntidadBase
    {
        /// <summary>FK hacia la asignación de productos a la que pertenece esta línea.</summary>
        public Guid AsignacionProductoId { get; set; }

        /// <summary>FK hacia el producto asignado en esta línea.</summary>
        public Guid ProductoId { get; set; }

        /// <summary>
        /// FK hacia el lote específico del que se toman los productos.
        /// Nulo si el producto no requiere control de lote.
        /// </summary>
        public Guid? LoteProductoId { get; set; }

        /// <summary>Número de línea para mantener el orden. Correlativo desde 1.</summary>
        public int NumeroLinea { get; set; }

        /// <summary>Descripción de la línea. Por defecto: Producto.Nombre.</summary>
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>Cantidad de unidades asignadas en la unidad de medida del producto.</summary>
        public decimal Cantidad { get; set; }

        /// <summary>
        /// Precio de venta unitario aplicado al cliente.
        /// 0 para asignaciones internas y a empleados (no facturable).
        /// </summary>
        public decimal PrecioVentaUnitario { get; set; } = 0m;

        /// <summary>Porcentaje de descuento aplicado en esta línea.</summary>
        public decimal PorcentajeDescuento { get; set; } = 0m;

        /// <summary>Porcentaje de IVA aplicable a esta línea.</summary>
        public decimal PorcentajeIVA { get; set; } = 21m;

        /// <summary>Importe neto de la línea: (PrecioVentaUnitario × Cantidad) × (1 - Descuento).</summary>
        public decimal ImporteTotal { get; set; } = 0m;

        /// <summary>
        /// Costo unitario del producto al momento del despacho (costo promedio ponderado).
        /// Usado para calcular el margen real de la asignación.
        /// </summary>
        public decimal CostoUnitario { get; set; } = 0m;

        /// <summary>
        /// Código de cuenta contable para la contabilización de esta línea.
        /// Tomado por defecto de la categoría del producto.
        /// </summary>
        public string? CodigoCuentaContable { get; set; }

        /// <summary>Notas o especificaciones adicionales de esta línea.</summary>
        public string? Notas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Asignación de productos a la que pertenece esta línea.</summary>
        public virtual AsignacionProducto? AsignacionProducto { get; set; }

        /// <summary>Producto asignado en esta línea.</summary>
        public virtual Catalogo.Producto? Producto { get; set; }
    }
}
