using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Operaciones
{
    /// <summary>
    /// Solicitud de aprobación de un documento en el flujo de aprobación centralizado.
    /// Gestiona las aprobaciones de órdenes de compra, facturas de proveedor, contratos,
    /// cotizaciones, gastos y otros documentos que requieren autorización interna.
    ///
    /// El flujo de aprobación soporta múltiples niveles (NivelAprobacion) para
    /// documentos que requieren aprobación escalonada (ej: nivel 1 supervisor,
    /// nivel 2 dirección, nivel 3 gerencia).
    ///
    /// La delegación (Estado = Delegado) permite que el aprobador original
    /// transfiera la responsabilidad a otro usuario durante ausencias.
    ///
    /// Las notificaciones al aprobador y al solicitante se envían automáticamente
    /// mediante ISignalRNotificationService e IEmailService al cambiar el estado.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.TipoDocumento).HasMaxLength(100);
    ///   builder.Property(x => x.MotivoRechazo).HasMaxLength(1000);
    ///   builder.Property(x => x.MotivoAprobacion).HasMaxLength(500);
    ///   builder.HasIndex(x => new { x.TipoDocumento, x.EntidadId });
    ///   builder.HasIndex(x => new { x.AprobadorId, x.Estado });
    ///   builder.HasIndex(x => x.FechaSolicitud);
    ///   builder.HasIndex(x => x.Estado);
    /// </remarks>
    public class AprobacionDocumento : EntidadBase
    {
        /// <summary>
        /// Tipo de documento que requiere aprobación.
        /// </summary>
        public TipoDocumentoAprobacion TipoDocumento { get; set; }

        /// <summary>
        /// Identificador del documento que requiere aprobación.
        /// Por ejemplo: Guid de la OrdenCompra o el ContratoServicio.
        /// </summary>
        public Guid EntidadId { get; set; }

        /// <summary>FK hacia el usuario que solicita la aprobación del documento.</summary>
        public Guid SolicitadoPorId { get; set; }

        /// <summary>FK hacia el usuario asignado como aprobador en este nivel.</summary>
        public Guid AprobadorId { get; set; }

        /// <summary>Estado actual de la solicitud de aprobación.</summary>
        public EstadoAprobacion Estado { get; set; } = EstadoAprobacion.Pendiente;

        /// <summary>
        /// Nivel de aprobación en el flujo escalonado.
        /// Nivel 1 = primer aprobador. Nivel 2 = segundo nivel (dirección). Etc.
        /// </summary>
        public int NivelAprobacion { get; set; } = 1;

        // ── Fechas ───────────────────────────────────────────────────────────

        /// <summary>Fecha y hora UTC en que se creó la solicitud de aprobación.</summary>
        public DateTime FechaSolicitud { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Fecha límite para resolver la aprobación.
        /// Pasada esta fecha, Hangfire genera una alerta de aprobación pendiente urgente.
        /// </summary>
        public DateTime? FechaLimite { get; set; }

        /// <summary>Fecha y hora UTC en que se resolvió la solicitud (aprobó/rechazó/delegó).</summary>
        public DateTime? FechaResolucion { get; set; }

        // ── Resolución ───────────────────────────────────────────────────────

        /// <summary>Motivo del rechazo cuando Estado = Rechazado. Requerido al rechazar.</summary>
        public string? MotivoRechazo { get; set; }

        /// <summary>Comentarios o condiciones del aprobador al aprobar el documento.</summary>
        public string? MotivoAprobacion { get; set; }

        /// <summary>
        /// FK hacia el usuario al que se delega la aprobación cuando Estado = Delegado.
        /// El usuario delegado recibe la notificación y asume la responsabilidad de aprobación.
        /// </summary>
        public Guid? DelegadoAId { get; set; }

        /// <summary>Motivo de la delegación de la aprobación.</summary>
        public string? MotivoDelegacion { get; set; }

        /// <summary>Descripción del documento a aprobar para facilitar la revisión del aprobador.</summary>
        public string? DescripcionDocumento { get; set; }

        /// <summary>
        /// Importe del documento que se está aprobando (para órdenes de compra, facturas, etc.).
        /// Permite al aprobador verificar rápidamente si supera su nivel de autorización.
        /// </summary>
        public decimal? ImporteDocumento { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Orden de compra vinculada si TipoDocumento = OrdenCompra.</summary>
        public virtual OrdenCompra? OrdenCompra { get; set; }
    }
}
