using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Flota
{
    /// <summary>
    /// Parada individual dentro de una ruta de servicio.
    /// Vincula una OrdenServicio con una RutaServicio y define el orden de visita,
    /// la hora estimada de llegada y los kilómetros desde la parada anterior.
    ///
    /// OrdenVisita permite reorganizar las paradas con drag-drop en el planificador
    /// de rutas. Al reordenar, se recalculan automáticamente
    /// HoraLlegadaEstimada y KilometrosDesdeAnterior para todas las paradas.
    ///
    /// HoraLlegadaReal se registra desde la PWA del conductor cuando llega
    /// a las instalaciones del cliente, generando el fichaje automático.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   NO hereda EntidadBase — tabla puente con campos adicionales.
    ///   builder.HasKey(x => x.Id);
    ///   builder.Property(x => x.KilometrosDesdeAnterior).HasPrecision(10, 2);
    ///   builder.HasIndex(x => new { x.RutaServicioId, x.OrdenVisita });
    ///   builder.HasIndex(x => x.OrdenServicioId).IsUnique();
    /// </remarks>
    public class OrdenServicioRuta
    {
        /// <summary>Identificador único del registro de parada.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>FK hacia la ruta de servicio a la que pertenece esta parada.</summary>
        public Guid RutaServicioId { get; set; }

        /// <summary>FK hacia la orden de servicio que se realizará en esta parada.</summary>
        public Guid OrdenServicioId { get; set; }

        /// <summary>
        /// Posición de esta parada en la secuencia de la ruta.
        /// 1 = primera parada, 2 = segunda, etc.
        /// Al reordenar con drag-drop se actualizan todos los valores.
        /// </summary>
        public int OrdenVisita { get; set; }

        /// <summary>
        /// Hora estimada de llegada a esta parada según la planificación de la ruta.
        /// Calculada sumando la duración estimada del servicio anterior + tiempo de desplazamiento.
        /// </summary>
        public TimeOnly? HoraLlegadaEstimada { get; set; }

        /// <summary>
        /// Hora real de llegada del conductor a las instalaciones del cliente.
        /// Registrada automáticamente desde la PWA al iniciar el fichaje de entrada.
        /// </summary>
        public TimeOnly? HoraLlegadaReal { get; set; }

        /// <summary>Hora estimada de salida tras completar el servicio en esta parada.</summary>
        public TimeOnly? HoraSalidaEstimada { get; set; }

        /// <summary>Hora real de salida registrada desde la PWA.</summary>
        public TimeOnly? HoraSalidaReal { get; set; }

        /// <summary>
        /// Kilómetros desde la parada anterior (o desde la base si es la primera parada).
        /// Calculado usando las coordenadas GPS de las instalaciones del cliente.
        /// </summary>
        public decimal? KilometrosDesdeAnterior { get; set; }

        /// <summary>Notas específicas de esta parada: instrucciones de acceso, contacto en destino.</summary>
        public string? Notas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Ruta de servicio a la que pertenece esta parada.</summary>
        public virtual RutaServicio? RutaServicio { get; set; }
    }
}
