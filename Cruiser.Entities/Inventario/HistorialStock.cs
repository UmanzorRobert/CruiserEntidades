using System;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Inventario
{
    /// <summary>
    /// Snapshot del estado del stock de un producto en un almacén en un momento específico.
    /// Funciona como un libro mayor de stock: registra el estado completo del stock
    /// después de cada movimiento relevante, permitiendo consultas eficientes sin
    /// recalcular desde todos los movimientos históricos.
    ///
    /// Proporciona el stock actual, reservado, disponible real, costo promedio ponderado
    /// y el valor económico total del inventario para cada producto×almacén.
    ///
    /// También se genera un snapshot diario de cierre mediante Hangfire para
    /// facilitar los reportes históricos de valoración de inventario.
    ///
    /// NO hereda de EntidadBase: registro append-only de snapshots de stock.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.Property(x => x.StockActual).HasPrecision(18, 4);
    ///   builder.Property(x => x.StockReservado).HasPrecision(18, 4);
    ///   builder.Property(x => x.StockDisponible).HasPrecision(18, 4);
    ///   builder.Property(x => x.CostoUnitarioPromedio).HasPrecision(18, 4);
    ///   builder.Property(x => x.ValorTotalStock).HasPrecision(18, 2);
    ///   builder.HasIndex(x => new { x.ProductoId, x.AlmacenId, x.FechaRegistro });
    ///   builder.HasIndex(x => new { x.ProductoId, x.AlmacenId, x.TipoRegistro });
    ///   builder.HasIndex(x => x.FechaRegistro);
    /// </remarks>
    public class HistorialStock
    {
        /// <summary>Identificador único del snapshot de stock.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>FK hacia el producto al que corresponde este snapshot.</summary>
        public Guid ProductoId { get; set; }

        /// <summary>FK hacia el almacén al que corresponde este snapshot.</summary>
        public Guid AlmacenId { get; set; }

        /// <summary>FK hacia el MovimientoInventario que generó este snapshot. Nulo para cierres diarios.</summary>
        public Guid? MovimientoInventarioId { get; set; }

        /// <summary>
        /// Stock físico total del producto en el almacén (incluye reservado).
        /// StockActual = StockDisponible + StockReservado.
        /// </summary>
        public decimal StockActual { get; set; }

        /// <summary>
        /// Stock reservado para pedidos, asignaciones u órdenes confirmadas pero no despachadas.
        /// No disponible para nuevas operaciones hasta que se libere o complete la reserva.
        /// </summary>
        public decimal StockReservado { get; set; }

        /// <summary>
        /// Stock disponible para nuevas operaciones: StockActual - StockReservado.
        /// Este es el valor que se compara contra StockMinimo para generar alertas.
        /// </summary>
        public decimal StockDisponible { get; set; }

        /// <summary>
        /// Costo unitario promedio ponderado del producto en este almacén.
        /// Recalculado con cada entrada de compra usando la fórmula CPP.
        /// Se usa para valorar el inventario y calcular el margen real de cada servicio.
        /// </summary>
        public decimal CostoUnitarioPromedio { get; set; }

        /// <summary>
        /// Valor económico total del stock en el almacén: StockActual × CostoUnitarioPromedio.
        /// Usado en los reportes de valoración de inventario y análisis de capital inmovilizado.
        /// </summary>
        public decimal ValorTotalStock { get; set; }

        /// <summary>Tipo de evento que originó este registro de historial de stock.</summary>
        public TipoRegistroStock TipoRegistro { get; set; }

        /// <summary>Fecha y hora UTC en que se registró este snapshot de stock.</summary>
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Producto al que corresponde este historial de stock.</summary>
        public virtual Catalogo.Producto? Producto { get; set; }

        /// <summary>Almacén al que corresponde este historial de stock.</summary>
        public virtual Almacen? Almacen { get; set; }
    }
}
