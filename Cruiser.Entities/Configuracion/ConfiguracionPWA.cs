using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Configuracion
{
    /// <summary>
    /// Configuración global de la Progressive Web App (PWA) del sistema.
    /// Entidad Singleton que define los parámetros de comportamiento offline,
    /// permisos requeridos, estrategias de caché del Service Worker y configuración
    /// del manifest.json generado dinámicamente desde ConfiguracionSistema.
    ///
    /// HTTPS es obligatorio para que la PWA funcione. Sin HTTPS el Service Worker
    /// no se registra y las funciones de cámara y geolocalización no están disponibles.
    /// Railway provee HTTPS automático en el dominio .railway.app.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.VersionServiceWorker).HasMaxLength(20);
    ///   builder.Property(x => x.EstrategiaCache).HasDefaultValue(EstrategiaCache.NetworkFirst);
    /// </remarks>
    public class ConfiguracionPWA : EntidadBase
    {
        /// <summary>
        /// Versión del Service Worker actualmente desplegado.
        /// Al cambiar esta versión, los clientes con el SW antiguo lo actualizan automáticamente.
        /// Formato recomendado: "v1.0.0", "v1.1.0".
        /// </summary>
        public string? VersionServiceWorker { get; set; }

        /// <summary>
        /// Indica si la PWA debe solicitar permiso de acceso a la cámara del dispositivo.
        /// Requerido para el módulo de escáner de códigos de barra y fotografías de servicio.
        /// </summary>
        public bool PermitirCamara { get; set; } = true;

        /// <summary>
        /// Indica si la PWA debe solicitar permiso de acceso a la geolocalización GPS.
        /// Requerido para el módulo de fichaje con validación de proximidad GPS.
        /// REQUIERE consentimiento GDPR previo (TipoConsentimientoGDPR.UbicacionGPS).
        /// </summary>
        public bool PermitirGeolocalizacion { get; set; } = true;

        /// <summary>
        /// Indica si la PWA debe solicitar permiso para enviar notificaciones push nativas.
        /// Las notificaciones push funcionan incluso cuando la app está cerrada.
        /// </summary>
        public bool PermitirNotificaciones { get; set; } = true;

        /// <summary>
        /// Indica si el módulo de caché offline está activo.
        /// Cuando es false, la PWA no almacena datos localmente y requiere conexión siempre.
        /// </summary>
        public bool CacheOfflineActiva { get; set; } = true;

        /// <summary>
        /// Estrategia de caché del Service Worker para las llamadas a la API.
        /// Por defecto NetworkFirst: intenta red y usa caché si no hay conexión.
        /// </summary>
        public EstrategiaCache EstrategiaCache { get; set; } = EstrategiaCache.NetworkFirst;

        /// <summary>
        /// Indica si la PWA está instalada en modo standalone (como app nativa).
        /// Se detecta mediante el media query (display-mode: standalone).
        /// </summary>
        public bool ModoInstalado { get; set; } = false;

        /// <summary>
        /// Tiempo máximo en segundos que los datos offline se consideran válidos
        /// antes de forzar sincronización con el servidor.
        /// Por defecto 86400 segundos (24 horas).
        /// </summary>
        public int TiempoMaximoCacheSegundos { get; set; } = 86400;

        /// <summary>
        /// Tamaño máximo en MB del almacenamiento IndexedDB para datos offline por dispositivo.
        /// Previene que un dispositivo ocupe demasiado espacio local. Por defecto 100 MB.
        /// </summary>
        public int TamanoMaximoCacheMB { get; set; } = 100;

        /// <summary>
        /// Clave pública VAPID para las notificaciones push del Service Worker.
        /// Se genera junto con la clave privada (almacenada en variable de entorno Railway).
        /// </summary>
        public string? VapidPublicKey { get; set; }
    }
}
