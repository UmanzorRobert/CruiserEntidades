using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Flota
{
    /// <summary>
    /// Asignación de un vehículo de empresa a un empleado para una ruta o servicio específico.
    /// Permite controlar el uso diario de la flota: quién usa qué vehículo, cuándo
    /// y para qué orden de servicio o ruta.
    ///
    /// El registro de kilómetros (inicio y fin) permite calcular el kilómetro recorrido
    /// por cada servicio para el cálculo de costes de transporte y el CostoServicio.
    ///
    /// Al finalizar la asignación, los KilometrosActuales del VehiculoEmpresa
    /// se actualizan automáticamente con los kilómetros del fin de la asignación.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasIndex(x => new { x.VehiculoEmpresaId, x.FechaAsignacion });
    ///   builder.HasIndex(x => new { x.EmpleadoId, x.FechaAsignacion });
    ///   builder.HasIndex(x => x.OrdenServicioId).HasFilter("\"OrdenServicioId\" IS NOT NULL");
    ///   builder.HasIndex(x => x.RutaServicioId).HasFilter("\"RutaServicioId\" IS NOT NULL");
    /// </remarks>
    public class AsignacionVehiculo : EntidadBase
    {
        /// <summary>FK hacia el vehículo asignado para la ruta o servicio.</summary>
        public Guid VehiculoEmpresaId { get; set; }

        /// <summary>FK hacia el empleado conductor de la asignación.</summary>
        public Guid EmpleadoId { get; set; }

        /// <summary>FK hacia la orden de servicio para la que se asigna el vehículo. Nulo si es ruta.</summary>
        public Guid? OrdenServicioId { get; set; }

        /// <summary>FK hacia la ruta de servicio planificada. Nulo si es servicio individual.</summary>
        public Guid? RutaServicioId { get; set; }

        /// <summary>Fecha de la asignación del vehículo.</summary>
        public DateOnly FechaAsignacion { get; set; }

        /// <summary>Hora de inicio de uso del vehículo.</summary>
        public TimeOnly? HoraInicio { get; set; }

        /// <summary>Hora de fin de uso del vehículo (devolución al aparcamiento de empresa).</summary>
        public TimeOnly? HoraFin { get; set; }

        /// <summary>Kilómetros del contador al inicio de la asignación.</summary>
        public int? KilometrosInicio { get; set; }

        /// <summary>Kilómetros del contador al final de la asignación.</summary>
        public int? KilometrosFin { get; set; }

        /// <summary>
        /// Kilómetros recorridos en esta asignación: KilometrosFin - KilometrosInicio.
        /// Calculado automáticamente al registrar el fin de la asignación.
        /// Usado en el cálculo de CostoServicio.CostoKilometraje.
        /// </summary>
        public int? KilometrosRecorridos { get; set; }

        /// <summary>
        /// Estado de la asignación: Planificada, EnCurso, Completada, Cancelada.
        /// </summary>
        public string Estado { get; set; } = "Planificada";

        /// <summary>Notas sobre la asignación o incidencias del vehículo durante el uso.</summary>
        public string? Notas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Vehículo asignado para el uso.</summary>
        public virtual VehiculoEmpresa? VehiculoEmpresa { get; set; }

        /// <summary>Empleado conductor de la asignación.</summary>
        public virtual Comercial.Empleado? Empleado { get; set; }
    }
}
