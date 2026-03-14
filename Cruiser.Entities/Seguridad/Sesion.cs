using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Seguridad
{
    /// <summary>
    /// Registro completo de cada sesión iniciada por un usuario en el sistema.
    /// Permite el tracking de dispositivos activos, cierre de sesiones remotas
    /// y análisis de patrones de acceso para detectar comportamientos anómalos.
    ///
    /// Cada login exitoso genera una nueva Sesion. El cierre de sesión (logout)
    /// o la expiración del token marca la sesión como inactiva.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Token).IsRequired().HasMaxLength(500);
    ///   builder.Property(x => x.DireccionIP).HasMaxLength(45);
    ///   builder.Property(x => x.UserAgent).HasMaxLength(500);
    ///   builder.Property(x => x.NombreDispositivo).HasMaxLength(200);
    ///   builder.HasIndex(x => x.Token).IsUnique();
    ///   builder.HasIndex(x => new { x.UsuarioId, x.EstaActiva });
    ///   builder.HasIndex(x => x.FechaExpiracion);
    /// </remarks>
    public class Sesion : EntidadBase
    {
        /// <summary>
        /// Identificador del usuario propietario de esta sesión.
        /// FK hacia la tabla Usuarios.
        /// </summary>
        public Guid UsuarioId { get; set; }

        /// <summary>
        /// Token único de identificación de la sesión.
        /// Se genera aleatoriamente con criptografía segura al iniciar sesión.
        /// Se almacena hasheado para evitar robo de sesión si se compromete la BD.
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Identificador del JWT asociado a esta sesión (campo "jti" del JWT).
        /// Permite invalidar el JWT específico sin afectar otras sesiones del usuario.
        /// </summary>
        public string? JwtId { get; set; }

        /// <summary>
        /// Fecha y hora UTC en que se inició la sesión (login exitoso).
        /// </summary>
        public DateTime FechaInicio { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Fecha y hora UTC en que se cerró la sesión (logout o expiración).
        /// Nulo si la sesión sigue activa.
        /// </summary>
        public DateTime? FechaCierre { get; set; }

        /// <summary>
        /// Fecha y hora UTC de expiración del token de sesión.
        /// Las sesiones expiradas se marcan automáticamente como inactivas
        /// mediante el job de Hangfire LimpiarSesionesExpiradas.
        /// </summary>
        public DateTime FechaExpiracion { get; set; }

        /// <summary>
        /// Indica si la sesión está actualmente activa.
        /// Se establece en false al hacer logout, al expirar o al revocar manualmente.
        /// </summary>
        public bool EstaActiva { get; set; } = true;

        /// <summary>
        /// Dirección IP del cliente desde la que se inició la sesión.
        /// Se captura al momento del login para comparar con accesos posteriores.
        /// </summary>
        public string? DireccionIP { get; set; }

        /// <summary>
        /// Cadena User-Agent completa del navegador o cliente HTTP.
        /// Permite identificar el navegador, sistema operativo y versión del cliente.
        /// </summary>
        public string? UserAgent { get; set; }

        /// <summary>
        /// Tipo de dispositivo desde el que se inició la sesión.
        /// Determinado mediante análisis del User-Agent al hacer login.
        /// </summary>
        public TipoDispositivo TipoDispositivo { get; set; } = TipoDispositivo.Desconocido;

        /// <summary>
        /// Nombre descriptivo del dispositivo identificado.
        /// Ejemplo: "Chrome 120 en Windows 11", "Safari en iPhone 15".
        /// Se muestra al usuario en la sección "Mis sesiones activas".
        /// </summary>
        public string? NombreDispositivo { get; set; }

        /// <summary>
        /// Indica si esta sesión fue iniciada desde un dispositivo marcado como confiable.
        /// Las sesiones desde dispositivos confiables pueden omitir el segundo factor MFA.
        /// </summary>
        public bool EsDispositivoConfiable { get; set; } = false;

        /// <summary>
        /// Motivo del cierre de sesión cuando aplica.
        /// Ejemplos: "Logout manual", "Expiración token", "Revocación admin", "Inactividad".
        /// </summary>
        public string? MotivoCierre { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Usuario propietario de la sesión.</summary>
        public virtual Usuario? Usuario { get; set; }

        /// <summary>Refresh token asociado a esta sesión.</summary>
        public virtual RefreshToken? RefreshToken { get; set; }
    }
}
