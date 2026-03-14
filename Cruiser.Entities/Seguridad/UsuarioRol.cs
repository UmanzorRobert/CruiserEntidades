using System;
using Microsoft.AspNetCore.Identity;

namespace Cruiser.Entities.Seguridad
{
    /// <summary>
    /// Tabla de unión entre Usuario y Rol que extiende IdentityUserRole&lt;Guid&gt;
    /// con metadatos adicionales de negocio: quién asignó el rol, cuándo y
    /// con una fecha de expiración opcional para roles temporales.
    ///
    /// NO hereda de EntidadBase: es una tabla puente/junction con campos mínimos propios.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.ToTable("UsuarioRoles");
    ///   builder.HasKey(x => new { x.UserId, x.RoleId });
    ///   builder.HasIndex(x => x.FechaExpiracion).HasFilter("\"FechaExpiracion\" IS NOT NULL");
    ///
    ///   Relaciones:
    ///   builder.HasOne(ur => ur.Usuario).WithMany(u => u.UsuarioRoles).HasForeignKey(ur => ur.UserId);
    ///   builder.HasOne(ur => ur.Rol).WithMany(r => r.UsuarioRoles).HasForeignKey(ur => ur.RoleId);
    /// </remarks>
    public class UsuarioRol : IdentityUserRole<Guid>
    {
        /// <summary>
        /// Fecha y hora UTC en que se asignó el rol al usuario.
        /// </summary>
        public DateTime FechaAsignacion { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Identificador del usuario administrador que realizó la asignación del rol.
        /// Nulo si fue asignado durante el seed inicial o proceso automático.
        /// </summary>
        public Guid? AsignadoPorId { get; set; }

        /// <summary>
        /// Fecha y hora UTC de expiración opcional del rol.
        /// Nulo indica que el rol es permanente mientras no sea revocado manualmente.
        /// Útil para roles temporales (ej. acceso de auditor externo por 30 días).
        /// Hangfire verifica diariamente y revoca los roles expirados automáticamente.
        /// </summary>
        public DateTime? FechaExpiracion { get; set; }

        /// <summary>
        /// Motivo por el que se asignó este rol al usuario.
        /// Recomendado para roles de alto privilegio (SuperAdmin, Administrador).
        /// </summary>
        public string? MotivoAsignacion { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Usuario al que se le asignó el rol.</summary>
        public virtual Usuario? Usuario { get; set; }

        /// <summary>Rol que fue asignado al usuario.</summary>
        public virtual Rol? Rol { get; set; }
    }
}
