using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Configuracion
{
    /// <summary>
    /// Dispositivo registrado que usa la PWA del sistema.
    /// Se crea automáticamente cuando un empleado accede por primera vez
    /// al módulo de fichaje o ejecución de órdenes desde un dispositivo móvil.
    ///
    /// Permite al administrador ver qué dispositivos están activos en campo,
    /// gestionar permisos por dispositivo, monitorear el estado de sincronización
    /// offline y revocar el acceso a dispositivos comprometidos.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.TokenDispositivo).HasMaxLength(500);
    ///   builder.Property(x => x.NombreDispositivo).HasMaxLength(200);
    ///   builder.Property(x => x.VersionSW).HasMaxLength(20);
    ///   builder.HasIndex(x => x.UsuarioId);
    ///   builder.HasIndex(x => x.TokenDispositivo).IsUnique()
    ///          .HasFilter("\"TokenDispositivo\" IS NOT NULL");
    ///   builder.HasIndex(x => x.EstaEnModoOffline);
    ///
    ///   Relación con ConfiguracionPWA:
    ///   builder.HasOne(d => d.ConfiguracionPWA).WithMany()
    ///          .HasForeignKey(d => d.ConfiguracionPWAId).OnDelete(DeleteBehavior.SetNull);
    /// </remarks>
    public class DispositivoPWA : EntidadBase
    {
        /// <summary>
        /// Identificador del usuario (empleado) que usa este dispositivo.
        /// FK hacia Usuarios.
        /// </summary>
        public Guid UsuarioId { get; set; }

        /// <summary>
        /// Identificador de la configuración PWA activa aplicada a este dispositivo.
        /// FK hacia ConfiguracionPWA.
        /// </summary>
        public Guid? ConfiguracionPWAId { get; set; }

        /// <summary>
        /// Token único del dispositivo para identificarlo en el sistema de push notifications.
        /// Generado por el navegador al suscribirse a las notificaciones push.
        /// </summary>
        public string? TokenDispositivo { get; set; }

        /// <summary>Nombre descriptivo del dispositivo para mostrarlo en el panel de administración.</summary>
        public string? NombreDispositivo { get; set; }

        /// <summary>Plataforma o sistema operativo del dispositivo que usa la PWA.</summary>
        public PlataformaPWA Plataforma { get; set; } = PlataformaPWA.Web;

        /// <summary>
        /// Versión del Service Worker instalado en este dispositivo.
        /// Permite detectar dispositivos con versiones desactualizadas del SW.
        /// </summary>
        public string? VersionSW { get; set; }

        // ── Estado de permisos del dispositivo ──────────────────────────────

        /// <summary>Indica si el usuario otorgó permiso de cámara en este dispositivo.</summary>
        public bool PermisosCamaraOtorgados { get; set; } = false;

        /// <summary>Indica si el usuario otorgó permiso de geolocalización GPS en este dispositivo.</summary>
        public bool PermisosGeoOtorgados { get; set; } = false;

        /// <summary>Indica si el usuario otorgó permiso de notificaciones push en este dispositivo.</summary>
        public bool PermisosNotificacionesOtorgados { get; set; } = false;

        /// <summary>
        /// Indica si el navegador/dispositivo soporta Service Workers.
        /// Determinado en el primer acceso. Sin soporte SW no hay funcionalidad offline.
        /// </summary>
        public bool SoportaServiceWorker { get; set; } = false;

        // ── Estado de conexión y sincronización ─────────────────────────────

        /// <summary>
        /// Indica si el dispositivo está actualmente en modo offline.
        /// Actualizado por el Service Worker cuando detecta pérdida o recuperación de conexión.
        /// </summary>
        public bool EstaEnModoOffline { get; set; } = false;

        /// <summary>
        /// Fecha y hora UTC de la última sincronización exitosa de datos offline.
        /// Nulo si el dispositivo nunca ha sincronizado datos offline.
        /// </summary>
        public DateTime? UltimaSincronizacion { get; set; }

        /// <summary>
        /// Número de operaciones pendientes de sincronizar en este dispositivo.
        /// Se actualiza en cada operación offline y al sincronizar.
        /// </summary>
        public int OperacionesPendientesSincronizacion { get; set; } = 0;

        /// <summary>
        /// Fecha y hora UTC del último acceso de este dispositivo al sistema.
        /// </summary>
        public DateTime? FechaUltimoAcceso { get; set; }

        /// <summary>
        /// Indica si el acceso de este dispositivo está revocado por el administrador.
        /// Los dispositivos revocados no pueden sincronizar ni enviar datos al servidor.
        /// </summary>
        public bool EsRevocado { get; set; } = false;

        /// <summary>Motivo por el que se revocó el acceso del dispositivo.</summary>
        public string? MotivoRevocacion { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Configuración PWA aplicada a este dispositivo.</summary>
        public virtual ConfiguracionPWA? ConfiguracionPWA { get; set; }
    }
}
