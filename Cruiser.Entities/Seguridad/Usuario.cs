using Cruiser.Entities.Base;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using static System.Collections.Specialized.BitVector32;

namespace Cruiser.Entities.Seguridad
{
    /// <summary>
    /// Entidad principal de usuario del sistema. Extiende IdentityUser&lt;Guid&gt; de
    /// ASP.NET Core Identity para integrar el sistema de autenticación estándar con
    /// campos adicionales de negocio, auditoría y soporte GDPR.
    ///
    /// NOTA IMPORTANTE sobre herencia:
    /// Esta entidad extiende IdentityUser&lt;Guid&gt; y NO puede extender EntidadBase
    /// simultáneamente (C# no admite herencia múltiple). Los campos de auditoría
    /// de EntidadBase se replican manualmente en esta clase.
    ///
    /// La autenticación soporta login por EMAIL o por NOMBRE DE USUARIO.
    /// La contraseña se hashea con BCrypt.Net antes de almacenarse.
    /// </summary>
    /// <remarks>
    /// Fluent API (en ApplicationDbContext):
    ///   builder.ToTable("Usuarios");  // renombrar la tabla de Identity
    ///   builder.Property(x => x.NombreCompleto).HasMaxLength(200);
    ///   builder.Property(x => x.FotoPerfil).HasMaxLength(500);
    ///   builder.Property(x => x.TokenAnonimizacion).HasMaxLength(100);
    ///   builder.HasIndex(x => x.TokenAnonimizacion).IsUnique().HasFilter("\"TokenAnonimizacion\" IS NOT NULL");
    ///
    ///   Relaciones:
    ///   builder.HasMany(u => u.Roles).WithMany().UsingEntity&lt;UsuarioRol&gt;();
    ///   builder.HasOne(u => u.Preferencias).WithOne(p => p.Usuario).HasForeignKey&lt;PreferenciaUsuario&gt;(p => p.UsuarioId);
    ///   builder.HasMany(u => u.RefreshTokens).WithOne(r => r.Usuario).HasForeignKey(r => r.UsuarioId);
    ///   builder.HasMany(u => u.Sesiones).WithOne(s => s.Usuario).HasForeignKey(s => s.UsuarioId);
    /// </remarks>
    public class Usuario : IdentityUser<Guid>
    {
        // ── Datos personales ─────────────────────────────────────────────────

        /// <summary>
        /// Nombre completo del usuario (Nombre + Apellidos).
        /// Se utiliza para mostrar en la interfaz y en documentos generados.
        /// </summary>
        public string NombreCompleto { get; set; } = string.Empty;

        /// <summary>
        /// Ruta relativa o URL de la foto de perfil del usuario.
        /// Almacena la ruta dentro del sistema de archivos del servidor
        /// o una URL de servicio externo (ej. Gravatar).
        /// </summary>
        public string? FotoPerfil { get; set; }

        // ── Control de acceso y seguridad ────────────────────────────────────

        /// <summary>
        /// Indica si la cuenta del usuario está actualmente bloqueada.
        /// El bloqueo puede ser automático (por exceso de intentos fallidos)
        /// o manual (por un administrador).
        /// Complementa LockoutEnabled de Identity con semántica de negocio explícita.
        /// </summary>
        public bool CuentaBloqueada { get; set; } = false;

        /// <summary>
        /// Número de intentos de login fallidos consecutivos desde el último login exitoso.
        /// Se reinicia a 0 tras un login exitoso o un desbloqueo de cuenta.
        /// Complementa AccessFailedCount de Identity con lógica de negocio adicional.
        /// </summary>
        public int IntentosFallidos { get; set; } = 0;

        /// <summary>
        /// Fecha y hora UTC del último acceso exitoso al sistema.
        /// Se actualiza en cada login correcto. Útil para detectar cuentas inactivas.
        /// </summary>
        public DateTime? UltimoAcceso { get; set; }

        /// <summary>
        /// Indica si el usuario debe cambiar su contraseña en el próximo inicio de sesión.
        /// Se activa al crear la cuenta con contraseña provisional o al hacer reset por admin.
        /// </summary>
        public bool RequiereCambioContrasena { get; set; } = false;

