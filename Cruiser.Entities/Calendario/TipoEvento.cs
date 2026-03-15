using System.Collections.Generic;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Calendario
{
    /// <summary>
    /// Catálogo de tipos de evento del calendario de la empresa.
    /// Define la duración predeterminada, el color de visualización en FullCalendar,
    /// si requiere ubicación física, si permite recurrencia y
    /// los minutos de anticipación para el recordatorio automático por email.
    ///
    /// SEED: OrdenServicio, CitaPrevia, FormacionEmpleado, ReunionInterna,
    ///       MantenimientoVehiculo, InspeccionCalidad, RecordatorioCobranza, Festivo.
    ///
    /// Nota Blazor: El módulo de Calendario se implementa con Blazor Server
    /// usando JSInterop para inicializar FullCalendar 6.x, con actualización
    /// en tiempo real via SignalR nativo de Blazor.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(50);
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.Color).HasMaxLength(7);
    ///   builder.Property(x => x.Icono).HasMaxLength(100);
    ///   builder.HasIndex(x => x.Codigo).IsUnique();
    /// </remarks>
    public class TipoEvento : EntidadBase
    {
        /// <summary>Código único del tipo de evento. Ejemplo: "ORDEN_SERVICIO", "CITA_PREVIA".</summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>Nombre descriptivo del tipo de evento para la interfaz y el calendario.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Descripción del tipo de evento y cuándo se usa.</summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Duración predeterminada del evento en minutos al crear uno nuevo de este tipo.
        /// El usuario puede ajustarla manualmente. Por defecto: 60 minutos.
        /// </summary>
        public int DuracionPredeterminadaMinutos { get; set; } = 60;

        /// <summary>
        /// Indica si este tipo de evento requiere especificar una ubicación física.
        /// True para OrdenServicio, CitaPrevia. False para reuniones internas o recordatorios.
        /// </summary>
        public bool RequiereUbicacion { get; set; } = false;

        /// <summary>
        /// Indica si los eventos de este tipo pueden configurarse como recurrentes
        /// con un patrón de repetición (diario, semanal, mensual).
        /// </summary>
        public bool PermiteRecurrencia { get; set; } = false;

        /// <summary>
        /// Minutos de anticipación con los que se envía el recordatorio por email al responsable.
        /// Ejemplo: 1440 = recordatorio 24 horas antes. 0 = sin recordatorio automático.
        /// </summary>
        public int MinutosAnticipacionRecordatorio { get; set; } = 60;

        /// <summary>
        /// Color hexadecimal (#RRGGBB) del evento en el calendario FullCalendar.
        /// Cada tipo de evento tiene su propio color para facilitar la lectura visual.
        /// </summary>
        public string? Color { get; set; }

        /// <summary>Clase CSS del icono Font Awesome para el tipo de evento en modales y listas.</summary>
        public string? Icono { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Eventos del calendario de este tipo.</summary>
        public virtual ICollection<EventoCalendario> Eventos { get; set; }
            = new List<EventoCalendario>();
    }
}
