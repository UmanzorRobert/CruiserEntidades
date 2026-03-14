using System;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Comercial
{
    /// <summary>
    /// Registro de la pertenencia de un empleado a un equipo de trabajo
    /// durante un período de tiempo determinado.
    ///
    /// La gestión histórica permite saber qué empleados formaban parte
    /// de un equipo en la fecha de cualquier orden de servicio pasada.
    ///
    /// Solo los registros sin FechaFin (o con FechaFin futura) son miembros activos.
    /// Un empleado puede pertenecer a varios equipos simultáneamente (equipos de reemplazo).
    ///
    /// NO hereda de EntidadBase: tabla de asociación con campos temporales.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.HasIndex(x => new { x.EquipoTrabajoId, x.EmpleadoId, x.FechaInicio });
    ///   builder.HasIndex(x => new { x.EmpleadoId, x.EsActivo });
    /// </remarks>
    public class EmpleadoEquipo
    {
        /// <summary>Identificador único del registro de membresía.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>FK hacia el equipo de trabajo.</summary>
        public Guid EquipoTrabajoId { get; set; }

        /// <summary>FK hacia el empleado miembro del equipo.</summary>
        public Guid EmpleadoId { get; set; }

        /// <summary>Rol que desempeña el empleado dentro de este equipo.</summary>
        public RolEnEquipo RolEnEquipo { get; set; } = RolEnEquipo.Auxiliar;

        /// <summary>Fecha de incorporación del empleado al equipo.</summary>
        public DateOnly FechaInicio { get; set; }

        /// <summary>Fecha de salida del empleado del equipo. Nulo si sigue activo.</summary>
        public DateOnly? FechaFin { get; set; }

        /// <summary>
        /// Indica si la membresía está actualmente vigente.
        /// FechaFin == null || FechaFin >= hoy.
        /// </summary>
        public bool EsActivo { get; set; } = true;

        /// <summary>Motivo de la incorporación o salida del equipo.</summary>
        public string? Motivo { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Equipo de trabajo al que pertenece el empleado.</summary>
        public virtual EquipoTrabajo? EquipoTrabajo { get; set; }

        /// <summary>Empleado miembro del equipo.</summary>
        public virtual Empleado? Empleado { get; set; }
    }
}
