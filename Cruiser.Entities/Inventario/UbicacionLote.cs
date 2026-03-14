using System;

namespace Cruiser.Entities.Inventario
{
    /// <summary>
    /// Posición actual de un lote de producto en una ubicación específica del almacén.
    /// Permite saber exactamente en qué estantería/pasillo/nivel está cada lote.
    ///
    /// Un mismo lote puede estar distribuido en múltiples ubicaciones
    /// (si no cabe en una sola). La suma de CantidadEnUbicacion de todos los registros
    /// de un lote debe ser igual a LoteProducto.CantidadActual.
    ///
    /// Se actualiza automáticamente con cada MovimientoInventario que incluye ubicación.
    ///
    /// NO hereda de EntidadBase: registro de estado sin ciclo de vida propio.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.Property(x => x.CantidadEnUbicacion).HasPrecision(18, 4);
    ///   builder.HasIndex(x => new { x.LoteProductoId, x.UbicacionId }).IsUnique();
    ///   builder.HasIndex(x => x.UbicacionId);
    /// </remarks>
    public class UbicacionLote
    {
        /// <summary>Identificador único del registro de posición de lote.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>FK hacia el lote de producto posicionado en esta ubicación.</summary>
        public Guid LoteProductoId { get; set; }

        /// <summary>FK hacia la ubicación física donde está almacenado el lote.</summary>
        public Guid UbicacionId { get; set; }

        /// <summary>
        /// Cantidad de unidades del lote almacenadas en esta ubicación específica.
        /// Actualizada automáticamente con cada movimiento que afecta a lote + ubicación.
        /// </summary>
        public decimal CantidadEnUbicacion { get; set; }

        /// <summary>
        /// Fecha y hora UTC de la última actualización de la cantidad en esta ubicación.
        /// </summary>
        public DateTime FechaUltimaActualizacion { get; set; } = DateTime.UtcNow;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Lote de producto almacenado en esta ubicación.</summary>
        public virtual Catalogo.LoteProducto? LoteProducto { get; set; }

        /// <summary>Ubicación física donde está almacenado el lote.</summary>
        public virtual Ubicacion? Ubicacion { get; set; }
    }
}
