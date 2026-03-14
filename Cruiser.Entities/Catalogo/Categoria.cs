using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Catalogo
{
    /// <summary>
    /// Categoría del catálogo de productos con soporte de árbol jerárquico ilimitado.
    /// Permite organizar los productos en una estructura de categorías y subcategorías
    /// navegable mediante jsTree en la interfaz de administración.
    ///
    /// Ejemplo de jerarquía:
    /// Productos de Limpieza → Desinfectantes → Lejías
    ///                      → Detergentes → Para suelos
    ///                                    → Para cristales
    /// EPIs → Guantes → Nitrilo
    ///               → Látex
    ///
    /// El campo Slug se usa para URLs amigables en el portal de cliente y en reportes.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(50);
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(150);
    ///   builder.Property(x => x.Slug).HasMaxLength(200);
    ///   builder.Property(x => x.Color).HasMaxLength(7);
    ///   builder.Property(x => x.Icono).HasMaxLength(100);
    ///   builder.Property(x => x.MetaDescripcion).HasMaxLength(300);
    ///   builder.HasIndex(x => x.Codigo).IsUnique();
    ///   builder.HasIndex(x => x.Slug).IsUnique().HasFilter("\"Slug\" IS NOT NULL");
    ///
    ///   Auto-relación jerárquica:
    ///   builder.HasOne(c => c.CategoriaPadre).WithMany(c => c.SubCategorias)
    ///          .HasForeignKey(c => c.CategoriaPadreId).OnDelete(DeleteBehavior.Restrict);
    /// </remarks>
    public class Categoria : EntidadBase
    {
        /// <summary>
        /// Identificador de la categoría padre en la jerarquía del catálogo.
        /// Nulo si es una categoría raíz de primer nivel.
        /// </summary>
        public Guid? CategoriaPadreId { get; set; }

        /// <summary>
        /// Código único de la categoría en formato SCREAMING_SNAKE_CASE.
        /// Ejemplo: "LIMPIEZA_DESINFECTANTES", "EPIS_GUANTES", "MAQUINARIA".
        /// Es la clave de negocio usada en el código para referencias seguras.
        /// </summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>
        /// Nombre descriptivo de la categoría para mostrar en la interfaz.
        /// Ejemplo: "Desinfectantes", "Guantes de Protección", "Maquinaria Industrial".
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Descripción detallada de la categoría y los productos que contiene.
        /// Se muestra como ayuda contextual al seleccionar la categoría en formularios.
        /// </summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Slug de la categoría para URLs amigables.
        /// Generado automáticamente desde el Nombre (minúsculas, guiones en lugar de espacios).
        /// Ejemplo: "desinfectantes", "guantes-de-proteccion".
        /// </summary>
        public string? Slug { get; set; }

        /// <summary>
        /// Color representativo de la categoría en formato hexadecimal (#RRGGBB).
        /// Se usa para el indicador visual en listados, árbol de categorías y etiquetas.
        /// Ejemplo: "#FF5733" (naranja para peligrosos), "#27AE60" (verde para eco).
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// Clase CSS del icono Font Awesome para representar visualmente la categoría.
        /// Ejemplo: "fas fa-spray-can", "fas fa-hand-paper", "fas fa-cogs".
        /// </summary>
        public string? Icono { get; set; }

        /// <summary>
        /// Meta descripción para SEO del portal de cliente y reportes.
        /// Máximo 300 caracteres con descripción concisa de la categoría.
        /// </summary>
        public string? MetaDescripcion { get; set; }

        /// <summary>
        /// Posición de ordenación dentro del nivel jerárquico.
        /// Las categorías con menor Orden aparecen primero en el árbol.
        /// </summary>
        public int Orden { get; set; } = 0;

        /// <summary>
        /// Nivel de profundidad en el árbol (1 = raíz, 2 = subcategoría, etc.).
        /// Se calcula y actualiza automáticamente al guardar.
        /// </summary>
        public int Nivel { get; set; } = 1;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Categoría padre en la jerarquía. Nulo si es categoría raíz.</summary>
        public virtual Categoria? CategoriaPadre { get; set; }

        /// <summary>Subcategorías hijas de esta categoría.</summary>
        public virtual ICollection<Categoria> SubCategorias { get; set; }
            = new List<Categoria>();

        /// <summary>Productos clasificados directamente en esta categoría.</summary>
        public virtual ICollection<Producto> Productos { get; set; }
            = new List<Producto>();
    }
}
