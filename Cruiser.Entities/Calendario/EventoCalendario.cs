using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Calendario
{
    /// <summary>
    /// Evento en el calendario de la empresa. Puede representar una orden de servicio,
    /// una cita previa, una formación, una reunión interna o cualquier evento
    /// del catálogo TipoEvento.
    ///
    /// RECURRENCIA: Los eventos recurrentes tienen un EventoPadreId que apunta
    /// al evento original. El PatronRecurrencia almacena la regla en formato
    /// compatible con la especificación RFC 5545 (iCalendar RRULE):
    /// Ejemplo: "FREQ=WEEKLY;BYDAY=MO,WE,FR" o "FREQ=MONTHLY;BYMONTHDAY=1".
    ///
    /// INTEGRACIÓN CON FULLCALENDAR: Los eventos se cargan directamente desde
    /// ICalendarioService en el componente Blazor, sin pasar por un API Controller.
    /// Los cambios de fecha/hora por drag-drop se sincronizan via SignalR nativo.
    ///
    /// INTEGRACIÓN CON ORDEN DE SERVICIO: Cada OrdenServicio tiene un EventoCalendarioId
    /// que vincula la orden con su bloque de tiempo en el calendario.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Titulo).IsRequired().HasMaxLength(200);
    ///   builder.Property(x => x.PatronRecurrencia).HasMaxLength(500);
    ///   builder.HasIndex(x => new { x.EmpleadoId, x.FechaInicio });
    ///   builder.HasIndex(x => new { x.ClienteId, x.FechaInicio });
    ///   builder.HasIndex(x => new { x.FechaInicio, x.FechaFin });
    ///   builder.HasIndex(x => x.OrdenServicioId).HasFilter("\"OrdenServicioId\" IS NOT NULL");
    ///   builder.HasIndex(x => x.Estado);
    ///   builder.HasIndex(x => x.EventoPadreId).HasFilter("\"EventoPadreId\" IS NOT NULL");
    /// </remarks>
    public class EventoCalendario : EntidadBase
    {
        /// <summary>FK hacia el tipo de evento que define el comportamiento y la apariencia.</summary>
        public Guid TipoEventoId { get; set; }

        /// <summary>
        /// FK hacia la orden de servicio vinculada a este evento del calendario.
        /// Nulo para eventos que no son órdenes de servicio.
        /// </summary>
        public Guid? OrdenServicioId { get; set; }

        /// <summary>
        /// FK hacia el empleado principal responsable de este evento.
        /// Nulo para eventos generales de la empresa sin responsable individual.
        /// </summary>
        public Guid? EmpleadoId { get; set; }

        /// <summary>FK hacia el cliente al que se refiere este evento. Nulo para eventos internos.</summary>
        public Guid? ClienteId { get; set; }

        // ── Tiempo ───────────────────────────────────────────────────────────

        /// <summary>Título del evento tal como aparece en el calendario FullCalendar.</summary>
        public string Titulo { get; set; } = string.Empty;

        /// <summary>Fecha y hora UTC de inicio del evento.</summary>
        public DateTime FechaInicio { get; set; }

        /// <summary>Fecha y hora UTC de fin del evento.</summary>
        public DateTime FechaFin { get; set; }

        /// <summary>
        /// Indica si el evento ocupa todo el día sin hora específica.
        /// True para festivos, formaciones de día completo, etc.
        /// </summary>
        public bool EsDiaCompleto { get; set; } = false;

        /// <summary>Estado actual del evento en su ciclo de vida.</summary>
        public EstadoEvento Estado { get; set; } = EstadoEvento.Planificado;

        // ── Recurrencia ──────────────────────────────────────────────────────

        /// <summary>Indica si este evento es recurrente con un patrón de repetición.</summary>
        public bool EsRecurrente { get; set; } = false;

        /// <summary>
        /// Regla de recurrencia en formato RFC 5545 (iCalendar RRULE).
        /// Ejemplo: "FREQ=WEEKLY;BYDAY=MO,WE;COUNT=10" o "FREQ=MONTHLY;BYMONTHDAY=1;UNTIL=20261231".
        /// Solo aplica cuando EsRecurrente = true.
        /// </summary>
        public string? PatronRecurrencia { get; set; }

        /// <summary>
        /// FK hacia el evento padre original del que esta instancia es una ocurrencia.
        /// Nulo para el evento padre original y eventos no recurrentes.
        /// </summary>
        public Guid? EventoPadreId { get; set; }

        // ── Detalle ──────────────────────────────────────────────────────────

        /// <summary>Descripción detallada del evento. Visible al expandir en el calendario.</summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Ubicación física del evento (dirección del cliente, sala de reuniones, etc.).
        /// Requerida cuando TipoEvento.RequiereUbicacion = true.
        /// </summary>
        public string? Ubicacion { get; set; }

        /// <summary>
        /// Resultado o notas tras completarse el evento.
        /// Solo relevante cuando Estado = Completado.
        /// </summary>
        public string? Resultado { get; set; }

        /// <summary>Indica si se ha enviado el recordatorio automático por email al responsable.</summary>
        public bool ReminderEnviado { get; set; } = false;

        /// <summary>Fecha y hora UTC en que se envió el recordatorio.</summary>
        public DateTime? FechaReminderEnviado { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Tipo de evento que define el comportamiento de este evento.</summary>
        public virtual TipoEvento? TipoEvento { get; set; }

        /// <summary>Empleado responsable del evento.</summary>
        public virtual Comercial.Empleado? Empleado { get; set; }

        /// <summary>Cliente al que se refiere el evento.</summary>
        public virtual Comercial.Cliente? Cliente { get; set; }

        /// <summary>Evento padre si este es una ocurrencia de una serie recurrente.</summary>
        public virtual EventoCalendario? EventoPadre { get; set; }

        /// <summary>Ocurrencias hijas de este evento recurrente.</summary>
        public virtual ICollection<EventoCalendario> Ocurrencias { get; set; }
            = new List<EventoCalendario>();
    }
}
