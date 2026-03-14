using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Comercial
{
    /// <summary>
    /// Registro CRM de una interacción comercial entre un empleado y un cliente.
    /// Permite llevar un historial completo de llamadas, emails, reuniones y visitas
    /// para dar seguimiento efectivo a la relación comercial con cada cliente.
    ///
    /// Las interacciones con ResultadoInteraccion = VentaCerrada se usan como
    /// entrada para el análisis de conversión en campañas comerciales.
    ///
    /// ProximaAccionFecha permite programar recordatorios automáticos mediante
    /// Hangfire cuando hay un seguimiento pendiente con una fecha acordada.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Resumen).IsRequired().HasMaxLength(500);
    ///   builder.Property(x => x.Detalle).HasMaxLength(4000);
    ///   builder.Property(x => x.ProximaAccion).HasMaxLength(500);
    ///   builder.HasIndex(x => new { x.ClienteId, x.FechaInteraccion });
    ///   builder.HasIndex(x => new { x.EmpleadoId, x.FechaInteraccion });
    ///   builder.HasIndex(x => x.ProximaAccionFecha).HasFilter("\"ProximaAccionFecha\" IS NOT NULL");
    /// </remarks>
    public class InteraccionComercial : EntidadBase
    {
        /// <summary>FK hacia el cliente con quien se tuvo la interacción.</summary>
        public Guid ClienteId { get; set; }

        /// <summary>FK hacia el empleado que registra y realiza la interacción.</summary>
        public Guid EmpleadoId { get; set; }

        /// <summary>Canal o modalidad de la interacción comercial.</summary>
        public TipoInteraccionComercial TipoInteraccion { get; set; }

        /// <summary>Fecha y hora UTC en que tuvo lugar la interacción.</summary>
        public DateTime FechaInteraccion { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Duración de la interacción en minutos.
        /// Relevante para llamadas, reuniones y visitas.
        /// Nulo para emails y mensajes donde no aplica duración.
        /// </summary>
        public int? DuracionMinutos { get; set; }

        /// <summary>
        /// Resumen ejecutivo de la interacción (máximo 500 caracteres).
        /// Se muestra en el listado del historial de interacciones del cliente.
        /// </summary>
        public string Resumen { get; set; } = string.Empty;

        /// <summary>
        /// Descripción detallada de la interacción: temas tratados, acuerdos alcanzados,
        /// objeciones del cliente y puntos pendientes.
        /// </summary>
        public string? Detalle { get; set; }

        /// <summary>Resultado o desenlace de la interacción para análisis de efectividad.</summary>
        public ResultadoInteraccion Resultado { get; set; } = ResultadoInteraccion.Neutral;

        /// <summary>
        /// Descripción de la próxima acción acordada con el cliente o planificada internamente.
        /// Ejemplo: "Enviar propuesta revisada", "Llamar para confirmar fecha de visita".
        /// </summary>
        public string? ProximaAccion { get; set; }

        /// <summary>
        /// Fecha y hora UTC programada para la próxima acción de seguimiento.
        /// Genera un EventoCalendario automático si se especifica.
        /// </summary>
        public DateTime? ProximaAccionFecha { get; set; }

        /// <summary>
        /// FK hacia la cotización de servicio generada como resultado de esta interacción.
        /// Nulo si la interacción no derivó en una cotización.
        /// </summary>
        public Guid? CotizacionServicioId { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Cliente con quien se tuvo la interacción.</summary>
        public virtual Cliente? Cliente { get; set; }

        /// <summary>Empleado que registró y realizó la interacción.</summary>
        public virtual Empleado? Empleado { get; set; }
    }
}
