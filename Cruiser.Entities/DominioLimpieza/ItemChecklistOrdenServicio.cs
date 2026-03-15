using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.DominioLimpieza
{
    /// <summary>
    /// Respuesta del empleado a un ítem del checklist de una orden de servicio.
    /// Almacena el valor de la respuesta, la foto adjunta (si aplica) y
    /// la fecha y hora de la respuesta para trazabilidad completa.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.HasIndex(x => new { x.ChecklistOrdenServicioId, x.ItemPlantillaChecklistId }).IsUnique();
    /// </remarks>
    public class ItemChecklistOrdenServicio
    {
        /// <summary>Identificador único de la respuesta.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>FK hacia el checklist de la orden al que pertenece esta respuesta.</summary>
        public Guid ChecklistOrdenServicioId { get; set; }

        /// <summary>FK hacia el ítem de la plantilla al que corresponde esta respuesta.</summary>
        public Guid ItemPlantillaChecklistId { get; set; }

        /// <summary>
        /// Respuesta del empleado al ítem.
        /// Para SiNo: "true"/"false". Para Texto: texto libre. Para Numerico: número.
        /// Para Calificacion: "1"-"5". Para Lista: la opción seleccionada.
        /// </summary>
        public string? Respuesta { get; set; }

        /// <summary>
        /// Ruta relativa de la foto adjunta cuando TipoRespuesta = Foto.
        /// La foto se sube desde la cámara de la PWA y se sincroniza al recuperar conexión.
        /// </summary>
        public string? RutaFotoAdjunta { get; set; }

        /// <summary>Notas u observaciones adicionales del empleado sobre este ítem.</summary>
        public string? Observaciones { get; set; }

        /// <summary>Fecha y hora UTC en que el empleado respondió el ítem.</summary>
        public DateTime? FechaRespuesta { get; set; }

        /// <summary>Indica si la respuesta fue registrada en modo offline y sincronizada.</summary>
        public bool FueRespondidoOffline { get; set; } = false;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Checklist de la orden al que pertenece esta respuesta.</summary>
        public virtual ChecklistOrdenServicio? ChecklistOrdenServicio { get; set; }
    }
}
