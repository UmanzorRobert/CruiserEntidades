using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Comercial
{
    /// <summary>
    /// Entidad central del módulo comercial. Representa a una persona física o jurídica
    /// que contrata o ha contratado servicios de limpieza con la empresa.
    ///
    /// Almacena datos fiscales, contacto, historial de facturación acumulado,
    /// segmentación comercial RFM y soporte completo para anonimización GDPR.
    ///
    /// Los datos personales (Nombre, Email, Telefono, NIF) quedan anonimizados
    /// al ejecutar una solicitud GDPR tipo Anonimizacion, pero los registros
    /// de Factura y ContratoServicio se mantienen intactos por obligación fiscal.
    ///
    /// Las estadísticas acumuladas (TotalFacturado, NumeroContratos) se actualizan
    /// mediante jobs de Hangfire y son usadas por el servicio RFM de segmentación.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.NombreComercial).IsRequired().HasMaxLength(200);
    ///   builder.Property(x => x.NIF).HasMaxLength(20);
    ///   builder.Property(x => x.Email).HasMaxLength(256);
    ///   builder.Property(x => x.Telefono).HasMaxLength(20);
    ///   builder.Property(x => x.TotalFacturado).HasPrecision(18, 2);
    ///   builder.HasIndex(x => x.NIF).HasFilter("\"NIF\" IS NOT NULL");
    ///   builder.HasIndex(x => x.Email).HasFilter("\"Email\" IS NOT NULL");
    ///   builder.HasIndex(x => new { x.TipoClienteId, x.EstaActivo });
    ///   builder.HasIndex(x => x.TokenAnonimizacion).HasFilter("\"TokenAnonimizacion\" IS NOT NULL");
    /// </remarks>
    public class Cliente : EntidadBase
    {
        // ── Identificación fiscal ────────────────────────────────────────────

        /// <summary>
        /// Nombre completo (persona física) o razón social (persona jurídica) del cliente.
        /// Se anonimiza a "GDPR_DEL_{token}" cuando se procesa una solicitud de anonimización.
        /// </summary>
        public string NombreComercial { get; set; } = string.Empty;

        /// <summary>
        /// Nombre comercial o nombre de la empresa (si difiere de la razón social).
        /// Usado en documentos y comunicaciones de marketing.
        /// </summary>
        public string? NombreFantasia { get; set; }

        /// <summary>
        /// NIF, CIF, NIE o número de identificación fiscal del cliente.
        /// Se anonimiza en operaciones GDPR. Validado con FluentValidation.
        /// </summary>
        public string? NIF { get; set; }

        /// <summary>
        /// Tipo de documento de identidad del NIF almacenado.
        /// FK hacia TipoDocumento del catálogo de configuración.
        /// </summary>
        public Guid? TipoDocumentoId { get; set; }

        /// <summary>
        /// FK hacia el tipo de cliente que define las condiciones comerciales por defecto.
        /// </summary>
        public Guid TipoClienteId { get; set; }

        // ── Datos de contacto ────────────────────────────────────────────────

        /// <summary>
        /// Email principal del cliente para comunicaciones y facturación electrónica.
        /// Se anonimiza a "gdpr_{token}@noreply.gdpr" en operaciones GDPR.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>Email secundario o de contacto alternativo.</summary>
        public string? EmailAlternativo { get; set; }

        /// <summary>
        /// Teléfono principal del cliente.
        /// Se anonimiza a "000-GDPR-{token}" en operaciones GDPR.
        /// </summary>
        public string? Telefono { get; set; }

        /// <summary>Teléfono móvil del cliente para contacto urgente y SMS.</summary>
        public string? TelefonoMovil { get; set; }

        /// <summary>Sitio web del cliente (para clientes empresa).</summary>
        public string? SitioWeb { get; set; }

        // ── Datos de facturación ─────────────────────────────────────────────

        /// <summary>
        /// Días de plazo de pago pactados con este cliente específico.
        /// Sobreescribe el valor por defecto del TipoCliente.
        /// </summary>
        public int DiasPlazoPago { get; set; } = 30;

        /// <summary>
        /// Límite de crédito personalizado para este cliente.
        /// Sobreescribe el valor por defecto del TipoCliente.
        /// </summary>
        public decimal? LimiteCredito { get; set; }

        /// <summary>
        /// Porcentaje de descuento comercial pactado con este cliente específico.
        /// Sobreescribe el descuento por defecto del TipoCliente.
        /// </summary>
        public decimal? PorcentajeDescuento { get; set; }

        /// <summary>FK hacia el impuesto aplicable al cliente según su ubicación geográfica.</summary>
        public Guid? ImpuestoId { get; set; }

        /// <summary>Número de cuenta bancaria IBAN del cliente para domiciliación de recibos.</summary>
        public string? IBAN { get; set; }

        /// <summary>Código BIC/SWIFT del banco del cliente.</summary>
        public string? BIC { get; set; }

        // ── Estadísticas acumuladas (actualizadas por Hangfire) ──────────────

        /// <summary>
        /// Total facturado acumulado al cliente desde su alta en el sistema.
        /// Actualizado periódicamente por el job de Hangfire CalcularKPIs.
        /// </summary>
        public decimal TotalFacturado { get; set; } = 0m;

        /// <summary>Número de contratos de servicio activos e históricos del cliente.</summary>
        public int NumeroContratos { get; set; } = 0;

        /// <summary>Fecha del primer contrato o primera factura del cliente.</summary>
        public DateTime? FechaPrimeraContratacion { get; set; }

        /// <summary>Fecha de la última factura emitida al cliente.</summary>
        public DateTime? FechaUltimaFactura { get; set; }

        /// <summary>Número total de órdenes de servicio completadas para este cliente.</summary>
        public int TotalOrdenesServicio { get; set; } = 0;

        // ── GDPR ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Indica si el cliente ha otorgado consentimiento de marketing.
        /// Se actualiza mediante ConsentimientoGDPR. Nunca se modifica directamente.
        /// </summary>
        public bool ConsentimientoMarketing { get; set; } = false;

        /// <summary>Indica si el cliente ha otorgado consentimiento de geolocalización GPS.</summary>
        public bool ConsentimientoGPS { get; set; } = false;

        /// <summary>Fecha de la última verificación/actualización de consentimientos GDPR.</summary>
        public DateTime? FechaUltimaVerificacionGDPR { get; set; }

        // ── Notas y observaciones ────────────────────────────────────────────

        /// <summary>
        /// Notas internas sobre el cliente visibles solo para el equipo comercial.
        /// No se muestran en documentos ni al cliente.
        /// </summary>
        public string? NotasInternas { get; set; }

        /// <summary>
        /// Instrucciones especiales de servicio aplicables a todas las órdenes de este cliente.
        /// Se muestran automáticamente al programar una nueva orden de servicio.
        /// </summary>
        public string? InstruccionesServicio { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Tipo de cliente que define las condiciones comerciales por defecto.</summary>
        public virtual TipoCliente? TipoCliente { get; set; }

        /// <summary>Segmentación RFM actual del cliente.</summary>
        public virtual SegmentacionCliente? Segmentacion { get; set; }

        /// <summary>Contactos adicionales del cliente (contacto de facturación, técnico, etc.).</summary>
        public virtual ICollection<ContactoAdicional> ContactosAdicionales { get; set; }
            = new List<ContactoAdicional>();

        /// <summary>Historial de interacciones comerciales con el cliente.</summary>
        public virtual ICollection<InteraccionComercial> Interacciones { get; set; }
            = new List<InteraccionComercial>();
    }
}
