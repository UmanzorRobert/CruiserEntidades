using System.Collections.Generic;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Facturacion
{
    /// <summary>
    /// Configuración de los tipos de retención fiscal aplicables en facturas.
    /// En España, las retenciones IRPF aplican a facturas emitidas por autónomos
    /// y profesionales en determinadas actividades económicas.
    ///
    /// También cubre retenciones de arrendamiento de bienes inmuebles (Art. 75 RIRPF)
    /// y otras retenciones especiales reguladas por la AEAT.
    ///
    /// SEED: Retención Profesional 15%, Retención Arrendamiento 19%,
    ///       Retención Profesional Nuevos Actividades 7%.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.NombreRetencion).IsRequired().HasMaxLength(150);
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(30);
    ///   builder.Property(x => x.PorcentajeRetencion).HasPrecision(5, 2);
    ///   builder.Property(x => x.CodigoHacienda).HasMaxLength(10);
    ///   builder.HasIndex(x => x.Codigo).IsUnique();
    ///   builder.HasIndex(x => x.EsActiva);
    /// </remarks>
    public class ConfiguracionRetencion : EntidadBase
    {
        /// <summary>Código único de la retención. Ejemplo: "IRPF-15", "IRPF-ARR-19".</summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>Nombre descriptivo de la retención para mostrar en facturas y reportes.</summary>
        public string NombreRetencion { get; set; } = string.Empty;

        /// <summary>
        /// Porcentaje de retención que se aplica sobre la base imponible de la factura.
        /// Ejemplo: 15.00 para retención profesional general.
        /// </summary>
        public decimal PorcentajeRetencion { get; set; }

        /// <summary>
        /// Indica si la retención aplica a personas físicas (autónomos).
        /// True: solo facturas de clientes persona física.
        /// False: puede aplicar también a personas jurídicas.
        /// </summary>
        public bool AplicaAPersonaFisica { get; set; } = true;

        /// <summary>
        /// Indica si la retención aplica a personas jurídicas (sociedades).
        /// </summary>
        public bool AplicaAPersonaJuridica { get; set; } = false;

        /// <summary>
        /// Código de la retención en el sistema de la AEAT para modelos 111/115/123.
        /// Necesario para la declaración trimestral de retenciones.
        /// </summary>
        public string? CodigoHacienda { get; set; }

        /// <summary>
        /// Referencia legal que regula esta retención.
        /// Ejemplo: "Art. 95 LIRPF", "Art. 75 RIRPF".
        /// </summary>
        public string? ReferenciaLegal { get; set; }

        /// <summary>Descripción del tipo de actividad o situación que origina la retención.</summary>
        public string? Descripcion { get; set; }

        /// <summary>Indica si esta configuración de retención está vigente y aplicable.</summary>
        public bool EsActiva { get; set; } = true;
    }
}
