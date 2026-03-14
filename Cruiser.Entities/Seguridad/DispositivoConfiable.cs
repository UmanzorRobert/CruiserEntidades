using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Seguridad
{
    /// <summary>
    /// Dispositivo marcado como confiable por un usuario tras verificación exitosa con MFA.
    /// Los dispositivos confiables pueden omitir el segundo factor de autenticación
    /// durante un período configurable (por defecto 30 días).
    ///
    /// La "huella" del dispositivo se calcula combinando User-Agent + características
    /// del navegador para identificar de forma pseudoanónima el dispositivo.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.HuellaDispositivo).IsRequired().HasMaxLength(256);
    ///   builder.Property(x => x.NombreDispositivo).HasMaxLength(200);
    ///   builder.Property(x => x.DireccionIPRegistro).HasMaxLength(45);
    ///   builder.Property(x => x.TokenVerificacion).HasMaxLength(256);
    ///   builder.HasIndex(x => new { x.UsuarioId, x.HuellaDispositivo }).IsUnique();
    ///   builder.HasIndex(x => x.TokenVerificacion).HasFilter("\"TokenVerificacion\" IS NOT NULL");
    /// </remarks>
    public class DispositivoConfiable : EntidadBase
    {
        /// <summary>
        /// Identificador del usuario propietario del dispositivo confiable.
        /// FK hacia la tabla Usuarios.
        /// </summary>
        public Guid UsuarioId { get; set; }

        /// <summary>
        /// Hash SHA-256 de la huella única del dispositivo.
        /// Se calcula combinando: User-Agent + Accept-Language + resolución de pantalla
        /// + zona horaria + otros atributos del navegador disponibles sin permiso.
        /// No incluye información PII directa para cumplir con GDPR.
        /// </summary>
        public string HuellaDispositivo { get; set; } = string.Empty;

        /// <summary>
        /// Nombre descriptivo del dispositivo, visible para el usuario en su panel.
        /// Generado automáticamente desde el User-Agent.
        /// Ejemplo: "Chrome 120 en Windows 11", "Firefox en Ubuntu 22.04".
        /// </summary>
        public string? NombreDispositivo { get; set; }

        /// <summary>
        /// Tipo de dispositivo identificado mediante análisis del User-Agent.
        /// </summary>
        public TipoDispositivo TipoDispositivo { get; set; } = TipoDispositivo.Desconocido;

        /// <summary>
        /// Dirección IP desde la que se registró el dispositivo como confiable.
        /// Se guarda como referencia pero NO se usa para identificar el dispositivo
        /// en accesos posteriores (las IPs pueden cambiar).
        /// </summary>
        public string? DireccionIPRegistro { get; set; }

        /// <summary>
        /// Fecha y hora UTC en que el dispositivo fue marcado como confiable.
        /// </summary>
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Fecha y hora UTC del último uso del dispositivo para iniciar sesión.
        /// Se actualiza en cada login exitoso desde este dispositivo.
        /// </summary>
        public DateTime? FechaUltimoUso { get; set; }

        /// <summary>
        /// Fecha y hora UTC de expiración del estado de confianza.
        /// Tras esta fecha, el dispositivo volverá a requerir MFA.
        /// Por defecto: FechaRegistro + 30 días (configurable en ParametroSistema).
        /// </summary>
        public DateTime FechaExpiracion { get; set; }

        /// <summary>
        /// Indica si el dispositivo sigue siendo confiable (no expirado, no revocado).
        /// </summary>
        public bool EsConfiable { get; set; } = true;

        /// <summary>
        /// Token de verificación generado al registrar el dispositivo.
        /// Se envía al email del usuario para confirmar que es él quien
        /// está marcando el dispositivo como confiable.
        /// </summary>
        public string? TokenVerificacion { get; set; }

        /// <summary>
        /// Indica si el token de verificación ya fue confirmado por el usuario.
        /// Un dispositivo no verificado no obtiene el beneficio de omitir MFA.
        /// </summary>
        public bool EsVerificado { get; set; } = false;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Usuario propietario de este dispositivo confiable.</summary>
        public virtual Usuario? Usuario { get; set; }
    }
}