using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Seguridad
{
    /// <summary>
    /// Configuración de autenticación de dos factores (MFA/2FA) para un usuario.
    /// Relación 1-a-1 con Usuario.
    ///
    /// Soporta TOTP (apps como Google Authenticator), SMS, Email y códigos de respaldo.
    /// Los códigos de respaldo se almacenan en JSONB como array de strings hasheados,
    /// permitiendo hasta 10 códigos de un solo uso para situaciones de emergencia.
    ///
    /// El campo SecretoTOTP debe almacenarse cifrado en producción usando
    /// Data Protection API de ASP.NET Core o variables de entorno de Railway.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.Property(x => x.SecretoTOTP).HasMaxLength(256);
    ///   builder.Property(x => x.CodigosRespaldo).HasColumnType("jsonb");
    ///   builder.Property(x => x.TelefonoVerificado).HasMaxLength(20);
    ///   builder.HasIndex(x => x.UsuarioId).IsUnique();
    ///
    ///   Relación:
    ///   builder.HasOne(c => c.Usuario).WithOne(u => u.ConfiguracionMFA)
    ///          .HasForeignKey&lt;ConfiguracionDosFactores&gt;(c => c.UsuarioId);
    ///
    ///   Estructura JSON de CodigosRespaldo:
    ///   ["hash_bcrypt_codigo1", "hash_bcrypt_codigo2", ...]  (máx. 10 códigos)
    /// </remarks>
    public class ConfiguracionDosFactores : EntidadBase
    {
        /// <summary>
        /// Identificador del usuario al que pertenece esta configuración MFA.
        /// FK hacia Usuarios. Índice único (1 configuración por usuario).
        /// </summary>
        public Guid UsuarioId { get; set; }

        /// <summary>
        /// Tipo de segundo factor de autenticación configurado por el usuario.
        /// </summary>
        public TipoMFA TipoMFA { get; set; }

        /// <summary>
        /// Secreto base32 para generación de códigos TOTP.
        /// Se utiliza junto con el tiempo actual para generar códigos de 6 dígitos
        /// compatibles con RFC 6238 (Google Authenticator, Authy, etc.).
        /// DEBE almacenarse cifrado. Nulo si TipoMFA != TOTP.
        /// </summary>
        public string? SecretoTOTP { get; set; }

        /// <summary>
        /// Número de teléfono verificado para envío de códigos SMS.
        /// Nulo si TipoMFA != SMS. Debe estar en formato E.164 (+34xxxxxxxxx).
        /// </summary>
        public string? TelefonoVerificado { get; set; }

        /// <summary>
        /// Códigos de respaldo de emergencia en formato JSON array.
        /// Almacenados como hashes BCrypt para que no sean legibles en BD.
        /// Cada código puede usarse una sola vez. Máximo 10 códigos.
        /// Se almacena en columna JSONB de PostgreSQL.
        /// Formato: ["$2a$11$hash1...", "$2a$11$hash2...", ...]
        /// </summary>
        public string? CodigosRespaldo { get; set; }

        /// <summary>
        /// Indica si la autenticación de dos factores está activa para este usuario.
        /// Se activa después de completar el proceso de verificación inicial.
        /// </summary>
        public bool EstaActivo { get; set; } = false;

        /// <summary>
        /// Fecha y hora UTC en que se activó el segundo factor de autenticación.
        /// Nulo si MFA nunca ha sido activado.
        /// </summary>
        public DateTime? FechaActivacion { get; set; }

        /// <summary>
        /// Fecha y hora UTC de la última vez que se utilizó MFA exitosamente.
        /// Útil para detectar cuentas con MFA inactivo durante largo tiempo.
        /// </summary>
        public DateTime? FechaUltimoUso { get; set; }

        /// <summary>
        /// Número de códigos de respaldo que ya han sido utilizados.
        /// Cuando llega a 10, todos los códigos están agotados y deben regenerarse.
        /// </summary>
        public int CodigosRespaldoUsados { get; set; } = 0;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Usuario propietario de esta configuración MFA.</summary>
        public virtual Usuario? Usuario { get; set; }
    }
}
