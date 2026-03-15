using Cruiser.Entities.Base;
using Cruiser.Entities.DominioLimpieza;
using Cruiser.Entities.Enums;
using System;
using System.Collections.Generic;

namespace Cruiser.Entities.DominioLimpieza
{ 
    /// <summary>
    /// Ítem individual de una plantilla de checklist.
    /// Define la pregunta, el tipo de respuesta aceptada (SiNo, Texto, Foto, etc.)
    /// y si es obligatorio para poder completar la orden de servicio.
    ///
    /// EsObligatorio=true impide que el empleado cierre la orden si el ítem
    /// no ha sido respondido en la instancia ChecklistOrdenServicio.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.Property(x => x.Descripcion).IsRequired().HasMaxLength(500);
    ///   builder.HasIndex(x => new { x.PlantillaChecklistId, x.Orden });
    /// </remarks>
    public class ItemPlantillaChecklist
    {
        /// <summary>Identificador único del ítem de la plantilla.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>FK hacia la plantilla de checklist a la que pertenece este ítem.</summary>
        public Guid PlantillaChecklistId { get; set; }

        /// <summary>Descripción de la tarea o pregunta que debe verificar el empleado.</summary>
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>Instrucciones adicionales para el empleado sobre cómo completar el ítem.</summary>
        public string? Instrucciones { get; set; }

        /// <summary>Tipo de respuesta que acepta el ítem. Determina el control UI en la PWA.</summary>
        public TipoRespuestaChecklist TipoRespuesta { get; set; } = TipoRespuestaChecklist.SiNo;

        /// <summary>
        /// Indica si el ítem es obligatorio.
        /// True impide completar la orden si no se responde.
        /// </summary>
        public bool EsObligatorio { get; set; } = false;

        /// <summary>
        /// Orden de visualización del ítem en el checklist (drag-drop en administración).
        /// </summary>
        public int Orden { get; set; } = 0;

        /// <summary>
        /// Opciones disponibles si TipoRespuesta = Lista.
        /// Almacenadas como JSON: ["Opción A", "Opción B", "Opción C"].
        /// </summary>
        public string? OpcionesLista { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Plantilla de checklist a la que pertenece este ítem.</summary>
        public virtual PlantillaChecklist? PlantillaChecklist { get; set; }
    }
}
