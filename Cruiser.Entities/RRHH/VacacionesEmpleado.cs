using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.RRHH
{
    /// <summary>
    /// Bolsa de días de vacaciones anuales de un empleado.
    /// Gestiona los días correspondientes por convenio, los días disfrutados,
    /// los días pendientes y los días congelados (traslado de un ejercicio al siguiente).
    ///
    /// La FechaCorte define hasta qué fecha se acumulan los días del ejercicio.
    /// Por convenio colectivo de limpieza, el período de disfrute suele ser
    /// del 1 de junio al 31 de agosto, aunque puede variar por acuerdo.
    ///
    /// Los días disfrutados se descuentan automáticamente al aprobar una
    /// AusenciaEmpleado con TipoAusencia = Vacaciones.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasIndex(x => new { x.EmpleadoId, x.Anio }).IsUnique();
    ///   builder.HasIndex(x => x.Anio);
    /// </remarks>
    public class VacacionesEmpleado : EntidadBase
    {
        /// <summary>FK hacia el empleado al que pertenece esta bolsa de vacaciones.</summary>
        public Guid EmpleadoId { get; set; }

        /// <summary>Año del ejercicio de vacaciones (ejemplo: 2026).</summary>
        public int Anio { get; set; }

        /// <summary>
        /// Días de vacaciones que corresponden al empleado en este ejercicio según convenio.
        /// Por defecto: 30 días naturales o 22 días laborables (según convenio del sector).
        /// </summary>
        public int DiasCorrespondientes { get; set; } = 22;

        /// <summary>Días de vacaciones disfrutados hasta la fecha en este ejercicio.</summary>
        public int DiasUsados { get; set; } = 0;

        /// <summary>
        /// Días de vacaciones pendientes de disfrutar: DiasCorrespondientes - DiasUsados + DiasCongelados.
        /// Calculado automáticamente.
        /// </summary>
        public int DiasPendientes { get; set; } = 0;

        /// <summary>
        /// Días congelados trasladados del ejercicio anterior por no haberse podido disfrutar
        /// (baja médica prolongada, acuerdo de empresa). Solo válidos hasta la FechaCorte.
        /// </summary>
        public int DiasCongelados { get; set; } = 0;

        /// <summary>
        /// Días de vacaciones adicionales devengados por antigüedad u otros conceptos del convenio.
        /// </summary>
        public int DiasAntiguedad { get; set; } = 0;

        /// <summary>
        /// Fecha límite hasta la que deben disfrutarse los días congelados del ejercicio anterior.
        /// Pasada esta fecha, los días congelados caducan y se pierden.
        /// </summary>
        public DateOnly? FechaCorte { get; set; }

        /// <summary>Notas sobre particularidades del cómputo de vacaciones de este ejercicio.</summary>
        public string? Notas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Empleado al que pertenece esta bolsa de vacaciones.</summary>
        public virtual Comercial.Empleado? Empleado { get; set; }
    }
}
