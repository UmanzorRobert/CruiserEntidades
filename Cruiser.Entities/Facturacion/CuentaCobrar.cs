using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Facturacion
{
    /// <summary>
    /// Cuenta por cobrar generada automáticamente al emitir una factura.
    /// Representa el saldo pendiente de cobro de una factura y gestiona
    /// el seguimiento de morosos, la escalada de recordatorios y la cartera de cobranza.
    ///
    /// Una CuentaCobrar por factura en relación 1-a-1.
    /// Se cierra automáticamente cuando Factura.ImportePendiente == 0.
    ///
    /// El campo EstaEnMorosidad se activa cuando la factura supera N días de vencida
    /// (configurable en ParametroSistema). Activa el flujo de RecordatorioCobranza.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.ImportePendiente).HasPrecision(18, 2);
    ///   builder.HasIndex(x => x.FacturaId).IsUnique();
    ///   builder.HasIndex(x => new { x.ClienteId, x.EstaEnMorosidad });
    ///   builder.HasIndex(x => x.FechaVencimiento);
    ///   builder.HasIndex(x => x.DiasVencida).HasFilter("\"DiasVencida\" > 0");
    ///   builder.HasIndex(x => x.ResponsableCobranzaId).HasFilter("\"ResponsableCobranzaId\" IS NOT NULL");
    /// </remarks>
    public class CuentaCobrar : EntidadBase
    {
        /// <summary>FK hacia la factura que origina esta cuenta por cobrar. Índice único (1-a-1).</summary>
        public Guid FacturaId { get; set; }

        /// <summary>FK hacia el cliente deudor.</summary>
        public Guid ClienteId { get; set; }

        /// <summary>
        /// Importe pendiente de cobro en la moneda base.
        /// Sincronizado con Factura.ImportePendiente. Actualizado con cada PagoFactura.
        /// </summary>
        public decimal ImportePendiente { get; set; }

        /// <summary>Fecha de vencimiento de la factura para el cobro.</summary>
        public DateOnly FechaVencimiento { get; set; }

        /// <summary>
        /// Número de días transcurridos desde la fecha de vencimiento.
        /// Negativo = aún no ha vencido. Positivo = días de mora.
        /// Actualizado diariamente por el job de Hangfire.
        /// </summary>
        public int DiasVencida { get; set; } = 0;

        /// <summary>
        /// Indica si el cliente ha sido marcado como moroso para esta cuenta.
        /// Se activa cuando DiasVencida supera el umbral configurado en ParametroSistema.
        /// </summary>
        public bool EstaEnMorosidad { get; set; } = false;

        /// <summary>Fecha en que se marcó la cuenta en estado de morosidad.</summary>
        public DateTime? FechaDeclaracionMorosidad { get; set; }

        /// <summary>
        /// Descripción de la acción de cobranza más reciente realizada.
        /// Ejemplo: "Enviado recordatorio amistoso", "Llamada telefónica sin respuesta".
        /// </summary>
        public string? AccionCobranza { get; set; }

        /// <summary>Fecha programada para la próxima acción de seguimiento de cobranza.</summary>
        public DateTime? ProximaAccionFecha { get; set; }

        /// <summary>
        /// FK hacia el empleado responsable de gestionar el cobro de esta cuenta.
        /// Asignado automáticamente según las AsignacionClienteEmpleado vigentes.
        /// </summary>
        public Guid? ResponsableCobranzaId { get; set; }

        /// <summary>Notas de la gestión de cobranza de esta cuenta.</summary>
        public string? NotasCobranza { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Factura que origina esta cuenta por cobrar.</summary>
        public virtual Factura? Factura { get; set; }

        /// <summary>Cliente deudor de esta cuenta por cobrar.</summary>
        public virtual Comercial.Cliente? Cliente { get; set; }

        /// <summary>Historial de recordatorios de cobranza enviados para esta cuenta.</summary>
        public virtual ICollection<RecordatorioCobranza> Recordatorios { get; set; }
            = new List<RecordatorioCobranza>();
    }
}
