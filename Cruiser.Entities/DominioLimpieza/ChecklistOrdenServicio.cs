using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.DominioLimpieza
{
    /// <summary>
    /// Instancia del checklist de calidad creada para una orden de servicio específica.
    /// Es la copia de trabajo de la PlantillaChecklist que el empleado completa
    /// desde la PWA durante la ejecución del servicio.
    ///
    /// EstaCompleto=true requiere que todos los ítems obligatorios
    /// (ItemChecklistOrdenServicio con EsObligatorio=true) estén respondidos.
    ///
    /// MODO OFFLINE: el empleado completa el checklist sin conexión.
    /// Las respuestas se almacenan en IndexedDB del dispositivo y se sincronizan
    /// al recuperar la conexión mediante ColaSincronizacionOffline.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasIndex(x => x.OrdenServicioId).IsUnique();
    ///   builder.HasIndex(x => x.EstaCompleto);
    /// </remarks>
    public class ChecklistOrdenServicio : EntidadBase
    {
        /// <summary>FK hacia la orden de servicio a la que pertenece este checklist.</summary>
        public Guid OrdenServicioId { get; set; }

        /// <summary>FK hacia la plantilla de checklist de la que se instanció.</summary>
        public Guid PlantillaChecklistId { get; set; }

        /// <summary>Versión de la plantilla en el momento de instanciar el checklist.</summary>
        public int VersionPlantilla { get; set; } = 1;

        /// <summary>
        /// Indica si todos los ítems obligatorios han sido respondidos.
        /// Se actualiza automáticamente al guardar la última respuesta obligatoria.
        /// </summary>
        public bool EstaCompleto { get; set; } = false;

        /// <summary>Fecha y hora UTC en que el empleado completó el checklist.</summary>
        public DateTime? FechaCompletado { get; set; }

        /// <summary>FK hacia el empleado que completó el checklist.</summary>
        public Guid? CompletadoPorId { get; set; }

        /// <summary>Porcentaje de ítems completados: (respondidos / total) × 100.</summary>
        public decimal PorcentajeCompletado { get; set; } = 0m;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Orden de servicio a la que pertenece el checklist.</summary>
        public virtual OrdenServicio? OrdenServicio { get; set; }

        /// <summary>Respuestas a los ítems del checklist.</summary>
        public virtual ICollection<ItemChecklistOrdenServicio> Items { get; set; }
            = new List<ItemChecklistOrdenServicio>();
    }
}