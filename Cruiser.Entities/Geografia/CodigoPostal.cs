using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Geografia
{
    /// <summary>
    /// Código postal con su clasificación de zona y parámetros logísticos.
    /// Un mismo municipio puede tener múltiples códigos postales (distritos urbanos).
    ///
    /// Se usa en DireccionCompleta para ubicar con precisión cualquier dirección del sistema.
    /// También se usa en ZonaServicio para delimitar las zonas de reparto y servicio.
    ///
    /// La TarifaEnvioBase y DiasEntregaEstimados se usan en el módulo de rutas
    /// para estimar costos y tiempos de desplazamiento entre órdenes de servicio.
    ///
    /// SEED INICIAL: códigos postales de las capitales de provincia y
    /// zonas de servicio habituales de la empresa.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(10);
    ///   builder.Property(x => x.ZonaReparto).HasMaxLength(100);
    ///   builder.Property(x => x.TarifaEnvioBase).HasPrecision(10, 2);
    ///   builder.HasIndex(x => x.Codigo);
    ///   builder.HasIndex(x => new { x.CiudadId, x.Codigo }).IsUnique();
    /// </remarks>
    public class CodigoPostal : EntidadBase
    {
        /// <summary>
        /// Identificador de la ciudad a la que pertenece este código postal.
        /// FK hacia Ciudades.
        /// </summary>
        public Guid CiudadId { get; set; }

        /// <summary>
        /// Código postal numérico o alfanumérico.
        /// Para España: 5 dígitos (Ejemplo: "28001", "41001", "35001").
        /// Para otros países puede ser alfanumérico (UK: "SW1A 1AA").
        /// </summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>
        /// Clasificación funcional de la zona para optimización logística y tarifas.
        /// </summary>
        public TipoZonaPostal TipoZona { get; set; } = TipoZonaPostal.Urbana;

        /// <summary>
        /// Nombre descriptivo de la zona de reparto asignada a este código postal.
        /// Ejemplo: "Zona Norte Madrid", "Centro Sevilla", "Polígono Industrial Sur".
        /// Útil para agrupar códigos postales en el planificador de rutas.
        /// </summary>
        public string? ZonaReparto { get; set; }

        /// <summary>
        /// Tarifa base de desplazamiento a esta zona en euros.
        /// Se usa en el cálculo de CostoServicio (CostoKilometraje) por orden de servicio.
        /// Nulo si no hay tarifa diferenciada para esta zona (usa tarifa general).
        /// </summary>
        public decimal? TarifaEnvioBase { get; set; }

        /// <summary>
        /// Días laborables estimados para llegar a esta zona desde el almacén/sede principal.
        /// Usado en la planificación de rutas y en las fechas estimadas de servicio.
        /// Por defecto 1 día para zonas urbanas.
        /// </summary>
        public int DiasEntregaEstimados { get; set; } = 1;

        /// <summary>
        /// Latitud del centroide geográfico del código postal.
        /// Más preciso que las coordenadas de la ciudad para rutas con Leaflet.js.
        /// </summary>
        public decimal? Latitud { get; set; }

        /// <summary>
        /// Longitud del centroide geográfico del código postal.
        /// </summary>
        public decimal? Longitud { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Ciudad a la que pertenece este código postal.</summary>
        public virtual Ciudad? Ciudad { get; set; }

        /// <summary>Direcciones registradas en este código postal.</summary>
        public virtual ICollection<DireccionCompleta> Direcciones { get; set; }
            = new List<DireccionCompleta>();
    }
}
