using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Archivos
{
    /// <summary>
    /// Archivo adjunto vinculado a un cliente: contratos firmados, documentación fiscal,
    /// autorizaciones GDPR, informes de visita, etc.
    ///
    /// El NivelConfidencialidad determina qué roles del sistema pueden acceder al archivo.
    /// Los archivos con EstaEncriptado=true se almacenan cifrados con AES-256
    /// y solo se descifran temporalmente al servir la descarga.
    ///
    /// FechaRecordatorioRenovacion permite generar alertas automáticas (AlertaVencimiento)
    /// cuando un documento del cliente está próximo a caducar.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasIndex(x => new { x.ClienteId, x.NivelConfidencialidad });
    ///   builder.HasIndex(x => x.FechaRecordatorioRenovacion)
    ///          .HasFilter("\"FechaRecordatorioRenovacion\" IS NOT NULL");
    /// </remarks>
    public class ArchivoCliente : EntidadBase
    {
        /// <summary>FK hacia el cliente al que pertenece este documento.</summary>
        public Guid ClienteId { get; set; }

        /// <summary>FK hacia el archivo adjunto con los metadatos del documento.</summary>
        public Guid ArchivoAdjuntoId { get; set; }

        /// <summary>
        /// Nivel de confidencialidad que determina qué roles pueden acceder al archivo.
        /// Por defecto: Interno (solo empleados).
        /// </summary>
        public NivelConfidencialidadArchivo NivelConfidencialidad { get; set; }
            = NivelConfidencialidadArchivo.Interno;

        /// <summary>
        /// Indica si el archivo está cifrado en el almacenamiento con AES-256.
        /// Se descifra temporalmente durante la descarga autorizada.
        /// </summary>
        public bool EstaEncriptado { get; set; } = false;

        /// <summary>
        /// Fecha en que se debe enviar un recordatorio de renovación del documento.
        /// Genera una AlertaVencimiento automática si se configura.
        /// Ejemplo: fecha de vencimiento del contrato o de la póliza de seguro.
        /// </summary>
        public DateOnly? FechaRecordatorioRenovacion { get; set; }

        /// <summary>Descripción del contenido o propósito del documento adjunto.</summary>
        public string? Descripcion { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Cliente al que pertenece este documento.</summary>
        public virtual Comercial.Cliente? Cliente { get; set; }

        /// <summary>Metadatos del archivo adjunto.</summary>
        public virtual ArchivoAdjunto? ArchivoAdjunto { get; set; }
    }
}
