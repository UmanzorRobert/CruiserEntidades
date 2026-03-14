using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Geografia
{
    /// <summary>
    /// Provincia, comunidad autónoma o región administrativa de primer nivel de un país.
    /// Para España incluye las 50 provincias más Ceuta y Melilla como ciudades autónomas,
    /// con sus particularidades fiscales (IGIC en Canarias, IPSI en Ceuta/Melilla).
    ///
    /// Soporta jerarquía de dos niveles mediante ProvinciaPadreId:
    /// Comunidad Autónoma → Provincia (Ej: Andalucía → Sevilla).
    ///
    /// SEED INICIAL: 50 provincias españolas + Ceuta + Melilla + 17 CCAA.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(10);
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.Capital).HasMaxLength(100);
    ///   builder.Property(x => x.Latitud).HasPrecision(9, 6);
    ///   builder.Property(x => x.Longitud).HasPrecision(9, 6);
    ///   builder.HasIndex(x => new { x.PaisId, x.Codigo }).IsUnique();
    ///   builder.HasIndex(x => x.Nombre);
    ///
    ///   Auto-relación jerárquica:
    ///   builder.HasOne(p => p.ProvinciaPadre).WithMany(p => p.SubProvincias)
    ///          .HasForeignKey(p => p.ProvinciaPadreId).OnDelete(DeleteBehavior.Restrict);
    /// </remarks>
    public class Provincia : EntidadBase
    {
        /// <summary>
        /// Identificador del país al que pertenece esta provincia.
        /// FK hacia Paises.
        /// </summary>
        public Guid PaisId { get; set; }

        /// <summary>
        /// Identificador de la provincia padre (comunidad autónoma).
        /// Nulo si es de primer nivel (comunidad autónoma o provincia sin jerarquía superior).
        /// </summary>
        public Guid? ProvinciaPadreId { get; set; }

        /// <summary>
        /// Código único de la provincia dentro del país.
        /// Para España: código INE de 2 dígitos (01-52 + CE/ML para Ceuta/Melilla).
        /// Ejemplos: "28" (Madrid), "41" (Sevilla), "35" (Las Palmas), "CE" (Ceuta).
        /// </summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>
        /// Nombre oficial de la provincia en español.
        /// Ejemplo: "Madrid", "Sevilla", "Barcelona", "Las Palmas".
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Nombre de la capital de la provincia.
        /// Ejemplo: "Madrid", "Sevilla", "Barcelona", "Las Palmas de Gran Canaria".
        /// </summary>
        public string? Capital { get; set; }

        /// <summary>
        /// Latitud del centroide geográfico de la provincia en grados decimales.
        /// Usado para cálculos de proximidad y visualización en mapas Leaflet.js.
        /// </summary>
        public decimal? Latitud { get; set; }

        /// <summary>
        /// Longitud del centroide geográfico de la provincia en grados decimales.
        /// </summary>
        public decimal? Longitud { get; set; }

        /// <summary>
        /// Régimen fiscal especial aplicable en esta provincia.
        /// Determina qué tipo de impuesto (IVA, IGIC, IPSI) se aplica en facturas
        /// a clientes domiciliados en esta provincia.
        /// </summary>
        public RegimenFiscalEspecial RegimenFiscalEspecial { get; set; }
            = RegimenFiscalEspecial.IVAGeneral;

        /// <summary>
        /// Código NUTS (Nomenclatura de Unidades Territoriales Estadísticas) de la UE.
        /// Usado para estadísticas y reportes europeos. Ejemplo: "ES300" (Madrid).
        /// </summary>
        public string? CodigoNUTS { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>País al que pertenece esta provincia.</summary>
        public virtual Pais? Pais { get; set; }

        /// <summary>
        /// Provincia padre (comunidad autónoma) si existe jerarquía de dos niveles.
        /// </summary>
        public virtual Provincia? ProvinciaPadre { get; set; }

        /// <summary>Sub-provincias dependientes de esta (ej. provincias de una CCAA).</summary>
        public virtual ICollection<Provincia> SubProvincias { get; set; }
            = new List<Provincia>();

        /// <summary>Ciudades y municipios pertenecientes a esta provincia.</summary>
        public virtual ICollection<Ciudad> Ciudades { get; set; }
            = new List<Ciudad>();
    }
}
