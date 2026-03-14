using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Configuracion
{
    /// <summary>
    /// Catálogo de tipos impositivos aplicables en el sistema.
    /// Soporta IVA peninsular (0%, 4%, 10%, 21%), IGIC canario (0%, 3%, 7%, 15%),
    /// IPSI de Ceuta/Melilla y retenciones IRPF.
    ///
    /// Cada producto y servicio lleva referencia a un impuesto de este catálogo.
    /// Los impuestos se aplican en facturación calculando el importe base + impuesto.
    ///
    /// SEED INICIAL: IVA 21%, IVA 10%, IVA 4%, IVA 0%, IGIC 7%, IGIC 3%, IGIC 0%.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(20);
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.Porcentaje).HasPrecision(5, 2);
    ///   builder.Property(x => x.Region).HasMaxLength(100);
    ///   builder.Property(x => x.ReferenciaLegal).HasMaxLength(500);
    ///   builder.HasIndex(x => x.Codigo).IsUnique();
    /// </remarks>
    public class Impuesto : EntidadBase
    {
        /// <summary>
        /// Código único del impuesto. Ejemplos: "IVA21", "IVA10", "IVA4", "IVA0", "IGIC7".
        /// Se usa como clave de negocio en importaciones y configuraciones.
        /// </summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>Nombre descriptivo del impuesto para mostrar en la UI y documentos.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Porcentaje del impuesto. Ejemplos: 21.00, 10.00, 4.00, 0.00, 7.00.
        /// Se almacena con precisión decimal para soportar tipos fraccionarios futuros.
        /// </summary>
        public decimal Porcentaje { get; set; }

        /// <summary>
        /// Región geográfica de aplicación del impuesto.
        /// Ejemplos: "Península y Baleares", "Canarias", "Ceuta y Melilla", "General".
        /// Permite filtrar los impuestos disponibles según la dirección del cliente.
        /// </summary>
        public string? Region { get; set; }

        /// <summary>
        /// Referencia a la norma legal que regula este tipo impositivo.
        /// Ejemplo: "Art. 90 LIVA", "Art. 27 LIGIC", "Art. 101 LIRPF".
        /// </summary>
        public string? ReferenciaLegal { get; set; }

        /// <summary>
        /// Fecha de inicio de vigencia de este tipo impositivo.
        /// Permite gestionar cambios de tipos por reforma fiscal sin perder el histórico.
        /// </summary>
        public DateTime? FechaInicioVigencia { get; set; }

        /// <summary>
        /// Fecha de fin de vigencia del tipo impositivo.
        /// Nulo si el impuesto sigue en vigor. Impuestos caducados no aparecen en nuevas facturas.
        /// </summary>
        public DateTime? FechaFinVigencia { get; set; }

        /// <summary>
        /// Indica si este impuesto es de tipo retención (IRPF) en lugar de impuesto añadido.
        /// Las retenciones se descuentan del importe bruto en lugar de sumarse.
        /// </summary>
        public bool EsRetencion { get; set; } = false;

        /// <summary>
        /// Código contable del impuesto para integración con software de contabilidad.
        /// Ejemplo: "477" (IVA repercutido), "473" (IRPF retenido).
        /// </summary>
        public string? CodigoCuentaContable { get; set; }
    }
}
