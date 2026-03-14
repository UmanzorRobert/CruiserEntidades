using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Catalogo
{
    /// <summary>
    /// Lote de un producto recibido en una recepción de orden de compra.
    /// Permite la trazabilidad completa desde la recepción hasta el consumo final,
    /// incluyendo gestión de caducidades, cuarentenas y bloqueos de calidad.
    ///
    /// Cada MovimientoInventario de tipo Entrada genera o incrementa un LoteProducto.
    /// Cada MovimientoInventario de tipo Salida decrementa el CantidadActual del lote.
    ///
    /// Los lotes con EsPerecedero=true generan alertas automáticas mediante
    /// Hangfire cuando se acerca su fecha de caducidad (configurado en ConfiguracionAlerta).
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.NumeroLote).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.CantidadInicial).HasPrecision(18, 4);
    ///   builder.Property(x => x.CantidadActual).HasPrecision(18, 4);
    ///   builder.Property(x => x.CostoPorUnidad).HasPrecision(18, 4);
    ///   builder.HasIndex(x => new { x.ProductoId, x.NumeroLote }).IsUnique();
    ///   builder.HasIndex(x => x.FechaCaducidad).HasFilter("\"FechaCaducidad\" IS NOT NULL");
    ///   builder.HasIndex(x => x.Estado);
    /// </remarks>
    public class LoteProducto : EntidadBase
    {
        /// <summary>
        /// Identificador del producto al que pertenece este lote.
        /// FK hacia Productos.
        /// </summary>
        public Guid ProductoId { get; set; }

        /// <summary>
        /// Número de lote asignado por el fabricante o generado internamente.
        /// Debe ser único por producto. Ejemplo: "LOT-2026-001", "LH2025123456".
        /// </summary>
        public string NumeroLote { get; set; } = string.Empty;

        /// <summary>
        /// Número de serie del lote si el producto requiere trazabilidad individual.
        /// Solo aplica cuando Producto.RequiereNumeroSerie = true.
        /// </summary>
        public string? NumeroSerie { get; set; }

        // ── Fechas del lote ──────────────────────────────────────────────────

        /// <summary>
        /// Fecha de fabricación o producción del lote por parte del fabricante.
        /// Nulo si no se conoce o no aplica al tipo de producto.
        /// </summary>
        public DateOnly? FechaFabricacion { get; set; }

        /// <summary>
        /// Fecha de recepción física del lote en el almacén del sistema.
        /// Se registra automáticamente al completar la recepción de la orden de compra.
        /// </summary>
        public DateOnly FechaRecepcion { get; set; }

        /// <summary>
        /// Fecha de caducidad o vencimiento del lote.
        /// Obligatoria cuando Producto.EsPerecedero = true.
        /// Genera alertas mediante Hangfire según los umbrales de ConfiguracionAlerta.
        /// </summary>
        public DateOnly? FechaCaducidad { get; set; }

        /// <summary>
        /// Días de vida útil restante desde hoy hasta la FechaCaducidad.
        /// Campo calculado que se actualiza diariamente mediante job de Hangfire.
        /// Negativo si el lote está caducado.
        /// </summary>
        public int? DiasParaCaducar { get; set; }

        // ── Cantidades ───────────────────────────────────────────────────────

        /// <summary>
        /// Cantidad total recibida en este lote expresada en la unidad de medida del producto.
        /// Este valor no varía tras la recepción; es el punto de referencia histórico.
        /// </summary>
        public decimal CantidadInicial { get; set; }

        /// <summary>
        /// Cantidad disponible actualmente en este lote tras descontar salidas y reservas.
        /// Se actualiza automáticamente con cada MovimientoInventario que afecta al lote.
        /// </summary>
        public decimal CantidadActual { get; set; }

        /// <summary>
        /// Costo unitario de compra de este lote en la moneda base del sistema.
        /// Se toma del precio acordado en la DetalleOrdenCompra correspondiente.
        /// Se usa para el cálculo del costo promedio ponderado del inventario.
        /// </summary>
        public decimal CostoPorUnidad { get; set; }

        // ── Estado y control de calidad ──────────────────────────────────────

        /// <summary>
        /// Estado de calidad y disponibilidad del lote.
        /// Solo los lotes en estado Aprobado pueden ser asignados o vendidos.
        /// </summary>
        public EstadoLote Estado { get; set; } = EstadoLote.Aprobado;

        /// <summary>
        /// Motivo del bloqueo o cuarentena del lote si Estado != Aprobado.
        /// Ejemplo: "Sospecha de contaminación", "Pendiente análisis de calidad".
        /// </summary>
        public string? MotivoEstado { get; set; }

        /// <summary>
        /// Identificador del almacén donde se recibió originalmente el lote.
        /// FK hacia Almacen.
        /// </summary>
        public Guid? AlmacenRecepcionId { get; set; }

        /// <summary>
        /// Identificador de la orden de compra de la que proviene este lote.
        /// FK hacia OrdenCompra. Permite la trazabilidad origen-destino completa.
        /// </summary>
        public Guid? OrdenCompraId { get; set; }

        /// <summary>
        /// Notas internas sobre el lote (observaciones de recepción, incidencias, etc.).
        /// </summary>
        public string? Observaciones { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Producto al que pertenece este lote.</summary>
        public virtual Producto? Producto { get; set; }
    }
}
