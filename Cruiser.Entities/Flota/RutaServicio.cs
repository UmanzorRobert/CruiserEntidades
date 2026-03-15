using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Flota
{
    /// <summary>
    /// Planificación de la ruta diaria de un vehículo que agrupa múltiples
    /// órdenes de servicio en un itinerario optimizado.
    ///
    /// El planificador de rutas del módulo de flota permite al responsable
    /// crear rutas diarias asignando órdenes de servicio con drag-drop,
    /// optimizando la secuencia para minimizar los kilómetros recorridos
    /// (algoritmo de vecino más cercano sobre las coordenadas GPS de los clientes).
    ///
    /// Los KilometrosEstimados se calculan al crear la ruta.
    /// Los KilometrosReales se registran al finalizar la ruta desde
    /// los kilómetros de AsignacionVehiculo inicio/fin.
    ///
    /// La exportación de la ruta al móvil genera un enlace de Google Maps
    /// con todos los waypoints en orden para que el conductor la siga.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(30);
    ///   builder.Property(x => x.KilometrosEstimados).HasPrecision(10, 2);
    ///   builder.Property(x => x.KilometrosReales).HasPrecision(10, 2);
    ///   builder.HasIndex(x => new { x.Fecha, x.VehiculoId });
    ///   builder.HasIndex(x => x.Estado);
    ///   builder.HasIndex(x => x.Fecha);
    /// </remarks>
    public class RutaServicio : EntidadBase
    {
        /// <summary>
        /// Código único de la ruta. Formato: "RUT-2026-0325-01" (fecha + secuencia del día).
        /// </summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>Fecha de la ruta planificada.</summary>
        public DateOnly Fecha { get; set; }

        /// <summary>FK hacia el vehículo asignado para esta ruta.</summary>
        public Guid VehiculoId { get; set; }

        /// <summary>FK hacia el empleado conductor asignado a la ruta.</summary>
        public Guid? ConductorId { get; set; }

        /// <summary>Estado del ciclo de vida de la ruta.</summary>
        public EstadoRutaServicio EstadoRuta { get; set; } = EstadoRutaServicio.Planificada;

        // ── Métricas ─────────────────────────────────────────────────────────

        /// <summary>Kilómetros totales estimados al planificar la ruta.</summary>
        public decimal? KilometrosEstimados { get; set; }

        /// <summary>Kilómetros totales reales recorridos al finalizar la ruta.</summary>
        public decimal? KilometrosReales { get; set; }

        /// <summary>Horas programadas de duración total de la ruta.</summary>
        public decimal? HorasProgramadas { get; set; }

        /// <summary>Horas reales de duración de la ruta desde inicio hasta vuelta a base.</summary>
        public decimal? HorasReales { get; set; }

        /// <summary>Número de paradas (órdenes de servicio) incluidas en la ruta.</summary>
        public int NumeroParadas { get; set; } = 0;

        /// <summary>Hora estimada de inicio de la ruta (salida desde la base de la empresa).</summary>
        public TimeOnly? HoraSalidaEstimada { get; set; }

        /// <summary>Hora real de inicio de la ruta.</summary>
        public TimeOnly? HoraSalidaReal { get; set; }

        /// <summary>Hora estimada de regreso a la base.</summary>
        public TimeOnly? HoraRegresoEstimada { get; set; }

        /// <summary>Hora real de regreso a la base.</summary>
        public TimeOnly? HoraRegresoReal { get; set; }

        /// <summary>Notas sobre la ruta, instrucciones especiales o incidencias.</summary>
        public string? Notas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Vehículo asignado a la ruta.</summary>
        public virtual VehiculoEmpresa? Vehiculo { get; set; }

        /// <summary>Empleado conductor de la ruta.</summary>
        public virtual Comercial.Empleado? Conductor { get; set; }

        /// <summary>Paradas de la ruta con las órdenes de servicio en orden de visita.</summary>
        public virtual ICollection<OrdenServicioRuta> Paradas { get; set; }
            = new List<OrdenServicioRuta>();
    }
}
