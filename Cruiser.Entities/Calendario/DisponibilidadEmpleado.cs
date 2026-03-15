using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Calendario
{
    /// <summary>
    /// Excepción puntual al horario regular de un empleado para una fecha específica.
    /// Permite gestionar disponibilidades e indisponibilidades excepcionales sin
    /// modificar el HorarioEmpleado base: festivos trabajados, guardias, asuntos
    /// propios puntuales, horarios especiales en períodos concretos.
    ///
    /// El servicio ICalendarioService.VerificarConflictosHorario consulta tanto
    /// el HorarioEmpleado (horario base) como las DisponibilidadEmpleado (excepciones)
    /// al validar la disponibilidad de un empleado para una nueva orden de servicio.
    ///
    /// Las excepciones recurrentes (EsRecurrente=true) se generan automáticamente
    /// para festivos locales que afectan a un empleado específico.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Motivo).HasMaxLength(500);
    ///   builder.HasIndex(x => new { x.EmpleadoId, x.Fecha });
    ///   builder.HasIndex(x => new { x.Fecha, x.TipoExcepcion });
    /// </remarks>
    public class DisponibilidadEmpleado : EntidadBase
    {
        /// <summary>FK hacia el empleado al que aplica esta excepción de disponibilidad.</summary>
        public Guid EmpleadoId { get; set; }

        /// <summary>Fecha específica en que aplica la excepción al horario regular.</summary>
        public DateOnly Fecha { get; set; }

        /// <summary>Tipo de excepción: disponible, no disponible u horario especial.</summary>
        public TipoExcepcionHorario TipoExcepcion { get; set; }

        /// <summary>
        /// Hora de inicio del período especial.
        /// Solo aplica cuando TipoExcepcion = Disponible o HorarioEspecial.
        /// Nulo cuando TipoExcepcion = NoDisponible (todo el día no disponible).
        /// </summary>
        public string? HoraInicio { get; set; }

        /// <summary>
        /// Hora de fin del período especial.
        /// Nulo cuando TipoExcepcion = NoDisponible.
        /// </summary>
        public string? HoraFin { get; set; }

        /// <summary>
        /// Motivo de la excepción al horario.
        /// Ejemplo: "Guardia de fin de semana", "Asunto propio", "Festivo local trabajado".
        /// </summary>
        public string? Motivo { get; set; }

        /// <summary>
        /// Indica si esta excepción se repite anualmente en la misma fecha.
        /// True para festivos anuales fijos (Navidad, Reyes) que siempre aplican.
        /// </summary>
        public bool EsRecurrente { get; set; } = false;

        /// <summary>FK hacia el usuario que registró esta excepción de disponibilidad.</summary>
        public Guid? RegistradoPorId { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Empleado al que aplica esta excepción de disponibilidad.</summary>
        public virtual Comercial.Empleado? Empleado { get; set; }
    }
}
