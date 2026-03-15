using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Notificaciones
{
    /// <summary>
    /// Token de suscripción a push notifications de un dispositivo específico de un usuario.
    /// Permite enviar notificaciones nativas del navegador o SO cuando la aplicación
    /// está cerrada o en segundo plano, usando la Web Push API con VAPID.
    ///
    /// Un usuario puede tener múltiples tokens (múltiples dispositivos/navegadores).
    /// Los tokens inactivos o expirados son eliminados automáticamente por Hangfire
    /// cuando el envío falla repetidamente (error 410 Gone de la API Push).
    ///
    /// En la PWA de campo (fichaje, órdenes de servicio), el Service Worker registra
    /// automáticamente el token al instalarse en el dispositivo del empleado.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.TokenDispositivo).IsRequired().HasMaxLength(1000);
    ///   builder.Property(x => x.NombreDispositivo).HasMaxLength(200);
    ///   builder.HasIndex(x => new { x.UsuarioId, x.EsActivo });
    ///   builder.HasIndex(x => x.TokenDispositivo).IsUnique();
    ///   builder.HasIndex(x => x.FechaUltimoUso);
    /// </remarks>
    public class ConfiguracionPush : EntidadBase
    {
        /// <summary>FK hacia el usuario propietario de este token de push.</summary>
        public Guid UsuarioId { get; set; }

        /// <summary>
        /// Token de suscripción push generado por el navegador o el SO al aceptar las notificaciones.
        /// Para Web Push: JSON serializado del objeto PushSubscription (endpoint + keys).
        /// Para FCM/APNs: token de registro del dispositivo.
        /// </summary>
        public string TokenDispositivo { get; set; } = string.Empty;

        /// <summary>Plataforma del dispositivo que recibe las notificaciones push.</summary>
        public PlataformaPush PlataformaPush { get; set; } = PlataformaPush.Web;

        /// <summary>
        /// Nombre descriptivo del dispositivo registrado para identificarlo en la gestión.
        /// Ejemplo: "Chrome - Windows 11", "Safari - iPhone 15", "Firefox - Android".
        /// Detectado automáticamente mediante el User-Agent del navegador.
        /// </summary>
        public string? NombreDispositivo { get; set; }

        /// <summary>
        /// Indica si el token está activo y puede recibir notificaciones.
        /// Se desactiva automáticamente cuando la API Push devuelve error 410 (suscripción inválida).
        /// </summary>
        public bool EsActivo { get; set; } = true;

        /// <summary>Fecha y hora UTC de la última notificación push enviada exitosamente.</summary>
        public DateTime? FechaUltimoUso { get; set; }

        /// <summary>Número de errores de envío consecutivos. Al superar 3, EsActivo = false.</summary>
        public int ErroresConsecutivos { get; set; } = 0;

        // ── Navegación ───────────────────────────────────────────────────────
        // La navegación al Usuario se realiza a través de la FK UsuarioId.
        // No se añade propiedad de navegación directa para evitar referencia circular con Seguridad.
    }
}
