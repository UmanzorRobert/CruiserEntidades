using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Cruiser.Entities.Seguridad
{
    /// <summary>
    /// Entidad de rol del sistema. Extiende IdentityRole&lt;Guid&gt; de ASP.NET Core Identity
    /// con campos adicionales de negocio: nivel jerárquico, descripción y flag de rol de sistema
    /// (no eliminable ni modificable por usuarios).
    ///
    /// Los roles definen el nivel de acceso y permisos en el sistema a través de
    /// la entidad ModuloPermisoRol que cruza Rol con Modulo.
    ///
    /// NOTA: Al igual que Usuario, no puede heredar EntidadBase por incompatibilidad
    /// con IdentityRole&lt;Guid&gt;. Los campos de auditoría se incluyen manualmente.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.ToTable("Roles");
    ///   builder.Property(x => x.Descripcion).HasMaxLength(500);
    ///   builder.HasIndex(x => x.Nivel);
    ///
    ///   Roles de sistema a sembrar en el seed:
    ///   - SuperAdmin (Nivel 1): acceso total, no modificable
    ///   - Administrador (Nivel 2): acceso completo excepto configuración crítica
    ///   - Supervisor (Nivel 3): acceso a operaciones y RRHH
    ///   - Empleado (Nivel 4): acceso a módulos de campo (fichaje, órdenes)
    ///   - Cliente (Nivel 5): portal de cliente (solo lectura de sus datos)
    /// </remarks>
    public class Rol : IdentityRole<Guid>
    {
        /// <summary>
        /// Descripción detallada del rol y sus responsabilidades en el sistema.
        /// Se muestra en el panel de administración de roles y permisos.
        /// </summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Nivel jerárquico del rol. Menor número = mayor privilegio.
        /// Se utiliza para validar que un usuario solo pueda asignar roles
        /// de nivel inferior al suyo propio (evita escalada de privilegios).
        /// Ejemplo: SuperAdmin=1, Administrador=2, Supervisor=3, Empleado=4.
        /// </summary>
        public int Nivel { get; set; } = 99;

        /// <summary>
        /// Indica si este es un rol de sistema predefinido e inmutable.
        /// Los roles de sistema no pueden ser eliminados ni renombrados desde la UI.
        /// Solo se crean mediante el seed inicial de la base de datos.
        /// </summary>
        public bool EsSistema { get; set; } = false;

        /// <summary>
        /// Indica si el rol está activo. Los roles inactivos no pueden ser asignados
        /// a nuevos usuarios, aunque los usuarios que ya lo tienen conservan el acceso.
        /// </summary>
        public bool EstaActivo { get; set; } = true;

        /// <summary>
        /// Fecha y hora UTC en que se creó el rol.
        /// </summary>
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Identificador del usuario administrador que creó este rol.
        /// Nulo para roles de sistema creados en el seed inicial.
        /// </summary>
        public Guid? CreadoPorId { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>
        /// Asignaciones de usuarios a este rol con metadatos adicionales.
        /// </summary>
        public virtual ICollection<UsuarioRol> UsuarioRoles { get; set; }
            = new List<UsuarioRol>();

        /// <summary>
        /// Permisos por módulo asignados a este rol.
        /// Define qué puede ver, insertar, editar, eliminar, exportar e imprimir
        /// cada rol en cada módulo del sistema.
        /// </summary>
        public virtual ICollection<ModuloPermisoRol> PermisosModulos { get; set; }
            = new List<ModuloPermisoRol>();
    }
}
