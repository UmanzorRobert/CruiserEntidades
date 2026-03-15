using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Facturacion
{
    /// <summary>
    /// Registro de un cobro aplicado a una factura.
    /// Una factura puede tener múltiples pagos parciales hasta completar el total.
    ///
    /// Al registrar un pago, el sistema actualiza automáticamente:
    /// - Factura.ImportePagado += Importe
    /// - Factura.ImportePendiente -= Importe
    /// - Si ImportePendiente == 0 → Factura.Estado = Pagada
    /// - CuentaCobrar.ImportePendiente -= Importe
    ///
    /// La conciliación bancaria (EstaConciliado) se realiza al cruzar los pagos
    /// con el extracto bancario importado o introducido manualmente.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Importe).HasPrecision(18, 2);
    ///   builder.Property(x => x.Comision).HasPrecision(18, 2);
    ///   builder.Property(x => x.NumeroTransaccion).HasMaxLength(100);
    ///   builder.HasIndex(x => new { x.FacturaId, x.FechaPago });
    ///   builder.HasIndex(x => x.EstaConciliado);
    ///   builder.HasIndex(x => x.EstaConfirmado);
    /// </remarks>
    public class PagoFactura : EntidadBase
    {
        /// <summary>FK hacia la factura a la que se aplica este pago.</summary>
        public Guid FacturaId { get; set; }

        /// <summary>FK hacia el método de pago utilizado en este cobro.</summary>
        public Guid MetodoPagoId { get; set; }

        /// <summary>FK hacia el usuario que registró el pago en el sistema.</summary>
        public Guid RegistradoPorId { get; set; }

        /// <summary>Fecha en que se efectuó el pago por parte del cliente.</summary>
        public DateOnly FechaPago { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        /// <summary>
        /// Importe cobrado en este pago en la moneda base del sistema.
        /// Puede ser un pago parcial (menor que el total pendiente de la factura).
        /// </summary>
        public decimal Importe { get; set; }

        /// <summary>
        /// Comisión cobrada por la entidad bancaria o pasarela de pago.
        /// Calculada automáticamente: Importe × MetodoPago.PorcentajeComision / 100.
        /// </summary>
        public decimal Comision { get; set; } = 0m;

        /// <summary>
        /// Número de referencia, ID de transacción o localizador del pago.
        /// Obligatorio para transferencias (referencia bancaria) y tarjetas (ID transacción).
        /// </summary>
        public string? NumeroTransaccion { get; set; }

        /// <summary>
        /// Ruta relativa del comprobante de pago (justificante de transferencia, ticket TPV).
        /// El usuario puede subir el comprobante desde la interfaz para adjuntarlo al pago.
        /// </summary>
        public string? RutaComprobante { get; set; }

        /// <summary>
        /// Indica si el pago ha sido confirmado por el área financiera.
        /// Para transferencias: false hasta verificar el ingreso en el banco.
        /// Para efectivo/tarjeta: true automáticamente al registrar.
        /// </summary>
        public bool EstaConfirmado { get; set; } = false;

        /// <summary>
        /// Indica si el pago ha sido conciliado con el extracto bancario.
        /// La conciliación cruza los pagos del sistema con los movimientos bancarios reales.
        /// </summary>
        public bool EstaConciliado { get; set; } = false;

        /// <summary>Fecha y hora UTC de la conciliación bancaria del pago.</summary>
        public DateTime? FechaConciliacion { get; set; }

        /// <summary>Notas sobre el pago (referencia del cliente, condiciones especiales, etc.).</summary>
        public string? Notas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Factura a la que se aplica este pago.</summary>
        public virtual Factura? Factura { get; set; }

        /// <summary>Método de pago utilizado en este cobro.</summary>
        public virtual MetodoPago? MetodoPago { get; set; }
    }
}
