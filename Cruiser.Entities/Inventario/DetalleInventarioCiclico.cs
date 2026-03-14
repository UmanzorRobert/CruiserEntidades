using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Inventario
{
    /// <summary>
    /// Línea de detalle de un proceso de inventario cíclico que registra
    /// el stock del sistema vs. el stock contado físicamente por el empleado,
    /// la diferencia encontrada y la justificación si aplica.
    ///
    /// Se crea un registro por cada producto×ubicación×lote contado.
    /// Al completar el inventario, las diferencias no justificadas generan
    /// automáticamente MovimientosInventario de ajuste.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.StockSistema).HasPrecision(18, 4);
    ///   builder.Property(x => x.StockContado).HasPrecision(18, 4);
    ///   builder.Property(x => x.Diferencia).HasPrecision(18, 4);
    ///   builder.HasIndex(x => new { x.InventarioCiclicoId, x.ProductoId, x.UbicacionId });
    /// </remarks>
    public class DetalleInventarioCiclico : EntidadBase
    {
        /// <summary>FK hacia el inventario cíclico al que pertenece este detalle.</summary>
        public Guid InventarioCiclicoId { get; set; }

        /// <summary>FK hacia el producto contado en esta línea.</summary>
        public Guid ProductoId { get; set; }

        /// <summary>FK hacia la ubicación donde se realizó el conteo. Nulo si sin gestión de ubicaciones.</summary>
        public Guid? UbicacionId { get; set; }

        /// <summary>FK hacia el lote específico contado. Nulo si se cuenta por producto total.</summary>
        public Guid? LoteProductoId { get; set; }

        /// <summary>
        /// Cantidad que el sistema registraba antes del conteo físico.
        /// Capturada automáticamente al crear la línea del inventario.
        /// </summary>
        public decimal StockSistema { get; set; }

        /// <summary>
        /// Cantidad contada físicamente por el empleado durante el inventario.
        /// Se introduce manualmente o mediante escáner desde la PWA.
        /// </summary>
        public decimal StockContado { get; set; }

        /// <summary>
        /// Diferencia entre el stock contado y el stock del sistema.
        /// Calculado automáticamente: Diferencia = StockContado - StockSistema.
        /// Positivo = exceso | Negativo = faltante.
        /// </summary>
        public decimal Diferencia { get; set; }

        /// <summary>
        /// Justificación de la diferencia encontrada.
        /// Requerida por el supervisor antes de generar el ajuste si |Diferencia| > umbral.
        /// Ejemplo: "Merma por caducidad", "Rotura durante manipulación", "Error conteo anterior".
        /// </summary>
        public string? JustificacionDiferencia { get; set; }

        /// <summary>
        /// Indica si la diferencia ha sido ajustada (se generó el MovimientoInventario de ajuste).
        /// </summary>
        public bool EstaAjustado { get; set; } = false;

        /// <summary>
        /// FK hacia el MovimientoInventario de ajuste generado para esta diferencia.
        /// </summary>
        public Guid? MovimientoAjusteId { get; set; }

        /// <summary>
        /// Identificador del empleado que realizó el conteo físico de esta línea.
        /// Permite rastrear quién contó cada producto en el inventario.
        /// </summary>
        public Guid? ContadoPorId { get; set; }

        /// <summary>Fecha y hora UTC en que se realizó el conteo físico de esta línea.</summary>
        public DateTime? FechaConteo { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Inventario cíclico al que pertenece esta línea de detalle.</summary>
        public virtual InventarioCiclico? InventarioCiclico { get; set; }

        /// <summary>Producto contado en esta línea.</summary>
        public virtual Catalogo.Producto? Producto { get; set; }
    }
}
