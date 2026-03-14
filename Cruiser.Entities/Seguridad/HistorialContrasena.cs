using System;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Seguridad
{
    /// <summary>
    /// Historial de contraseñas anteriores de un usuario para implementar la política
    /// de no reutilización de las últimas N contraseñas (configurable en ParametroSistema,
    /// por defecto las últimas 5).
    ///
    /// Al cambiar la contraseña, el sistema verifica que el nuevo hash BCrypt no coincida
    /// con ninguno de los registros activos de este historial para el usuario.
    ///
    /// NO hereda de EntidadBase: es un registro append-only de seguridad.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(256);
    ///   builder.Property(x => x.DireccionIp).HasMaxLength(45);
    ///   builder.HasIndex(x => new { x.UsuarioId, x.FechaCambio });
    ///
    ///   Relaciones:
    ///   builder.HasOne(h => h.Usuario).WithMany(u => u.HistorialContrasenas)
    ///          .HasForeignKey(h => h.UsuarioId).OnDelete(DeleteBehavior.Cascade);
    /// </remarks>
    public class HistorialContrasena
    {
        /// <summary>Identificador único del registro de historial.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Identificador del usuario al que pertenece este historial de contraseñas.
        /// FK hacia la tabla Usuarios.
        /// </summary>
        public Guid UsuarioId { get; set; }

        /// <summary>
        /// Hash BCrypt de la contraseña anterior.
        /// Se almacena el hash, NUNCA el texto plano.
        /// Se utiliza para verificar que nuevas contraseñas no coincidan con anteriores.
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Fecha y hora UTC en que se realizó el cambio de contraseña que generó este registro.
        /// </summary>
        public DateTime FechaCambio { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Identificador del usuario que realizó el cambio de contraseña.
        /// Puede ser el propio usuario o un administrador que hizo el reset.
        /// </summary>
        public Guid? CambiadoPorId { get; set; }

        /// <summary>
        /// Tipo o motivo del cambio de contraseña que originó este registro.
        /// </summary>
        public TipoCambioContrasena TipoCambio { get; set; }

        /// <summary>
        /// Dirección IP desde la que se realizó el cambio de contraseña.
        /// Útil para auditoría de seguridad.
        /// </summary>
        public string? DireccionIp { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Usuario propietario de este historial de contraseña.</summary>
        public virtual Usuario? Usuario { get; set; }
    }
}
