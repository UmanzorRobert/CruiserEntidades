using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Inventario
{
    /// <summary>
    /// Reserva de una cantidad de stock de un producto para una operación futura confirmada.
    /// Impide que el stock reservado sea asignado a otras operaciones mientras la reserva esté activa.
    ///
    /// El stock reservado se descuenta del stock disponible (HistorialStock.StockDisponible)
    /// pero no del stock físico (HistorialStock.StockActual).
    ///
    /// Las reservas tienen una fecha de expiración configurable. El job de Hangfire
    /// libera automáticamente las reservas expiradas no procesadas.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.CantidadReservada).HasPrecision(18, 4);
    ///   builder.HasIndex(x => new { x.ProductoId, x.AlmacenId, x.EstaActiva });
    ///   builder.HasIndex(x => new { x.TipoOrigen, x.EntidadOrigenId });
    ///   builder.HasIndex(x => x.FechaExpiracion).HasFilter("\"FechaExpiracion\" IS NOT NULL");
    /// </remarks>
    public class ReservaStock : EntidadBase
    {
        /// <summary>FK hacia el producto cuyo stock está siendo reservado.</summary>
        public Guid ProductoId { get; set; }

        /// <summary>FK hacia el almacén del que se reserva el stock.</summary>
        public Guid AlmacenId { get; set; }

        /// <summary>FK hacia el lote específico reservado. Nulo si no se reserva lote concreto.</summary>
        public Guid? LoteProductoId { get; set; }

        /// <summary>
        /// Cantidad de unidades reservadas en la unidad de medida del producto.
        /// </summary>
        public decimal CantidadReservada { get; set; }

        /// <summary>Tipo de operación que originó la reserva.</summary>
        public TipoOrigenReserva TipoOrigen { get; set; }

        /// <summary>
        /// Identificador de la entidad origen que generó la reserva (referencia polimórfica).
        /// Ejemplo: Guid de la AsignacionProducto, OrdenServicio o CotizacionServicio.
        /// </summary>
        public Guid EntidadOrigenId { get; set; }

        /// <summary>Fecha y hora UTC en que se creó la reserva.</summary>
        public DateTime FechaReserva { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Fecha y hora UTC límite de validez de la reserva.
        /// Tras esta fecha, Hangfire libera automáticamente la reserva si no fue procesada.
        /// Nulo para reservas sin expiración (reservas manuales permanentes).
        /// </summary>
        public DateTime? FechaExpiracion { get; set; }

        /// <summary>
        /// Indica si la reserva está actualmente vigente.
        /// False cuando se libera (manual, automática) o cuando se convierte en movimiento real.
        /// </summary>
        public bool EstaActiva { get; set; } = true;

        /// <summary>Motivo o descripción de la liberación de la reserva cuando aplica.</summary>
        public string? MotivoLiberacion { get; set; }

        /// <summary>Fecha y hora UTC en que se liberó la reserva.</summary>
        public DateTime? FechaLiberacion { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Producto cuyo stock está reservado.</summary>
        public virtual Catalogo.Producto? Producto { get; set; }

        /// <summary>Almacén del que proviene el stock reservado.</summary>
        public virtual Almacen? Almacen { get; set; }
    }
}
