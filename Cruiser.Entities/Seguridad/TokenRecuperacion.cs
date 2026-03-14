using System;

namespace Cruiser.Entities.Seguridad
{
    /// <summary>
    /// Token temporal generado para el proceso de recuperación de contraseña olvidada.
    /// Se envía al email del usuario como enlace seguro de un solo uso con expiración de 1 hora.
    ///
    /// Flujo completo:
    /// 1. Usuario solicita recuperación → se genera Token + se envía email con enlace.
    /// 2. Usuario hace clic en el enlace → se valida Token (no expirado, no utilizado).
    /// 3. Usuario introduce nueva contraseña → se marca Token como Utilizado.
    /// 4. Se invalidan todos los RefreshTokens activos del usuario por seguridad.
    ///
    /// NO hereda de EntidadBase: es un registro de seguridad append-only.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.Property(x => x.Token).IsRequired().HasMaxLength(256);
    ///   builder.Property(x => x.DireccionIPSolicitud).HasMaxLength(45);
    ///   builder.HasIndex(x => x.Token).IsUnique();
    ///   builder.HasIndex(x => new { x.UsuarioId, x.Utilizado });
    /// </remarks>
    public class TokenRecuperacion
    {
        /// <summary>Identificador único del token de recuperación.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Identificador del usuario que solicitó la recuperación de contraseña.
        /// FK hacia la tabla Usuarios.
        /// </summary>
        public Guid UsuarioId { get; set; }

        /// <summary>
        /// Valor del token de recuperación. Generado con RandomNumberGenerator
        /// (32 bytes aleatorios en Base64Url). Se incluye en el enlace del email.
        /// Se almacena hasheado (SHA-256) para que el valor en BD no sirva directamente.
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Fecha y hora UTC en que se generó y envió el token de recuperación.
        /// </summary>
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Fecha y hora UTC de expiración del token.
        /// Por defecto: FechaCreacion + 1 hora. Configurable en ParametroSistema.
        /// Tras expirar, el enlace del email ya no es válido y debe solicitarse uno nuevo.
        /// </summary>
        public DateTime FechaExpiracion { get; set; }

        /// <summary>
        /// Indica si el token ya fue utilizado para completar el cambio de contraseña.
        /// Un token utilizado no puede reutilizarse aunque no haya expirado.
        /// </summary>
        public bool Utilizado { get; set; } = false;

        /// <summary>
        /// Fecha y hora UTC en que se utilizó el token para cambiar la contraseña.
        /// Nulo si el token no ha sido utilizado aún.
        /// </summary>
        public DateTime? FechaUso { get; set; }

        /// <summary>
        /// Dirección IP desde la que se realizó la solicitud de recuperación.
        /// Útil para detectar solicitudes masivas de recuperación (posible ataque).
        /// </summary>
        public string? DireccionIPSolicitud { get; set; }

        /// <summary>
        /// Dirección IP desde la que se utilizó el token para el cambio efectivo.
        /// Nulo si el token no ha sido utilizado. Permite comparar con la IP de solicitud.
        /// </summary>
        public string? DireccionIPUso { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Usuario que solicitó la recuperación de contraseña.</summary>
        public virtual Usuario? Usuario { get; set; }
    }
}
