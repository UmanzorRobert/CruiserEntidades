using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Catalogo
{
    /// <summary>
    /// Relación entre dos productos del catálogo que define su vinculación comercial
    /// o funcional: sustitución, complementariedad, accesoriedad o inclusión en pack.
    ///
    /// Se usa para:
    /// - Proponer automáticamente un sustituto cuando el producto A no tiene stock.
    /// - Mostrar productos complementarios como venta cruzada en cotizaciones.
    /// - Gestionar kits de limpieza donde un pack incluye varios productos.
    ///
    /// EsBidireccional=true indica que la relación aplica en ambas direcciones
    /// (A→B y B→A), evitando duplicar el registro para relaciones simétricas.
    ///
    /// NO hereda de EntidadBase: es una tabla de relación con campos propios.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.HasIndex(x => new { x.ProductoId, x.ProductoSustituoId, x.TipoRelacion }).IsUnique();
    ///
    ///   Relaciones:
    ///   builder.HasOne(ps => ps.Producto).WithMany(p => p.ProductosRelacionados)
    ///          .HasForeignKey(ps => ps.ProductoId).OnDelete(DeleteBehavior.Cascade);
    ///   builder.HasOne(ps => ps.ProductoSustituto).WithMany()
    ///          .HasForeignKey(ps => ps.ProductoSustituoId).OnDelete(DeleteBehavior.Restrict);
    /// </remarks>
    public class ProductoSustituto
    {
        /// <summary>Identificador único de esta relación entre productos.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Identificador del producto origen de la relación.
        /// FK hacia Productos.
        /// </summary>
        public Guid ProductoId { get; set; }

        /// <summary>
        /// Identificador del producto relacionado (sustituto, complementario, accesorio, pack).
        /// FK hacia Productos.
        /// </summary>
        public Guid ProductoSustituoId { get; set; }

        /// <summary>
        /// Tipo de relación que vincula los dos productos.
        /// Determina cómo se presenta y usa la relación en la interfaz.
        /// </summary>
        public TipoRelacionProducto TipoRelacion { get; set; }

        /// <summary>
        /// Indica si la relación aplica en ambos sentidos.
        /// True: si A es sustituto de B, entonces B también es sustituto de A.
        /// False: la relación es unidireccional (A puede sustituir a B pero no viceversa).
        /// </summary>
        public bool EsBidireccional { get; set; } = true;

        /// <summary>
        /// Prioridad de este producto relacionado respecto a otros del mismo tipo.
        /// Menor número = mayor prioridad. Determina el orden en que se sugieren.
        /// </summary>
        public int Prioridad { get; set; } = 1;

        /// <summary>
        /// Nota o justificación de la relación.
        /// Ejemplo: "Misma fórmula, diferente presentación", "Requiere para uso seguro".
        /// </summary>
        public string? Nota { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Producto origen de la relación.</summary>
        public virtual Producto? Producto { get; set; }

        /// <summary>Producto relacionado (sustituto, complementario, accesorio).</summary>
        public virtual Producto? ProductoSustituido { get; set; }
    }
}
