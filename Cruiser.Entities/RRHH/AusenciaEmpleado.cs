using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.RRHH
{
    /// <summary>
    /// Ausencia laboral de un empleado: baja médica, permiso retribuido, asuntos propios,
    /// accidente de trabajo u otro tipo de ausencia del puesto de trabajo.
    ///
    /// El flujo de aprobación usa AprobacionDocumento con TipoDocumento = AusenciaEmpleado
    /// para ausencias que requieren autorización previa del supervisor.
    ///
    /// AfectaFacturacion=true indica que la ausencia puede afectar al servicio de un cliente,
    /// lo que dispara la lógica de sustitución de empleado en IOrdenServicioService.
    ///
    /// El DocumentoJustificante almacena la ruta del PDF del parte médico u otro
    /// documento acreditativo subido al sistema de archivos.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasIndex(x => new { x.EmpleadoId, x.FechaInicio });
    ///   builder.HasIndex(x => new { x.Estado, x.FechaInicio });
    ///   builder.HasIndex(x => x.TipoAusencia);
    ///   builder.Property(x => x.DocumentoJustificante).HasMaxLength(1000);
    /// </remarks>
    public class AusenciaEmpleado : EntidadBase
    {
        /// <summary>FK hacia el empleado que registra la ausencia.</summary>
        public Guid EmpleadoId { get; set; }

        /// <summary>FK hacia el supervisor que aprueba o rechaza la ausencia.</summary>
        public Guid? SupervisorId { get; set; }

        /// <summary>Tipo o causa de la ausencia laboral.</summary>
        public TipoAusencia TipoAusencia { get; set; }

        /// <summary>Fecha de inicio de la ausencia (primer día de baja).</summary>
        public DateOnly FechaInicio { get; set; }

        /// <summary>Fecha de fin de la ausencia (último día de baja). Nulo si es indefinida.</summary>
        public DateOnly? FechaFin { get; set; }

        /// <summary>
        /// Número de días naturales totales de la ausencia.
        /// Calculado automáticamente: FechaFin - FechaInicio + 1.
        /// </summary>
        public int DiasCalendario { get; set; } = 0;

        /// <summary>
        /// Número de días laborables de la ausencia (excluye festivos y fines de semana).
        /// Calculado por ICalendarioService descontando los FestivoLaboral del período.
        /// </summary>
        public int DiasLaborables { get; set; } = 0;

        /// <summary>Estado de aprobación de la ausencia.</summary>
        public EstadoAusencia Estado { get; set; } = EstadoAusencia.Solicitada;

        /// <summary>Motivo detallado de la ausencia o justificación del empleado.</summary>
        public string? Motivo { get; set; }

        /// <summary>Ruta relativa del documento justificante adjunto (parte médico, etc.).</summary>
        public string? DocumentoJustificante { get; set; }

        /// <summary>Motivo del rechazo del supervisor cuando Estado = Rechazada.</summary>
        public string? MotivoRechazo { get; set; }

        /// <summary>
        /// Indica si la ausencia puede afectar a la cobertura de servicios asignados al empleado.
        /// True dispara la búsqueda automática de sustituto en IOrdenServicioService.
        /// </summary>
        public bool AfectaFacturacion { get; set; } = true;

        /// <summary>Fecha y hora UTC en que el supervisor resolvió la solicitud.</summary>
        public DateTime? FechaResolucion { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Empleado que registra la ausencia.</summary>
        public virtual Comercial.Empleado? Empleado { get; set; }
    }
}
