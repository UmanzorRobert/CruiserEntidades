using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.DominioLimpieza
{
    /// <summary>
    /// Plantilla de checklist de calidad vinculada a un tipo de servicio.
    /// Define los ítems que el empleado debe completar durante la ejecución
    /// del servicio para garantizar la calidad según los estándares de la empresa.
    ///
    /// Al asignar una orden de servicio, se instancia una copia de la plantilla
    /// como ChecklistOrdenServicio para que el empleado la complete desde la PWA.
    ///
    /// Drag-drop en la interfaz de administración permite reordenar los ítems
    /// actualizando el campo Orden de cada ItemPlantillaChecklist.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(200);
    ///   builder.HasIndex(x => new { x.TipoServicioId, x.EsActiva });
    /// </remarks>
    public class PlantillaChecklist : EntidadBase
    {
        /// <summary>FK hacia el tipo de servicio al que aplica esta plantilla.</summary>
        public Guid TipoServicioId { get; set; }

        /// <summary>Nombre descriptivo de la plantilla de checklist.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Descripción del propósito del checklist y cuándo se aplica.</summary>
        public string? Descripcion { get; set; }

        /// <summary>Indica si esta plantilla está activa y se usa para nuevas órdenes.</summary>
        public bool EsActiva { get; set; } = true;

        /// <summary>Versión de la plantilla. Se incrementa al modificar ítems obligatorios.</summary>
        public int Version { get; set; } = 1;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Tipo de servicio al que pertenece la plantilla.</summary>
        public virtual TipoServicio? TipoServicio { get; set; }

        /// <summary>Ítems que componen la plantilla.</summary>
        public virtual ICollection<ItemPlantillaChecklist> Items { get; set; }
            = new List<ItemPlantillaChecklist>();
    }
}