using System;

namespace Cruiser.Entities.Inventario
{
    /// <summary>
    /// Detalle del impacto de un MovimientoInventario sobre un lote específico.
    /// Cuando un movimiento afecta a múltiples lotes (FIFO, FEFO),
    /// se genera un MovimientoLote por cada lote afectado.
    ///
    /// Permite la trazabilidad completa: saber exactamente de qué lote
    /// proviene cada unidad de producto consumida o vendida.
    ///
    /// NO hereda de EntidadBase: tabla de detalle append-only.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.Property(x => x.CantidadAfectada).HasPrecision(18, 4);
    ///   builder.Property(x => x.StockResultanteLote).HasPrecision(18, 4);
    ///   builder.HasIndex(x => x.LoteProductoId);
    ///   builder.HasIndex(x => x.MovimientoInventarioId);
    /// </remarks>
    public class MovimientoLote
    {
        /// <summary>Identificador único del detalle de lote afectado.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>FK hacia el lote de producto afectado por este detalle.</summary>
        public Guid LoteProductoId { get; set; }

        /// <summary>FK hacia el movimiento de inventario que origina este detalle.</summary>
        public Guid MovimientoInventarioId { get; set; }

        /// <summary>
        /// Cantidad de unidades del lote afectadas por este movimiento.
        /// Siempre positiva. La dirección (entrada/salida) la define el TipoMovimiento padre.
        /// </summary>
        public decimal CantidadAfectada { get; set; }

        /// <summary>
        /// Stock resultante del lote DESPUÉS de aplicar este movimiento.
        /// Permite verificar la consistencia del stock del lote en cualquier momento.
        /// </summary>
        public decimal StockResultanteLote { get; set; }

        /// <summary>
        /// Costo unitario del lote en el momento del movimiento.
        /// Para salidas: costo de adquisición del lote específico.
        /// </summary>
        public decimal? CostoUnitarioLote { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Lote de producto afectado por este movimiento.</summary>
        public virtual Catalogo.LoteProducto? LoteProducto { get; set; }

        /// <summary>Movimiento de inventario que originó este detalle de lote.</summary>
        public virtual MovimientoInventario? MovimientoInventario { get; set; }
    }
}
