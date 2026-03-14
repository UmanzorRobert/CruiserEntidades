using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Comercial
{
    /// <summary>
    /// Evaluación de calidad y desempeño de un proveedor realizada por un empleado
    /// de compras o por el responsable de almacén tras una recepción de mercancía.
    ///
    /// Cada evaluación puntúa cuatro dimensiones independientes con escala 1-5:
    /// calidad del producto, cumplimiento del plazo, servicio comercial e incidencias.
    /// La PuntuacionGeneral es el promedio ponderado de las cuatro dimensiones.
    ///
    /// Las AccionesCorrectivas documentan las medidas acordadas con el proveedor
    /// cuando se detectan problemas en la evaluación.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.PuntuacionCalidad).HasPrecision(3, 2);
    ///   builder.Property(x => x.PuntuacionPlazo).HasPrecision(3, 2);
    ///   builder.Property(x => x.PuntuacionServicio).HasPrecision(3, 2);
    ///   builder.Property(x => x.PuntuacionIncidencias).HasPrecision(3, 2);
    ///   builder.Property(x => x.PuntuacionGeneral).HasPrecision(3, 2);
    ///   builder.HasIndex(x => new { x.ProveedorId, x.FechaEvaluacion });
    /// </remarks>
    public class EvaluacionProveedor : EntidadBase
    {
        /// <summary>FK hacia el proveedor evaluado.</summary>
        public Guid ProveedorId { get; set; }

        /// <summary>FK hacia el empleado que realizó la evaluación.</summary>
        public Guid EvaluadoPorId { get; set; }

        /// <summary>
        /// FK hacia la orden de compra que originó esta evaluación.
        /// Nulo para evaluaciones periódicas no vinculadas a una compra concreta.
        /// </summary>
        public Guid? OrdenCompraId { get; set; }

        /// <summary>Fecha y hora UTC en que se realizó la evaluación.</summary>
        public DateTime FechaEvaluacion { get; set; } = DateTime.UtcNow;

        // ── Puntuaciones (escala 1.00 a 5.00) ───────────────────────────────

        /// <summary>
        /// Puntuación de calidad del producto recibido (1-5).
        /// 5 = excelente, 1 = inaceptable. Evalúa conformidad con especificaciones.
        /// </summary>
        public decimal PuntuacionCalidad { get; set; }

        /// <summary>
        /// Puntuación de cumplimiento del plazo de entrega acordado (1-5).
        /// 5 = entrega puntual, 1 = retraso grave con impacto operativo.
        /// </summary>
        public decimal PuntuacionPlazo { get; set; }

        /// <summary>
        /// Puntuación del servicio comercial y atención postventa (1-5).
        /// Evalúa la capacidad de respuesta, resolución de incidencias y trato.
        /// </summary>
        public decimal PuntuacionServicio { get; set; }

        /// <summary>
        /// Puntuación de gestión de incidencias y reclamaciones (1-5).
        /// 5 = ninguna incidencia o resolución excelente, 1 = incidencias graves no resueltas.
        /// </summary>
        public decimal PuntuacionIncidencias { get; set; }

        /// <summary>
        /// Puntuación general calculada como promedio ponderado de las cuatro dimensiones.
        /// Se calcula automáticamente: (Calidad×0.4 + Plazo×0.3 + Servicio×0.2 + Incidencias×0.1).
        /// </summary>
        public decimal PuntuacionGeneral { get; set; }

        /// <summary>Comentarios cualitativos sobre la evaluación y los aspectos valorados.</summary>
        public string? Comentarios { get; set; }

        /// <summary>
        /// Acciones correctivas acordadas con el proveedor como consecuencia de la evaluación.
        /// Se documenta cuando la puntuación cae por debajo del umbral mínimo aceptable.
        /// </summary>
        public string? AccionesCorrectivas { get; set; }

        /// <summary>Fecha límite para que el proveedor implemente las acciones correctivas.</summary>
        public DateOnly? FechaLimiteAccionesCorrectivas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Proveedor que ha sido evaluado.</summary>
        public virtual Proveedor? Proveedor { get; set; }
    }
}
