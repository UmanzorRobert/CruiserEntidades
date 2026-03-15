using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Calendario
{
    /// <summary>
    /// Solicitud de cita previa realizada por un cliente para recibir un servicio.
    /// Permite al cliente solicitar una visita en una fecha y franja horaria concretas,
    /// que el equipo confirma o rechaza en función de la disponibilidad de empleados.
    ///
    /// Al confirmar la cita, el sistema crea automáticamente un EventoCalendario
    /// y, si procede, una OrdenServicio vinculada.
    ///
    /// El ReminderEnviado controla que el job de Hangfire no envíe más de un
    /// recordatorio previo al cliente por la misma cita.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.MotivoRechazo).HasMaxLength(500);
    ///   builder.Property(x => x.Notas).HasMaxLength(1000);
    ///   builder.HasIndex(x => new { x.ClienteId, x.Estado });
    ///   builder.HasIndex(x => new { x.EmpleadoId, x.FechaSolicitada }).HasFilter("\"EmpleadoId\" IS NOT NULL");
    ///   builder.HasIndex(x => x.FechaSolicitada);
    ///   builder.HasIndex(x => x.Estado);
    /// </remarks>
    public class CitaPrevia : EntidadBase
    {
        /// <summary>FK hacia el cliente que solicita la cita previa.</summary>
        public Guid ClienteId { get; set; }

        /// <summary>
        /// FK hacia el empleado asignado para atender la cita.
        /// Puede ser nulo hasta que el equipo confirme y asigne el empleado.
        /// </summary>
        public Guid? EmpleadoId { get; set; }

        /// <summary>FK hacia el tipo de servicio solicitado en la cita.</summary>
        public Guid? TipoServicioId { get; set; }

        /// <summary>FK hacia el EventoCalendario creado al confirmar la cita.</summary>
        public Guid? EventoCalendarioId { get; set; }

        /// <summary>Fecha y hora UTC solicitada por el cliente para la cita.</summary>
        public DateTime FechaSolicitada { get; set; }

        /// <summary>
        /// Fecha y hora UTC alternativa propuesta por el cliente.
        /// Permite al equipo ofrecer la segunda opción si la primera no está disponible.
        /// </summary>
        public DateTime? FechaAlternativa { get; set; }

        /// <summary>
        /// Duración estimada del servicio solicitado en minutos.
        /// Por defecto: TipoServicio.DuracionEstimadaMinutos.
        /// </summary>
        public int DuracionEstimadaMinutos { get; set; } = 60;

        /// <summary>Estado actual de la solicitud de cita previa.</summary>
        public EstadoCitaPrevia Estado { get; set; } = EstadoCitaPrevia.Pendiente;

        /// <summary>Canal a través del cual el cliente realizó la solicitud.</summary>
        public CanalSolicitudCita CanalSolicitud { get; set; } = CanalSolicitudCita.Telefono;

        /// <summary>Notas o instrucciones especiales del cliente para la cita.</summary>
        public string? Notas { get; set; }

        /// <summary>Motivo del rechazo de la cita cuando Estado = Rechazada.</summary>
        public string? MotivoRechazo { get; set; }

        /// <summary>
        /// Indica si se ha enviado el recordatorio automático al cliente
        /// previo a la cita confirmada. Evita duplicados de recordatorio.
        /// </summary>
        public bool ReminderEnviado { get; set; } = false;

        /// <summary>FK hacia el usuario que gestionó (confirmó o rechazó) la cita.</summary>
        public Guid? GestionadoPorId { get; set; }

        /// <summary>Fecha y hora UTC en que se gestionó la cita.</summary>
        public DateTime? FechaGestion { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Cliente que solicita la cita previa.</summary>
        public virtual Comercial.Cliente? Cliente { get; set; }

        /// <summary>Empleado asignado para atender la cita.</summary>
        public virtual Comercial.Empleado? Empleado { get; set; }

        /// <summary>Evento de calendario creado al confirmar la cita.</summary>
        public virtual EventoCalendario? EventoCalendario { get; set; }
    }
}
