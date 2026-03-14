using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.GDPR
{
    /// <summary>
    /// Registro de una firma electrónica aplicada sobre un documento del sistema.
    /// Soporta múltiples tipos de firma: canvas PWA (firma biométrica simple),
    /// certificado digital cualificado (eIDAS), y proveedores externos como DocuSign.
    ///
    /// El HashDocumento es el hash SHA-256 del contenido del documento en el momento
    /// de la firma, lo que permite verificar posteriormente que el documento no fue
    /// alterado tras ser firmado (integridad documental).
    ///
    /// Uso principal: firma del cliente al completar una Orden de Servicio (PWA en campo).
    /// También aplicable a contratos, cotizaciones y solicitudes GDPR.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.TipoDocumento).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.FirmanteNombre).IsRequired().HasMaxLength(200);
    ///   builder.Property(x => x.FirmanteEmail).HasMaxLength(256);
    ///   builder.Property(x => x.FirmanteDocumento).HasMaxLength(20);
    ///   builder.Property(x => x.HashDocumento).IsRequired().HasMaxLength(128);
    ///   builder.Property(x => x.IPFirma).HasMaxLength(45);
    ///   builder.Property(x => x.RutaImagenFirma).HasMaxLength(500);
    ///   builder.HasIndex(x => new { x.TipoDocumento, x.EntidadId });
    ///   builder.HasIndex(x => x.HashDocumento);
    /// </remarks>
    public class FirmaElectronica : EntidadBase
    {
        /// <summary>
        /// Tipo de documento que está siendo firmado.
        /// Ejemplo: "OrdenServicio", "ContratoServicio", "Cotizacion", "SolicitudGDPR".
        /// </summary>
        public string TipoDocumento { get; set; } = string.Empty;

        /// <summary>
        /// Identificador GUID del documento específico que está siendo firmado.
        /// Referencia polimórfica: puede apuntar a OrdenServicio, Contrato, etc.
        /// </summary>
        public Guid EntidadId { get; set; }

        /// <summary>
        /// Nombre completo de la persona que está firmando el documento.
        /// Se captura en el momento de la firma y se almacena de forma inmutable.
        /// </summary>
        public string FirmanteNombre { get; set; } = string.Empty;

        /// <summary>
        /// Email del firmante para envío del documento firmado como copia.
        /// También sirve como identificación adicional del firmante.
        /// </summary>
        public string? FirmanteEmail { get; set; }

        /// <summary>
        /// Número de documento de identidad del firmante (NIF, CIF, NIE, pasaporte).
        /// Se solicita opcionalmente para elevar el nivel de verificación de identidad.
        /// </summary>
        public string? FirmanteDocumento { get; set; }

        /// <summary>
        /// Hash SHA-256 del contenido del documento en el momento exacto de la firma.
        /// Permite verificar posteriormente que el documento no fue modificado post-firma.
        /// Es el corazón de la integridad documental del sistema.
        /// </summary>
        public string HashDocumento { get; set; } = string.Empty;

        /// <summary>
        /// Contenido del certificado digital utilizado si el proveedor es CertificadoDigital.
        /// En formato PEM (texto). Nulo para otros proveedores.
        /// </summary>
        public string? CertificadoDigital { get; set; }

        /// <summary>
        /// Ruta relativa de la imagen PNG de la firma capturada en canvas HTML5.
        /// Solo aplica cuando ProveedorFirma = CanvasPWA.
        /// </summary>
        public string? RutaImagenFirma { get; set; }

        /// <summary>
        /// Fecha y hora UTC exacta en que se ejecutó la firma del documento.
        /// </summary>
        public DateTime FechaFirma { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Dirección IP del dispositivo desde el que se firmó el documento.
        /// </summary>
        public string? IPFirma { get; set; }

        /// <summary>
        /// Latitud GPS del dispositivo en el momento de la firma.
        /// Capturado desde la Geolocation API del navegador/PWA con consentimiento previo.
        /// </summary>
        public decimal? LatitudFirma { get; set; }

        /// <summary>
        /// Longitud GPS del dispositivo en el momento de la firma.
        /// </summary>
        public decimal? LongitudFirma { get; set; }

        /// <summary>
        /// Indica si la firma es jurídicamente válida y puede usarse como evidencia.
        /// Puede marcarse como inválida si se detecta fraude o error en el proceso.
        /// </summary>
        public bool EsValida { get; set; } = true;

        /// <summary>
        /// Motivo por el que la firma fue invalidada. Nulo si es válida.
        /// </summary>
        public string? MotivoInvalidacion { get; set; }

        /// <summary>
        /// Proveedor o tecnología utilizada para ejecutar la firma electrónica.
        /// </summary>
        public TipoProveedorFirma ProveedorFirma { get; set; } = TipoProveedorFirma.CanvasPWA;

        /// <summary>
        /// Identificador externo de la firma en el sistema del proveedor (DocuSign, Signaturit).
        /// Permite hacer seguimiento y consultas a la API externa del proveedor.
        /// Nulo para firmas gestionadas internamente (CanvasPWA, OTP).
        /// </summary>
        public string? IdFirmaExterno { get; set; }

        /// <summary>
        /// Identificador del empleado que actuó como testigo de la firma presencial.
        /// Aplica principalmente a firmas en campo (CanvasPWA en órdenes de servicio).
        /// </summary>
        public Guid? EmpleadoTestigoId { get; set; }
    }
}
