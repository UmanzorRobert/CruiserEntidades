using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Notificaciones
{
    /// <summary>
    /// Registro de entrega y estado de lectura de una Notificacion para un usuario específico.
    /// Relaciona la notificación con el usuario destinatario y gestiona el estado
    /// de lectura, archivado y el estado de entrega por cada canal (sistema, email, push, SMS).
    ///
    /// La separación Notificacion / NotificacionUsuario permite que una misma
    /// notificación (por ejemplo "Stock bajo del producto X") sea entregada
    /// simultáneamente a varios usuarios (gestor de compras, responsable de almacén,
    /// director de operaciones) sin duplicar el contenido del mensaje.
    ///
    /// El badge de campana muestra el COUNT de registros con EstaLeida=false
    /// para el usuario autenticado.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasIndex(x => new { x.UsuarioId, x.EstaLeida });
    ///   builder.HasIndex(x => new { x.NotificacionId, x.UsuarioId }).IsUnique();
    ///   builder.HasIndex(x => x.FechaLectura).HasFilter("\"FechaLectura\" IS NOT NULL");
    ///   builder.HasIndex(x => x.EstaArchivada);
    /// </remarks>
    public class NotificacionUsuario : EntidadBase
    {
        /// <summary>FK hacia la notificación que se entrega a este usuario.</summary>
        public Guid NotificacionId { get; set; }

        /// <summary>FK hacia el usuario destinatario de la notificación.</summary>
        public Guid UsuarioId { get; set; }

        // ── Estado de lectura ────────────────────────────────────────────────

        /// <summary>
        /// Indica si el usuario ha leído la notificación.
        /// Se marca automáticamente al pulsar la notificación en el panel.
        /// </summary>
        public bool EstaLeida { get; set; } = false;

        /// <summary>Fecha y hora UTC en que el usuario leyó la notificación.</summary>
        public DateTime? FechaLectura { get; set; }

        /// <summary>
        /// Indica si el usuario ha archivado la notificación.
        /// Las notificaciones archivadas no aparecen en el panel principal
        /// pero sí en el historial completo de notificaciones.
        /// </summary>
        public bool EstaArchivada { get; set; } = false;

        /// <summary>Fecha y hora UTC en que el usuario archivó la notificación.</summary>
        public DateTime? FechaArchivado { get; set; }

        /// <summary>
        /// Indica si el usuario realizó la acción requerida por la notificación.
        /// Solo aplica cuando TipoNotificacion.RequiereAccion = true.
        /// </summary>
        public bool AccionRealizada { get; set; } = false;

        /// <summary>Fecha y hora UTC en que el usuario realizó la acción.</summary>
        public DateTime? FechaAccion { get; set; }

        // ── Estado de entrega por canal ──────────────────────────────────────

        /// <summary>Estado de entrega por el canal Sistema (SignalR en tiempo real).</summary>
        public EstadoEnvioNotificacion EstadoSistema { get; set; }
            = EstadoEnvioNotificacion.Pendiente;

        /// <summary>Estado de entrega por el canal Email.</summary>
        public EstadoEnvioNotificacion EstadoEmail { get; set; }
            = EstadoEnvioNotificacion.NoAplica;

        /// <summary>Estado de entrega por el canal Push (Service Worker PWA).</summary>
        public EstadoEnvioNotificacion EstadoPush { get; set; }
            = EstadoEnvioNotificacion.NoAplica;

        /// <summary>Estado de entrega por el canal SMS.</summary>
        public EstadoEnvioNotificacion EstadoSMS { get; set; }
            = EstadoEnvioNotificacion.NoAplica;

        /// <summary>Mensaje de error del último intento de envío fallido (cualquier canal).</summary>
        public string? UltimoErrorEnvio { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Notificación entregada al usuario.</summary>
        public virtual Notificacion? Notificacion { get; set; }
    }
}
