using System;

namespace Cruiser.Entities.Seguridad
{
    /// <summary>
    /// Token de refresco para renovar el JWT de acceso sin requerir nuevo login.
    /// Se almacena en cookie HttpOnly+Secure en el cliente para máxima seguridad.
    ///
    /// Ciclo de vida:
    /// 1. Se genera un RefreshToken al hacer login exitoso.
    /// 2. Cuando el JWT expira (15 min), el cliente envía el RefreshToken en cookie.
    /// 3. El servidor valida el RefreshToken, lo marca como usado y emite un nuevo par JWT+RefreshToken.
    /// 4. Si el RefreshToken ya fue usado (posible robo), se invalidan TODOS los tokens de la sesión.
    ///
    /// NO hereda de EntidadBase: es un registro de seguridad append-only.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.Property(x => x.Token).IsRequired().HasMaxLength(256);
    ///   builder.Property(x => x.JwtId).IsRequired().HasMaxLength(128);
    ///   builder.Property(x => x.DireccionIP).HasMaxLength(45);
    ///   builder.Property(x => x.UserAgent).HasMaxLength(500);
    ///   builder.HasIndex(x => x.Token).IsUnique();
    ///   builder.HasIndex(x => new { x.UsuarioId, x.EsUtilizado, x.EsInvalido });
    ///   builder.HasIndex(x => x.FechaExpiracion);
    /// </remarks>
    public class RefreshToken
    {
        /// <summary>Identificador único del refresh token.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Identificador del usuario propietario de este token.
        /// FK hacia la tabla Usuarios.
        /// </summary>
        public Guid UsuarioId { get; set; }

        /// <summary>
        /// Valor del token de refresco. Generado con RandomNumberGenerator
        /// (32 bytes aleatorios codificados en Base64Url).
        /// Se almacena en texto plano ya que su seguridad depende del canal (cookie HttpOnly+Secure).
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Identificador único (claim "jti") del JWT al que este RefreshToken está vinculado.
        /// Permite invalidar el JWT asociado cuando se detecta un uso indebido del RefreshToken.
        /// </summary>
        public string JwtId { get; set; } = string.Empty;

        /// <summary>
        /// Identificador de la sesión a la que pertenece este RefreshToken.
        /// FK hacia la entidad Sesion.
        /// </summary>
        public Guid? SesionId { get; set; }

        /// <summary>
        /// Fecha y hora UTC de creación del token.
        /// </summary>
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Fecha y hora UTC de expiración del token.
        /// Por defecto 7 días desde la creación (configurable en ParametroSistema).
        /// </summary>
        public DateTime FechaExpiracion { get; set; }

        /// <summary>
        /// Indica si el token ya fue utilizado para emitir un nuevo par JWT+RefreshToken.
        /// Un RefreshToken solo puede usarse UNA vez. Uso múltiple indica posible robo.
        /// </summary>
        public bool EsUtilizado { get; set; } = false;

        /// <summary>
        /// Indica si el token ha sido invalidado manualmente (logout, revocación, robo detectado).
        /// Un token inválido no puede utilizarse aunque no haya sido utilizado ni expirado.
        /// </summary>
        public bool EsInvalido { get; set; } = false;

        /// <summary>
        /// Dirección IP desde la que se generó este token (IP del login).
        /// Se compara con la IP del request de refresco para detectar cambios sospechosos.
        /// </summary>
        public string? DireccionIP { get; set; }

        /// <summary>
        /// User-Agent del cliente al momento de generar el token.
        /// Se compara en el refresco para detectar cambios de dispositivo.
        /// </summary>
        public string? UserAgent { get; set; }

        /// <summary>
        /// Motivo de invalidación del token cuando aplica.
        /// Ejemplo: "Logout", "Robo detectado", "Revocación admin", "Todos los tokens revocados".
        /// </summary>
        public string? MotivoInvalidacion { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Usuario propietario del refresh token.</summary>
        public virtual Usuario? Usuario { get; set; }

        /// <summary>Sesión a la que pertenece este refresh token.</summary>
        public virtual Sesion? Sesion { get; set; }
    }
}
