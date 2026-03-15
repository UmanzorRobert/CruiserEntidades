using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.RRHH
{
    /// <summary>
    /// Reconocimiento o distinción otorgada a un empleado por su desempeño,
    /// conducta ejemplar, antigüedad o aportación a la empresa.
    ///
    /// Los reconocimientos públicos (EsPublico=true) aparecen en el panel
    /// de reconocimientos del dashboard para que todo el equipo los vea,
    /// fomentando una cultura de reconocimiento positivo.
    ///
    /// Los PuntosOtorgados forman parte de un sistema de gamificación interno
    /// que puede canjearse por beneficios según la política de la empresa.
    ///
    /// La integración con el dashboard del supervisor muestra el ranking de
    /// empleados por reconocimientos recibidos para motivar al equipo.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Descripcion).HasMaxLength(1000);
    ///   builder.HasIndex(x => new { x.EmpleadoId, x.FechaOtorgamiento });
    ///   builder.HasIndex(x => x.EsPublico);
    ///   builder.HasIndex(x => x.TipoReconocimiento);
    /// </remarks>
    public class Reconocimiento : EntidadBase
    {
        /// <summary>FK hacia el empleado que recibe el reconocimiento.</summary>
        public Guid EmpleadoId { get; set; }

        /// <summary>FK hacia el supervisor, director o compañero que otorga el reconocimiento.</summary>
        public Guid OtorgadoPorId { get; set; }

        /// <summary>Tipo de reconocimiento otorgado.</summary>
        public TipoReconocimiento TipoReconocimiento { get; set; }

        /// <summary>Fecha en que se otorgó el reconocimiento.</summary>
        public DateOnly FechaOtorgamiento { get; set; }

        /// <summary>Descripción detallada de la razón del reconocimiento y los logros reconocidos.</summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Indica si el reconocimiento es visible públicamente en el panel de toda la empresa.
        /// False para reconocimientos privados entre supervisor y empleado.
        /// </summary>
        public bool EsPublico { get; set; } = true;

        /// <summary>
        /// Puntos asociados al reconocimiento para el sistema de gamificación interno.
        /// Acumulados en el perfil del empleado. 0 si no aplica el sistema de puntos.
        /// </summary>
        public int PuntosOtorgados { get; set; } = 0;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Empleado que recibe el reconocimiento.</summary>
        public virtual Comercial.Empleado? Empleado { get; set; }
    }
}
