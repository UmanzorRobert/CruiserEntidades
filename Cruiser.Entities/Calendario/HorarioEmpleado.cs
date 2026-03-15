using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Calendario
{
    /// <summary>
    /// Asignación de un horario laboral a un empleado durante un período de tiempo.
    /// Permite gestionar cambios de horario con historial completo: si un empleado
    /// cambia de jornada completa a media jornada, se cierra el registro anterior
    /// y se crea uno nuevo con el nuevo horario.
    ///
    /// El registro con EsActual=true es el horario vigente del empleado.
    /// Los registros históricos se conservan para el cálculo de nóminas y auditoría.
    ///
    /// La integración con ICalendarioService usa HorarioEmpleado para verificar
    /// la disponibilidad del empleado al asignarlo a una nueva orden de servicio.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasIndex(x => new { x.EmpleadoId, x.EsActual }).HasFilter("\"EsActual\" = true");
    ///   builder.HasIndex(x => new { x.EmpleadoId, x.FechaInicio });
    ///   builder.HasIndex(x => x.HorarioId);
    /// </remarks>
    public class HorarioEmpleado : EntidadBase
    {
        /// <summary>FK hacia el empleado al que se asigna el horario.</summary>
        public Guid EmpleadoId { get; set; }

        /// <summary>FK hacia la plantilla de horario asignada al empleado.</summary>
        public Guid HorarioId { get; set; }

        /// <summary>Tipo de jornada que aplica en este período (partida, continua, media jornada).</summary>
        public TipoJornada TipoJornada { get; set; } = TipoJornada.Partida;

        /// <summary>Fecha de inicio de vigencia de este horario para el empleado.</summary>
        public DateOnly FechaInicio { get; set; }

        /// <summary>
        /// Fecha de fin de vigencia. Nulo si es el horario actual activo del empleado.
        /// Se rellena automáticamente al asignar un nuevo horario al empleado.
        /// </summary>
        public DateOnly? FechaFin { get; set; }

        /// <summary>
        /// Indica si este es el horario actual y vigente del empleado.
        /// Solo puede haber un registro activo (EsActual=true) por empleado.
        /// </summary>
        public bool EsActual { get; set; } = true;

        /// <summary>Motivo del cambio de horario. Ejemplo: "Reducción jornada por conciliación familiar".</summary>
        public string? MotivoAsignacion { get; set; }

        /// <summary>FK hacia el usuario (RRHH) que realizó el cambio de horario.</summary>
        public Guid? AsignadoPorId { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Empleado al que pertenece este registro de horario.</summary>
        public virtual Comercial.Empleado? Empleado { get; set; }

        /// <summary>Plantilla de horario asignada.</summary>
        public virtual Horario? Horario { get; set; }
    }
}
