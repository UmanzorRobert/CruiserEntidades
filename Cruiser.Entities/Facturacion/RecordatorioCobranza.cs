using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Facturacion
{
    /// <summary>
    /// Registro de un recordatorio de pago enviado a un cliente para una cuenta por cobrar.
    /// Documenta el canal de contacto, el tipo de recordatorio, el resultado y la próxima acción.
    ///
    /// Los recordatorios se pueden generar manualmente desde la pantalla de gestión de cobranza
    /// o automáticamente mediante el job de Hangfire EnviarRecordatoriosVencidos
    /// que ejecuta la lógica de escalada según los días de mora.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.ResultadoGestion).HasMaxLength(500);
    ///   builder.Property(x => x.ProximaAccion).HasMaxLength(500);
    ///   builder.HasIndex(x => new { x.CuentaCobrarId, x.FechaEnvio });
    ///   builder.HasIndex(x => x.TipoRecordatorio);
    /// </remarks>
    public class RecordatorioCobranza : EntidadBase
    {
        /// <summary>FK hacia la cuenta por cobrar a la que aplica este recordatorio.</summary>
        public Guid CuentaCobrarId { get; set; }

        /// <summary>Tipo de recordatorio según el nivel de urgencia y el tono del mensaje.</summary>
        public TipoRecordatorioCobranza TipoRecordatorio { get; set; }

        /// <summary>Canal utilizado para enviar el recordatorio al cliente.</summary>
        public CanalRecordatorio Canal { get; set; }

        /// <summary>Fecha y hora UTC en que se envió o realizó el recordatorio.</summary>
        public DateTime FechaEnvio { get; set; } = DateTime.UtcNow;

        /// <summary>FK hacia el empleado que realizó o supervisó el recordatorio.</summary>
        public Guid RealizadoPorId { get; set; }

        /// <summary>
        /// Resultado o respuesta del cliente a este recordatorio.
        /// Ejemplo: "Cliente promete pagar el 15/02", "Sin respuesta", "Acepta plan de pagos".
        /// </summary>
        public string? ResultadoGestion { get; set; }

        /// <summary>
        /// Descripción de la próxima acción de seguimiento acordada o planificada.
        /// </summary>
        public string? ProximaAccion { get; set; }

        /// <summary>Fecha programada para la próxima acción de seguimiento.</summary>
        public DateTime? ProximaAccionFecha { get; set; }

        /// <summary>
        /// Indica si el recordatorio fue generado automáticamente por Hangfire
        /// o manualmente por el equipo de cobranzas.
        /// </summary>
        public bool EsAutomatico { get; set; } = false;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Cuenta por cobrar a la que aplica este recordatorio.</summary>
        public virtual CuentaCobrar? CuentaCobrar { get; set; }
    }
}
