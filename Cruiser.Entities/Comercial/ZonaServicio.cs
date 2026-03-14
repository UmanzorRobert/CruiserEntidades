using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Comercial
{
    /// <summary>
    /// Zona geográfica de servicio que agrupa códigos postales atendidos por la empresa.
    /// Permite asignar empleados y equipos responsables por zona y optimizar
    /// la planificación de rutas de servicio.
    ///
    /// Las coordenadas del centro (LatitudCentro, LongitudCentro) y el radio en kilómetros
    /// se usan en el mapa Leaflet.js del panel de supervisión para visualizar
    /// la cobertura geográfica de la empresa y la posición de los empleados en campo.
    ///
    /// SEED: zonas geográficas principales según la configuración inicial de la empresa.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(20);
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.Color).HasMaxLength(7);
    ///   builder.Property(x => x.LatitudCentro).HasPrecision(9, 6);
    ///   builder.Property(x => x.LongitudCentro).HasPrecision(9, 6);
    ///   builder.HasIndex(x => x.Codigo).IsUnique();
    /// </remarks>
    public class ZonaServicio : EntidadBase
    {
        /// <summary>
        /// Código único de la zona en el sistema.
        /// Ejemplo: "ZONA-NORTE", "ZONA-CENTRO", "ZONA-SUR-SEV".
        /// </summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>Nombre descriptivo de la zona para la interfaz.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Descripción del área geográfica que cubre la zona.</summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// FK hacia el empleado o supervisor responsable de esta zona de servicio.
        /// Recibe alertas de incidencias y gestiona los equipos de la zona.
        /// </summary>
        public Guid? ResponsableId { get; set; }

        /// <summary>
        /// Color hexadecimal (#RRGGBB) para representar la zona en el mapa Leaflet.js.
        /// Cada zona tiene un color distinto para facilitar la visualización geográfica.
        /// </summary>
        public string? Color { get; set; }

        // ── Coordenadas geográficas del centro de la zona ────────────────────

        /// <summary>
        /// Latitud del punto central de la zona geográfica en grados decimales WGS84.
        /// Centro del círculo de cobertura en Leaflet.js.
        /// </summary>
        public decimal? LatitudCentro { get; set; }

        /// <summary>Longitud del punto central de la zona geográfica en grados decimales WGS84.</summary>
        public decimal? LongitudCentro { get; set; }

        /// <summary>
        /// Radio de cobertura de la zona en kilómetros desde el punto central.
        /// Usado para dibujar el círculo de cobertura en el mapa Leaflet.js
        /// y para validar si un cliente está dentro de la zona de servicio.
        /// </summary>
        public decimal? KilometrosRadio { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Responsable de la zona de servicio.</summary>
        public virtual Empleado? Responsable { get; set; }

        /// <summary>Equipos de trabajo asignados a esta zona.</summary>
        public virtual ICollection<EquipoTrabajo> Equipos { get; set; }
            = new List<EquipoTrabajo>();
    }
}
