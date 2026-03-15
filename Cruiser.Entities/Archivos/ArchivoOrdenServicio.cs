using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Archivos
{
    /// <summary>
    /// Archivo adjunto vinculado a una orden de servicio.
    /// Incluye: actas de servicio firmadas, informes de inspección, albaranes,
    /// fotografías adicionales y cualquier documento generado durante la orden.
    ///
    /// EsVisibleParaCliente determina si el archivo aparece en el portal del cliente
    /// o es solo para uso interno del equipo de operaciones.
    ///
    /// Los archivos subidos desde la PWA en campo (offline) se almacenan temporalmente
    /// en IndexedDB del dispositivo y se sincronizan con el servidor al recuperar
    /// la conexión mediante ISincronizacionOfflineService.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.TipoDocumento).HasMaxLength(100);
    ///   builder.HasIndex(x => new { x.OrdenServicioId, x.EsVisibleParaCliente });
    ///   builder.HasIndex(x => x.SubidoPorId);
    /// </remarks>
    public class ArchivoOrdenServicio : EntidadBase
    {
        /// <summary>FK hacia la orden de servicio a la que pertenece este documento.</summary>
        public Guid OrdenServicioId { get; set; }

        /// <summary>FK hacia el archivo adjunto con los metadatos del documento.</summary>
        public Guid ArchivoAdjuntoId { get; set; }

        /// <summary>FK hacia el empleado que subió el archivo (desde la PWA o la web).</summary>
        public Guid SubidoPorId { get; set; }

        /// <summary>
        /// Tipo o categoría del documento vinculado a la orden.
        /// Ejemplo: "ActaServicio", "InformeInspeccion", "Albarana", "FotoIncidencia".
        /// </summary>
        public string? TipoDocumento { get; set; }

        /// <summary>
        /// Indica si el archivo es visible para el cliente en su portal o en los reportes que recibe.
        /// False para documentos internos del equipo de operaciones.
        /// True para actas de servicio, albaranes e informes de calidad destinados al cliente.
        /// </summary>
        public bool EsVisibleParaCliente { get; set; } = false;

        /// <summary>Descripción del contenido o contexto del archivo adjunto.</summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Indica si el archivo fue subido en modo offline desde la PWA y sincronizado
        /// posteriormente. Solo informativo para trazabilidad de la sincronización.
        /// </summary>
        public bool FueSubidoOffline { get; set; } = false;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Metadatos del archivo adjunto.</summary>
        public virtual ArchivoAdjunto? ArchivoAdjunto { get; set; }
    }
}
