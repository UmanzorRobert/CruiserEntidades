using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.DominioLimpieza
{
    /// <summary>
    /// Evaluación de calidad del servicio realizada por el cliente o por el supervisor.
    /// Incluye cuatro dimensiones de valoración independientes y la pregunta NPS.
    /// Los resultados alimentan el dashboard de calidad y los KPIs de la empresa.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasIndex(x => new { x.OrdenServicioId, x.ClienteId });
    ///   builder.Property(x => x.RespuestaEmpresa).HasMaxLength(1000);
    /// </remarks>
    public class EvaluacionServicio : EntidadBase
    {
        public Guid OrdenServicioId { get; set; }
        public Guid ClienteId { get; set; }
        public Guid? EmpleadoEvaluadoId { get; set; }

        /// <summary>Puntuación de limpieza general (1-10).</summary>
        public int PuntuacionLimpieza { get; set; }
        /// <summary>Puntuación de puntualidad del equipo (1-10).</summary>
        public int PuntuacionPuntualidad { get; set; }
        /// <summary>Puntuación de trato del personal (1-10).</summary>
        public int PuntuacionTrato { get; set; }
        /// <summary>Puntuación de calidad global del servicio (1-10).</summary>
        public int PuntuacionCalidadGlobal { get; set; }

        /// <summary>Puntuación media calculada de las cuatro dimensiones.</summary>
        public decimal PuntuacionMedia { get; set; }

        /// <summary>Comentario libre del cliente sobre el servicio.</summary>
        public string? Comentario { get; set; }

        /// <summary>Indica si el cliente recomendaría la empresa a otras personas.</summary>
        public bool? RecomendariaNosotros { get; set; }

        /// <summary>Indica si la evaluación es pública (visible en el panel de calidad del cliente).</summary>
        public bool EsPublica { get; set; } = false;

        /// <summary>Respuesta de la empresa al comentario del cliente.</summary>
        public string? RespuestaEmpresa { get; set; }

        public virtual OrdenServicio? OrdenServicio { get; set; }
    }

    
}