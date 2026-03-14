using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Catalogo
{
    /// <summary>
    /// Definición de un atributo diferenciador del catálogo de productos.
    /// Los atributos permiten distinguir variantes de un mismo producto base
    /// (talla, color, capacidad, concentración, etc.).
    ///
    /// Cada AtributoProducto define el tipo y comportamiento del atributo.
    /// Los valores concretos de cada producto se almacenan en ValorAtributoProducto.
    ///
    /// Los atributos con EsFiltrable=true aparecen como filtros en el listado
    /// de productos de la interfaz de administración y en el portal de cliente.
    ///
    /// SEED: Color, Talla, Capacidad, Material, Concentración, Presentación.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(50);
    ///   builder.HasIndex(x => x.Codigo).IsUnique();
    ///   builder.HasIndex(x => new { x.TipoAtributo, x.EsFiltrable });
    /// </remarks>
    public class AtributoProducto : EntidadBase
    {
        /// <summary>
        /// Código único del atributo en formato SCREAMING_SNAKE_CASE.
        /// Ejemplo: "COLOR", "TALLA", "CAPACIDAD_ML", "CONCENTRACION".
        /// </summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>
        /// Nombre descriptivo del atributo para mostrar en formularios y filtros.
        /// Ejemplo: "Color", "Talla", "Capacidad", "Concentración", "Presentación".
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Tipo de atributo que determina el widget de entrada y la validación.
        /// </summary>
        public TipoAtributoProducto TipoAtributo { get; set; }

        /// <summary>
        /// Indica si este atributo aparece como filtro en el listado de productos.
        /// Los atributos filtrables se muestran en el panel lateral de filtros de la UI.
        /// </summary>
        public bool EsFiltrable { get; set; } = false;

        /// <summary>
        /// Posición de ordenación del atributo en la ficha de producto y en los filtros.
        /// Los atributos con menor Orden se muestran primero.
        /// </summary>
        public int Orden { get; set; } = 0;

        /// <summary>
        /// Unidad de medida del atributo cuando TipoAtributo = Capacidad.
        /// Ejemplo: "ml", "L", "kg", "g". Nulo para atributos sin unidad (Color, Talla).
        /// </summary>
        public string? UnidadMedida { get; set; }

        /// <summary>
        /// Descripción del atributo y sus valores válidos.
        /// Se muestra como ayuda contextual al asignar valores en la ficha de producto.
        /// </summary>
        public string? Descripcion { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Valores concretos de este atributo asignados a productos.</summary>
        public virtual ICollection<ValorAtributoProducto> Valores { get; set; }
            = new List<ValorAtributoProducto>();
    }
}
