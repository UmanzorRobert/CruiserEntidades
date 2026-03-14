using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Geografia
{
    /// <summary>
    /// Municipio o ciudad perteneciente a una provincia.
    /// Almacena coordenadas GPS, altitud, población y zona horaria para
    /// su uso en la planificación de rutas (Leaflet.js) y en el cálculo
    /// de distancias entre zonas de servicio.
    ///
    /// SEED INICIAL: capitales de provincia españolas + principales ciudades
    /// con más de 50.000 habitantes. Total aproximado: 300-500 registros.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(150);
    ///   builder.Property(x => x.NombreNormalizado).HasMaxLength(150);
    ///   builder.Property(x => x.Latitud).HasPrecision(9, 6);
    ///   builder.Property(x => x.Longitud).HasPrecision(9, 6);
    ///   builder.Property(x => x.ZonaHoraria).HasMaxLength(50);
    ///   builder.Property(x => x.CodigoINE).HasMaxLength(10);
    ///   builder.HasIndex(x => new { x.ProvinciaId, x.Nombre });
    ///   builder.HasIndex(x => x.CodigoINE).HasFilter("\"CodigoINE\" IS NOT NULL");
    ///   builder.HasIndex(x => x.EsCapital);
    /// </remarks>
    public class Ciudad : EntidadBase
    {
        /// <summary>
        /// Identificador de la provincia a la que pertenece esta ciudad.
        /// FK hacia Provincias.
        /// </summary>
        public Guid ProvinciaId { get; set; }

        /// <summary>
        /// Nombre oficial del municipio o ciudad.
        /// Ejemplo: "Madrid", "Sevilla", "Hospitalet de Llobregat", "Las Palmas de Gran Canaria".
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Nombre normalizado sin tildes ni caracteres especiales para búsquedas.
        /// Generado automáticamente al guardar. Ejemplo: "Hospitalet de Llobregat".
        /// </summary>
        public string? NombreNormalizado { get; set; }

        /// <summary>
        /// Latitud del centroide geográfico de la ciudad en grados decimales WGS84.
        /// Usado en mapas Leaflet.js y cálculos de proximidad GPS del módulo de fichaje.
        /// </summary>
        public decimal? Latitud { get; set; }

        /// <summary>
        /// Longitud del centroide geográfico de la ciudad en grados decimales WGS84.
        /// </summary>
        public decimal? Longitud { get; set; }

        /// <summary>
        /// Altitud media del municipio sobre el nivel del mar en metros.
        /// Relevante para zonas con condiciones climáticas especiales.
        /// </summary>
        public int? AltitudMetros { get; set; }

        /// <summary>
        /// Población aproximada del municipio según último censo disponible.
        /// Usado para priorización de zonas de servicio y análisis de mercado.
        /// </summary>
        public int? Poblacion { get; set; }

        /// <summary>
        /// Zona horaria oficial del municipio en formato IANA.
        /// La mayoría de España: "Europe/Madrid". Canarias: "Atlantic/Canary".
        /// </summary>
        public string ZonaHoraria { get; set; } = "Europe/Madrid";

        /// <summary>
        /// Indica si la ciudad es capital de su provincia.
        /// Las capitales de provincia tienen mayor relevancia en búsquedas y filtros.
        /// </summary>
        public bool EsCapital { get; set; } = false;

        /// <summary>
        /// Código INE (Instituto Nacional de Estadística) del municipio español.
        /// Formato: 5 dígitos (código provincia 2 dígitos + código municipio 3 dígitos).
        /// Ejemplo: "28079" (Madrid), "41091" (Sevilla).
        /// </summary>
        public string? CodigoINE { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Provincia a la que pertenece esta ciudad.</summary>
        public virtual Provincia? Provincia { get; set; }

        /// <summary>Códigos postales que corresponden a esta ciudad.</summary>
        public virtual ICollection<CodigoPostal> CodigosPostales { get; set; }
            = new List<CodigoPostal>();
    }
}
