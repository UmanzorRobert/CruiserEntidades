using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.DominioLimpieza
{
    /// <summary>
    /// Inspección de calidad realizada por un supervisor en una orden de servicio.
    /// Puede ser rutinaria, sorpresa o posterior a una reclamación del cliente.
    /// Los resultados alimentan los KPIs de calidad y el ranking de empleados.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasIndex(x => new { x.OrdenServicioId, x.TipoInspeccion });
    ///   builder.HasIndex(x => x.InspectorId);
    ///   builder.Property(x => x.PuntuacionMedia).HasPrecision(5, 2);
    /// </remarks>
    public class InspeccionCalidad : EntidadBase
    {
        public Guid OrdenServicioId { get; set; }
        public Guid InspectorId { get; set; }

        /// <summary>Tipo de inspección: Rutinaria, Sorpresa o PostReclamacion.</summary>
        public string TipoInspeccion { get; set; } = "Rutinaria";

        public int PuntuacionLimpieza { get; set; }
        public int PuntuacionProtocolo { get; set; }
        public int PuntuacionPresentacion { get; set; }
        public int PuntuacionSeguridad { get; set; }

        /// <summary>Puntuación media calculada de las cuatro dimensiones de inspección.</summary>
        public decimal PuntuacionMedia { get; set; }

        /// <summary>Observaciones del inspector sobre el servicio inspeccionado.</summary>
        public string? Observaciones { get; set; }

        /// <summary>Acciones correctivas comunicadas al equipo tras la inspección.</summary>
        public string? AccionesCorrectivas { get; set; }

        /// <summary>Indica si la incidencia detectada requiere seguimiento posterior.</summary>
        public bool RequiereSeguimiento { get; set; } = false;

        public virtual OrdenServicio? OrdenServicio { get; set; }
    }
}
