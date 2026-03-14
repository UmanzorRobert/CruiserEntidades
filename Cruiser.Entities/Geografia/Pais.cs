using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Geografia
{
    /// <summary>
    /// Catálogo de países del mundo con sus códigos ISO y datos relevantes
    /// para el funcionamiento del sistema: pertenencia a la UE, zona euro,
    /// código telefónico y continente.
    ///
    /// Se utiliza en DireccionCompleta, Cliente, Empleado, Proveedor y
    /// en la determinación del régimen fiscal aplicable.
    ///
    /// SEED INICIAL: todos los países del mundo (249 según ISO 3166-1).
    /// Con énfasis especial en España y países de la UE más frecuentes.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.CodigoISO).IsRequired().HasMaxLength(2);
    ///   builder.Property(x => x.CodigoISO3).IsRequired().HasMaxLength(3);
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.NombreIngles).HasMaxLength(100);
    ///   builder.Property(x => x.CodigoTelefonico).HasMaxLength(10);
    ///   builder.HasIndex(x => x.CodigoISO).IsUnique();
    ///   builder.HasIndex(x => x.CodigoISO3).IsUnique();
    ///   builder.HasIndex(x => x.Nombre);
    /// </remarks>
    public class Pais : EntidadBase
    {
        /// <summary>
        /// Código ISO 3166-1 alpha-2 de dos letras del país.
        /// Ejemplos: "ES" (España), "FR" (Francia), "DE" (Alemania), "MX" (México).
        /// Es la clave de negocio principal del país en el sistema.
        /// </summary>
        public string CodigoISO { get; set; } = string.Empty;

        /// <summary>
        /// Código ISO 3166-1 alpha-3 de tres letras del país.
        /// Ejemplos: "ESP" (España), "FRA" (Francia), "DEU" (Alemania).
        /// Usado en documentos internacionales y remesas SEPA.
        /// </summary>
        public string CodigoISO3 { get; set; } = string.Empty;

        /// <summary>
        /// Código numérico ISO 3166-1 del país.
        /// Ejemplo: 724 (España), 250 (Francia), 276 (Alemania).
        /// </summary>
        public int? CodigoNumerico { get; set; }

        /// <summary>
        /// Nombre oficial del país en español.
        /// Ejemplo: "España", "Francia", "Alemania", "México".
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Nombre oficial del país en inglés.
        /// Se usa en documentos internacionales y en la interfaz en modo inglés.
        /// </summary>
        public string? NombreIngles { get; set; }

        /// <summary>
        /// Código telefónico internacional del país (prefijo de llamada).
        /// Ejemplos: "+34" (España), "+33" (Francia), "+52" (México).
        /// Incluye el símbolo "+" como prefijo.
        /// </summary>
        public string? CodigoTelefonico { get; set; }

        /// <summary>
        /// Continente al que pertenece el país.
        /// Usado para agrupación en reportes geográficos.
        /// </summary>
        public Continente Continente { get; set; }

        /// <summary>
        /// Indica si el país pertenece a la Unión Europea.
        /// Afecta a las reglas de facturación intracomunitaria y
        /// a la validación del NIF-IVA europeo (VIES).
        /// </summary>
        public bool PerteneceUE { get; set; } = false;

        /// <summary>
        /// Indica si el país pertenece a la Zona Euro (moneda oficial = EUR).
        /// Usado en cálculos de tipo de cambio y en la generación de remesas SEPA.
        /// </summary>
        public bool PerteneceZonaEuro { get; set; } = false;

        /// <summary>
        /// Indica si el país es el país principal de operación del sistema.
        /// Solo España tiene EsPaisPrincipal=true. Afecta al seed de provincias y ciudades.
        /// </summary>
        public bool EsPaisPrincipal { get; set; } = false;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>
        /// Provincias, comunidades autónomas o regiones de primer nivel de este país.
        /// Solo poblado para España en el seed inicial.
        /// </summary>
        public virtual ICollection<Provincia> Provincias { get; set; }
            = new List<Provincia>();
    }
}
