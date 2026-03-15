using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Operaciones
{
    /// <summary>
    /// Plantilla de orden de compra recurrente a un proveedor habitual.
    /// Permite generar órdenes de compra estándar con un solo clic, manteniendo
    /// los productos y cantidades habituales sin tener que reintroducirlos cada vez.
    ///
    /// Especialmente útil para pedidos periódicos de productos de limpieza
    /// de alta rotación (lejías, desinfectantes, guantes, papel higiénico, etc.)
    /// que se repiten con la misma frecuencia y cantidades.
    ///
    /// Al "Generar desde Plantilla", el sistema crea una OrdenCompra en estado Borrador
    /// con todos los detalles de la plantilla, lista para revisar y enviar.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(200);
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(30);
    ///   builder.HasIndex(x => x.Codigo).IsUnique();
    ///   builder.HasIndex(x => new { x.ProveedorId, x.EstaActiva });
    /// </remarks>
    public class PlantillaOrdenCompra : EntidadBase
    {
        /// <summary>
        /// Código único de la plantilla en el sistema.
        /// Ejemplo: "TMPL-LEJIAS-MENSUAL", "TMPL-EPIS-TRIMESTRAL".
        /// </summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>Nombre descriptivo de la plantilla para identificarla en la interfaz.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Descripción del propósito y frecuencia de uso de la plantilla.</summary>
        public string? Descripcion { get; set; }

        /// <summary>FK hacia el proveedor al que se emiten las órdenes generadas desde esta plantilla.</summary>
        public Guid ProveedorId { get; set; }

        /// <summary>FK hacia el almacén de destino de las recepciones de órdenes generadas.</summary>
        public Guid AlmacenId { get; set; }

        /// <summary>
        /// Indica si la plantilla está activa y disponible para generar órdenes.
        /// Las plantillas inactivas se conservan por historial pero no se muestran en la UI.
        /// </summary>
        public bool EstaActiva { get; set; } = true;

        /// <summary>Notas o instrucciones especiales incluidas en las órdenes generadas.</summary>
        public string? Notas { get; set; }

        /// <summary>Fecha de la última vez que se generó una orden desde esta plantilla.</summary>
        public DateTime? FechaUltimoUso { get; set; }

        /// <summary>Número de órdenes generadas desde esta plantilla (estadístico).</summary>
        public int NumeroOrdenesGeneradas { get; set; } = 0;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Proveedor habitual de esta plantilla de compra.</summary>
        public virtual Comercial.Proveedor? Proveedor { get; set; }

        /// <summary>Líneas de productos incluidas en esta plantilla de compra.</summary>
        public virtual ICollection<DetallePlantillaOrdenCompra> Detalles { get; set; }
            = new List<DetallePlantillaOrdenCompra>();
    }
}