        /// <summary>
        /// Fecha en que expira la contraseña actual del usuario.
        /// Nulo si la contraseña no tiene fecha de expiración configurada.
        /// Controlado por la política de contraseñas en ParametroSistema.
        /// </summary>
        public DateTime? FechaExpiracionContrasena { get; set; }

        // ── Auditoría (replicados de EntidadBase por incompatibilidad con IdentityUser) ──

        /// <summary>
        /// Fecha y hora UTC en que se creó la cuenta del usuario.
        /// </summary>
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Fecha y hora UTC de la última modificación del perfil del usuario.
        /// </summary>
        public DateTime? FechaModificacion { get; set; }

        /// <summary>
        /// Identificador del usuario (administrador) que creó esta cuenta.
        /// Nulo si la cuenta fue creada mediante registro propio o seed inicial.
        /// </summary>
        public Guid? UsuarioCreacionId { get; set; }

        /// <summary>
        /// Identificador del usuario que realizó la última modificación en esta cuenta.
        /// </summary>
        public Guid? UsuarioModificacionId { get; set; }

        /// <summary>
        /// Indica si la cuenta de usuario está activa en el sistema.
        /// False equivale a soft-delete: la cuenta existe pero no puede autenticarse.
        /// Diferente de CuentaBloqueada: una cuenta inactiva no puede ser desbloqueada.
        /// </summary>
        public bool EstaActivo { get; set; } = true;

        // ── GDPR ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Indica si los datos personales del usuario han sido anonimizados
        /// en cumplimiento del Art. 17 del GDPR (Derecho al olvido).
        /// Un usuario anonimizado no puede autenticarse en el sistema.
        /// </summary>
        public bool EstaAnonimizado { get; set; } = false;

        /// <summary>
        /// Fecha y hora UTC en que se ejecutó la anonimización GDPR del usuario.
        /// </summary>
        public DateTime? FechaAnonimizacion { get; set; }

        /// <summary>
        /// Token único generado durante la anonimización GDPR.
        /// Los campos anonimizados adoptan el formato "GDPR_DEL_{TokenAnonimizacion}".
        /// Sirve como comprobante legal auditable de la operación.
        /// </summary>
        public string? TokenAnonimizacion { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Preferencias personalizadas del usuario (tema, idioma, notificaciones).</summary>
        public virtual PreferenciaUsuario? Preferencias { get; set; }

        /// <summary>Configuración de autenticación de dos factores del usuario.</summary>
        public virtual ConfiguracionDosFactores? ConfiguracionMFA { get; set; }

        /// <summary>Historial de contraseñas anteriores para evitar reutilización.</summary>
        public virtual ICollection<HistorialContrasena> HistorialContrasenas { get; set; }
            = new List<HistorialContrasena>();

        /// <summary>Refresh tokens activos e históricos del usuario.</summary>
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
            = new List<RefreshToken>();

        /// <summary>Sesiones activas e históricas del usuario.</summary>
        public virtual ICollection<Sesion> Sesiones { get; set; }
            = new List<Sesion>();

        /// <summary>Dispositivos marcados como confiables por el usuario.</summary>
        public virtual ICollection<DispositivoConfiable> DispositivosConfiables { get; set; }
            = new List<DispositivoConfiable>();

        /// <summary>Tokens de recuperación de contraseña generados para el usuario.</summary>
        public virtual ICollection<TokenRecuperacion> TokensRecuperacion { get; set; }
            = new List<TokenRecuperacion>();

        /// <summary>Intentos de bloqueo de cuenta registrados para el usuario.</summary>
        public virtual ICollection<IntentoBloqueoCuenta> IntentosBloqueoCuenta { get; set; }
            = new List<IntentoBloqueoCuenta>();

        /// <summary>Asignaciones de roles del usuario con metadatos adicionales.</summary>
        public virtual ICollection<UsuarioRol> UsuarioRoles { get; set; }
            = new List<UsuarioRol>();
    }
}
