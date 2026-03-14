using System;

namespace Cruiser.Entities.Seguridad
{
    /// <summary>
    /// Tabla de permisos que define qué operaciones puede realizar cada Rol en cada Módulo.
    /// Implementa una matriz de permisos Módulos × Roles con 6 tipos de operación
    /// (Ver, Insertar, Editar, Eliminar, Exportar, Imprimir) más permisos especiales en JSONB.
    ///
    /// NO hereda de EntidadBase: es una tabla de relación con campos propios.
    ///
    /// El sistema verifica estos permisos en cada request mediante un ActionFilter
    /// o middleware de autorización personalizado que consulta esta tabla (con caché).
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => new { x.ModuloId, x.RolId });
    ///   builder.Property(x => x.PermisosEspeciales).HasColumnType("jsonb");
    ///   builder.HasIndex(x => x.PermisosEspeciales).HasMethod("gin");  // índice GIN para búsquedas JSON
    ///
    ///   Relaciones:
    ///   builder.HasOne(mp => mp.Modulo).WithMany(m => m.PermisosRoles)
    ///          .HasForeignKey(mp => mp.ModuloId).OnDelete(DeleteBehavior.Cascade);
    ///   builder.HasOne(mp => mp.Rol).WithMany(r => r.PermisosModulos)
    ///          .HasForeignKey(mp => mp.RolId).OnDelete(DeleteBehavior.Cascade);
    ///
    ///   Estructura JSON de PermisosEspeciales:
    ///   { "puedeAprobarOrdenes": true, "puedeVerCostos": false, "limiteImporteAprobacion": 5000 }
    /// </remarks>
    public class ModuloPermisoRol
    {
        /// <summary>
        /// Identificador del módulo al que aplican estos permisos.
        /// Parte de la clave primaria compuesta.
        /// </summary>
        public Guid ModuloId { get; set; }

        /// <summary>
        /// Identificador del rol al que se aplican estos permisos sobre el módulo.
        /// Parte de la clave primaria compuesta.
        /// </summary>
        public Guid RolId { get; set; }

        /// <summary>
        /// Permiso para ver/listar registros en el módulo.
        /// Sin este permiso el módulo no es accesible ni visible en el sidebar.
        /// </summary>
        public bool PuedeVer { get; set; } = false;

        /// <summary>
        /// Permiso para crear nuevos registros en el módulo.
        /// Controla la visibilidad del botón "Nuevo" y el acceso al formulario de creación.
        /// </summary>
        public bool PuedeInsertar { get; set; } = false;

        /// <summary>
        /// Permiso para modificar registros existentes en el módulo.
        /// Controla la visibilidad del botón "Editar" y el acceso al formulario de edición.
        /// </summary>
        public bool PuedeEditar { get; set; } = false;

        /// <summary>
        /// Permiso para eliminar (soft-delete) registros en el módulo.
        /// Controla la visibilidad del botón "Eliminar" y la confirmación de borrado.
        /// </summary>
        public bool PuedeEliminar { get; set; } = false;

        /// <summary>
        /// Permiso para exportar datos del módulo a PDF o Excel.
        /// Controla la visibilidad de los botones de exportación en los listados.
        /// </summary>
        public bool PuedeExportar { get; set; } = false;

        /// <summary>
        /// Permiso para imprimir documentos generados en el módulo.
        /// Controla la visibilidad del botón "Imprimir" en formularios y listados.
        /// </summary>
        public bool PuedeImprimir { get; set; } = false;

        /// <summary>
        /// Permisos especiales adicionales específicos del módulo en formato JSONB.
        /// Permite definir permisos granulares que no encajan en los 6 estándar.
        /// Ejemplos: aprobar órdenes, ver costos, limite de importe de aprobación.
        /// Se almacena en columna JSONB con índice GIN para búsquedas eficientes.
        /// </summary>
        public string? PermisosEspeciales { get; set; }

        /// <summary>
        /// Identificador del administrador que configuró estos permisos.
        /// Útil para auditoría de cambios en la matriz de permisos.
        /// </summary>
        public Guid? AsignadoPorId { get; set; }

        /// <summary>
        /// Fecha y hora UTC en que se configuraron o modificaron por última vez estos permisos.
        /// </summary>
        public DateTime FechaAsignacion { get; set; } = DateTime.UtcNow;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Módulo al que aplican los permisos.</summary>
        public virtual Modulo? Modulo { get; set; }

        /// <summary>Rol al que pertenecen los permisos sobre el módulo.</summary>
        public virtual Rol? Rol { get; set; }
    }
}
