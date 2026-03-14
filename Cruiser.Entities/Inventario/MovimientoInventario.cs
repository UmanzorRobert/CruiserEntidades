using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Inventario
{
    /// <summary>
    /// Registro de cada movimiento de stock en el sistema de inventario.
    /// Toda variación de stock (entrada, salida, ajuste, transferencia)
    /// genera un MovimientoInventario que queda registrado permanentemente
    /// para garantizar la trazabilidad completa del inventario.
    ///
    /// El stock actual de un producto en un almacén se puede recalcular
    /// en cualquier momento sumando todos sus movimientos (principio de libro diario).
    /// En la práctica, el stock actual se lee de HistorialStock (snapshot actualizado).
    ///
    /// Los movimientos completados son inmutables. Para corregir un error
    /// se genera un movimiento de reversa vinculado mediante ReversadoPorMovimientoId.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Cantidad).HasPrecision(18, 4);
    ///   builder.Property(x => x.StockAnterior).HasPrecision(18, 4);
    ///   builder.Property(x => x.StockPosterior).HasPrecision(18, 4);
    ///   builder.Property(x => x.CostoUnitario).HasPrecision(18, 4);
    ///   builder.Property(x => x.NumeroDocumento).HasMaxLength(100);
    ///   builder.HasIndex(x => new { x.ProductoId, x.AlmacenId, x.FechaMovimiento });
    ///   builder.HasIndex(x => x.Estado);
    ///   builder.HasIndex(x => x.TipoMovimientoId);
    ///   builder.HasIndex(x => x.FechaMovimiento);
    /// </remarks>
    public class MovimientoInventario : EntidadBase
    {
        /// <summary>FK hacia el tipo de movimiento que define el comportamiento de esta operación.</summary>
        public Guid TipoMovimientoId { get; set; }

        /// <summary>FK hacia el producto cuyo stock se modifica.</summary>
        public Guid ProductoId { get; set; }

        /// <summary>FK hacia el almacén origen o principal del movimiento.</summary>
        public Guid AlmacenId { get; set; }

        /// <summary>
        /// FK hacia el almacén destino en movimientos de transferencia.
        /// Nulo para movimientos de entrada/salida simples.
        /// </summary>
        public Guid? AlmacenDestinoId { get; set; }

        /// <summary>
        /// FK hacia la ubicación específica dentro del almacén origen.
        /// Nulo si el almacén no gestiona ubicaciones.
        /// </summary>
        public Guid? UbicacionId { get; set; }

        /// <summary>FK hacia el lote específico afectado por este movimiento.</summary>
        public Guid? LoteProductoId { get; set; }

        // ── Cantidades y costos ──────────────────────────────────────────────

        /// <summary>
        /// Cantidad de unidades del movimiento expresada en la unidad de medida del producto.
        /// Siempre positiva: la naturaleza (entrada/salida) la define TipoMovimiento.Naturaleza.
        /// </summary>
        public decimal Cantidad { get; set; }

        /// <summary>
        /// Stock del producto en el almacén ANTES de aplicar este movimiento.
        /// Capturado automáticamente al crear el movimiento. Para auditoría.
        /// </summary>
        public decimal StockAnterior { get; set; }

        /// <summary>
        /// Stock del producto en el almacén DESPUÉS de aplicar este movimiento.
        /// Calculado al completar el movimiento. Debe coincidir con HistorialStock.
        /// </summary>
        public decimal StockPosterior { get; set; }

        /// <summary>
        /// Costo unitario del producto en el momento del movimiento.
        /// Para entradas: precio de compra del proveedor.
        /// Para salidas: costo promedio ponderado vigente.
        /// </summary>
        public decimal? CostoUnitario { get; set; }

        /// <summary>
        /// Costo total del movimiento (Cantidad × CostoUnitario).
        /// Calculado automáticamente. Usado en la valoración del inventario.
        /// </summary>
        public decimal? CostoTotal { get; set; }

        // ── Trazabilidad del documento origen ───────────────────────────────

        /// <summary>
        /// Número de documento que origina el movimiento.
        /// Ejemplo: número de orden de compra, número de albarán, número de asignación.
        /// </summary>
        public string? NumeroDocumento { get; set; }

        /// <summary>
        /// Tipo de entidad que origina el movimiento (referencia polimórfica).
        /// Ejemplos: "OrdenCompra", "AsignacionProducto", "InventarioCiclico".
        /// </summary>
        public string? TipoEntidadOrigen { get; set; }

        /// <summary>
        /// Identificador de la entidad origen que generó este movimiento.
        /// Permite navegar desde el movimiento al documento origen.
        /// </summary>
        public Guid? EntidadOrigenId { get; set; }

        // ── Control y autorización ───────────────────────────────────────────

        /// <summary>Estado actual del movimiento en su ciclo de vida.</summary>
        public EstadoMovimiento Estado { get; set; } = EstadoMovimiento.Pendiente;

        /// <summary>
        /// Fecha y hora UTC en que se registró el movimiento en el sistema.
        /// Puede diferir de FechaCreacion si el movimiento fue registrado con retroactividad.
        /// </summary>
        public DateTime FechaMovimiento { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Identificador del usuario que autorizó el movimiento (si RequiereAutorizacion=true).
        /// </summary>
        public Guid? AutorizadoPorId { get; set; }

        /// <summary>Fecha y hora UTC en que se autorizó el movimiento.</summary>
        public DateTime? FechaAutorizacion { get; set; }

        /// <summary>Motivo o descripción del movimiento para trazabilidad y auditoría.</summary>
        public string? Motivo { get; set; }

        /// <summary>
        /// Identificador del movimiento de reversa que anuló este movimiento.
        /// Nulo si el movimiento no ha sido reversado.
        /// </summary>
        public Guid? ReversadoPorMovimientoId { get; set; }

        /// <summary>Motivo de la reversión cuando aplica.</summary>
        public string? MotivoReversion { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Tipo de movimiento que define el comportamiento de esta operación.</summary>
        public virtual TipoMovimiento? TipoMovimiento { get; set; }

        /// <summary>Movimientos de lote asociados a este movimiento principal.</summary>
        public virtual ICollection<MovimientoLote> MovimientosLote { get; set; }
            = new List<MovimientoLote>();
    }
}
