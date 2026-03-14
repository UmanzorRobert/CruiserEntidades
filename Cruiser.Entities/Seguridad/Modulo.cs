using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Seguridad
{
    /// <summary>
    /// Módulo del sistema que representa un nodo del árbol jerárquico de navegación.
    /// Cada módulo puede tener un módulo padre (para submenús) y múltiples módulos hijos.
    ///
    /// Los módulos se usan para:
    /// 1. Construir dinámicamente el sidebar de navegación según los permisos del usuario.
    /// 2. Controlar el acceso a rutas mediante ModuloPermisoRol.
    /// 3. Registrar en Bitácora a qué módulo corresponde cada acción.
    ///
    /// El árbol se siembra completamente en el seed inicial (FASE 20.10).
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(50);
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.Ruta).HasMaxLength(200);
    ///   builder.Property(x => x.Icono).HasMaxLength(100);
    ///   builder.HasIndex(x => x.Codigo).IsUnique();
    ///   builder.HasIndex(x => new { x.ModuloPadreId, x.Orden });
    ///
    ///   Auto-relación jerárquica:
    ///   builder.HasOne(m => m.ModuloPadre).WithMany(m => m.ModulosHijos)
    ///          .HasForeignKey(m => m.ModuloPadreId).OnDelete(DeleteBehavior.Restrict);
    /// </remarks>
    public class Modulo : EntidadBase
    {
        /// <summary>
        /// Identificador del módulo padre en la jerarquía de navegación.
        /// Nulo si es un módulo raíz (elemento de primer nivel en el sidebar).
        /// </summary>
        public Guid? ModuloPadreId { get; set; }

        /// <summary>
        /// Código único del módulo en formato SCREAMING_SNAKE_CASE.
        /// Ejemplo: "DASHBOARD", "INVENTARIO_PRODUCTOS", "FACTURACION_EMITIR".
        /// Se usa en código para referencias seguras sin depender del nombre.
        /// </summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>
        /// Nombre legible del módulo que se muestra en el sidebar y breadcrumbs.
        /// Debe ser conciso y descriptivo. Soporta localización mediante i18n.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Ruta relativa del módulo en la aplicación web.
        /// Ejemplo: "/dashboard", "/inventario/productos", "/facturacion/emitir".
        /// Nulo para módulos que son solo agrupadores de submenú (sin ruta propia).
        /// </summary>
        public string? Ruta { get; set; }

        /// <summary>
        /// Clase CSS del icono de Font Awesome o Bootstrap Icons para el módulo.
        /// Ejemplo: "fas fa-tachometer-alt", "fas fa-boxes", "fas fa-file-invoice".
        /// Se muestra junto al nombre en el sidebar.
        /// </summary>
        public string? Icono { get; set; }

        /// <summary>
        /// Área funcional a la que pertenece el módulo.
        /// Se usa para agrupar módulos en secciones del sidebar con separadores.
        /// </summary>
        public AreaModulo Area { get; set; }

        /// <summary>
        /// Posición de ordenación del módulo dentro de su nivel jerárquico.
        /// Los módulos con menor Orden aparecen primero en el sidebar.
        /// </summary>
        public int Orden { get; set; } = 0;

        /// <summary>
        /// Indica si el módulo es visible en el sidebar de navegación.
        /// Los módulos invisibles siguen siendo accesibles por ruta directa
        /// pero no aparecen en el menú. Útil para páginas de detalle.
        /// </summary>
        public bool EsVisible { get; set; } = true;

        /// <summary>
        /// Indica si el módulo requiere autenticación para ser accedido.
        /// Solo los módulos públicos (login, recuperar contraseña) tienen esto en false.
        /// </summary>
        public bool RequiereAutenticacion { get; set; } = true;

        /// <summary>
        /// Descripción del módulo para el panel de administración de permisos.
        /// Ayuda al administrador a entender qué hace cada módulo al asignar permisos.
        /// </summary>
        public string? Descripcion { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Módulo padre en la jerarquía de navegación. Nulo si es módulo raíz.</summary>
        public virtual Modulo? ModuloPadre { get; set; }

        /// <summary>Módulos hijos (submenú) de este módulo.</summary>
        public virtual ICollection<Modulo> ModulosHijos { get; set; }
            = new List<Modulo>();

        /// <summary>Permisos de roles asignados a este módulo.</summary>
        public virtual ICollection<ModuloPermisoRol> PermisosRoles { get; set; }
            = new List<ModuloPermisoRol>();
    }
}
