using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Catalogo
{
    /// <summary>
    /// Catálogo de unidades de medida para productos y servicios.
    /// Soporta conversiones entre unidades del mismo tipo mediante
    /// FactorConversion y UnidadBaseId.
    ///
    /// Ejemplos:
    /// - Litro (unidad base de Volumen, factor 1.0)
    /// - Mililitro (Volumen, factor 0.001, base = Litro)
    /// - Kilogramo (unidad base de Masa, factor 1.0)
    /// - Gramo (Masa, factor 0.001, base = Kilogramo)
    ///
    /// Las unidades de tipo Servicio se usan para facturar servicios
    /// por visita, servicio o intervención en lugar de por cantidad física.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(20);
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.Simbolo).IsRequired().HasMaxLength(10);
    ///   builder.Property(x => x.FactorConversion).HasPrecision(18, 8);
    ///   builder.HasIndex(x => x.Codigo).IsUnique();
    ///
    ///   Auto-relación de conversión:
    ///   builder.HasOne(u => u.UnidadBase).WithMany(u => u.UnidadesDerivadasDe)
    ///          .HasForeignKey(u => u.UnidadBaseId).OnDelete(DeleteBehavior.Restrict);
    /// </remarks>
    public class UnidadMedida : EntidadBase
    {
        /// <summary>
        /// Código único de la unidad en formato corto.
        /// Ejemplos: "KG", "L", "ML", "UN", "H", "M2", "SERV".
        /// </summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>
        /// Nombre completo de la unidad de medida en español.
        /// Ejemplo: "Kilogramo", "Litro", "Mililitro", "Unidad", "Hora", "Metro cuadrado".
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Símbolo internacional de la unidad.
        /// Ejemplo: "kg", "L", "ml", "ud", "h", "m²".
        /// Se muestra en formularios y documentos para máxima legibilidad.
        /// </summary>
        public string Simbolo { get; set; } = string.Empty;

        /// <summary>
        /// Tipo o dimensión física que mide esta unidad.
        /// Permite agrupar unidades compatibles y habilitar conversiones.
        /// </summary>
        public TipoUnidadMedida Tipo { get; set; }

        /// <summary>
        /// Indica si esta unidad pertenece al Sistema Internacional de Unidades (SI).
        /// Ejemplo: kilogramo = true, libra = false.
        /// </summary>
        public bool EsSistemaInternacional { get; set; } = true;

        /// <summary>
        /// Identificador de la unidad base de este tipo.
        /// Nulo si esta unidad ES la unidad base.
        /// Ejemplo: Mililitro.UnidadBaseId = Litro.Id
        /// </summary>
        public Guid? UnidadBaseId { get; set; }

        /// <summary>
        /// Factor de conversión respecto a la unidad base del mismo tipo.
        /// 1 unidad_actual = FactorConversion × unidad_base.
        /// Ejemplo: 1 ml = 0.001 L → Mililitro.FactorConversion = 0.001
        /// Para la unidad base el factor es siempre 1.0.
        /// </summary>
        public decimal FactorConversion { get; set; } = 1.0m;

        /// <summary>
        /// Número de decimales permitidos al introducir cantidades en esta unidad.
        /// Ejemplo: Unidades = 0 decimales, Litros = 3 decimales, Horas = 2 decimales.
        /// </summary>
        public int DecimalesPermitidos { get; set; } = 2;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Unidad base de este tipo (ej. Litro para Mililitro).</summary>
        public virtual UnidadMedida? UnidadBase { get; set; }

        /// <summary>Unidades derivadas que toman esta unidad como base de conversión.</summary>
        public virtual ICollection<UnidadMedida> UnidadesDerivadasDe { get; set; }
            = new List<UnidadMedida>();

        /// <summary>Productos que usan esta unidad de medida.</summary>
        public virtual ICollection<Producto> Productos { get; set; }
            = new List<Producto>();
    }
}
