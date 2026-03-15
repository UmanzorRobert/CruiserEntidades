using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Facturacion
{
    /// <summary>
    /// Catálogo de métodos de pago disponibles para cobrar facturas.
    /// Define las características de cada método: si es inmediato, si cobra comisión
    /// y los datos de la pasarela de pago online si aplica.
    ///
    /// SEED: Transferencia, Domiciliación SEPA, Tarjeta, Efectivo, Cheque.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(30);
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.PorcentajeComision).HasPrecision(5, 4);
    ///   builder.Property(x => x.UrlPasarela).HasMaxLength(500);
    ///   builder.Property(x => x.ApiKey).HasMaxLength(500);
    ///   builder.HasIndex(x => x.Codigo).IsUnique();
    ///   builder.HasIndex(x => x.EsPorDefecto).HasFilter("\"EsPorDefecto\" = true");
    /// </remarks>
    public class MetodoPago : EntidadBase
    {
        /// <summary>Código único del método de pago. Ejemplo: "TRANSF", "SEPA", "TARJETA".</summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>Nombre descriptivo del método de pago para la interfaz y documentos.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Tipo de método de pago que determina el flujo de conciliación.</summary>
        public MetodoPagoTipo Tipo { get; set; }

        /// <summary>
        /// Indica si el pago se confirma de forma inmediata (Efectivo, Tarjeta).
        /// False para transferencias y domiciliaciones (confirmación diferida).
        /// </summary>
        public bool EsInmediato { get; set; } = false;

        /// <summary>
        /// Porcentaje de comisión que cobra la entidad bancaria o la pasarela de pago.
        /// 0 para métodos sin comisión (efectivo, transferencia). Ej: 1.5% para tarjeta.
        /// </summary>
        public decimal PorcentajeComision { get; set; } = 0m;

        /// <summary>
        /// URL de la pasarela de pago online (Stripe, Redsys, PayPal).
        /// Solo para tipo PasarelaOnline. Los pagos se redirigen a esta URL.
        /// </summary>
        public string? UrlPasarela { get; set; }

        /// <summary>
        /// API Key de la pasarela de pago almacenada cifrada.
        /// Solo para tipo PasarelaOnline.
        /// </summary>
        public string? ApiKey { get; set; }

        /// <summary>
        /// Indica si este es el método de pago por defecto para nuevas facturas.
        /// El método por defecto se selecciona automáticamente al registrar un cobro.
        /// </summary>
        public bool EsPorDefecto { get; set; } = false;

        /// <summary>Descripción del método de pago y sus condiciones.</summary>
        public string? Descripcion { get; set; }

        /// <summary>Clase CSS del icono Font Awesome para el método de pago en la UI.</summary>
        public string? Icono { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Pagos de facturas realizados con este método de pago.</summary>
        public virtual ICollection<PagoFactura> Pagos { get; set; }
            = new List<PagoFactura>();
    }
}
