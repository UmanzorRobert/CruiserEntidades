using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.DominioLimpieza
{
    /// <summary>
    /// Puntuación NPS (Net Promoter Score) obtenida del cliente tras una orden de servicio.
    /// Pregunta: "¿Con qué probabilidad recomendarías nuestros servicios del 0 al 10?".
    /// 9-10: Promotor | 7-8: Pasivo | 0-6: Detractor.
    /// NPS = %Promotores - %Detractores. Objetivo: NPS > 50.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasIndex(x => new { x.ClienteId, x.FechaEncuesta });
    ///   builder.HasIndex(x => x.Categoria);
    /// </remarks>
    public class SatisfaccionNPS : EntidadBase
    {
        public Guid ClienteId { get; set; }
        public Guid OrdenServicioId { get; set; }

        /// <summary>Puntuación NPS del 0 (muy poco probable) al 10 (completamente seguro).</summary>
        public int Puntuacion { get; set; }

        /// <summary>Categoría calculada automáticamente según la puntuación: Promotor/Pasivo/Detractor.</summary>
        public string Categoria { get; set; } = "Pasivo";

        /// <summary>Comentario opcional del cliente sobre su respuesta.</summary>
        public string? Comentario { get; set; }

        /// <summary>Canal por el que se recibió la respuesta: Email, QR, Tablet, Web.</summary>
        public string? CanalEncuesta { get; set; }

        /// <summary>Fecha en que el cliente respondió la encuesta NPS.</summary>
        public DateOnly FechaEncuesta { get; set; }

        /// <summary>Indica si la encuesta fue respondida de forma anónima.</summary>
        public bool EsAnonima { get; set; } = false;

        public virtual Comercial.Cliente? Cliente { get; set; }
    }
}