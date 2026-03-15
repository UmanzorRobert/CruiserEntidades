using System;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.DominioLimpieza
{
    /// <summary>
    /// Vinculación entre un empleado y una orden de servicio.
    /// Registra el rol del empleado, sus horas de entrada y salida reales
    /// y el fichaje vinculado para trazabilidad completa.
    ///
    /// Al completar la orden, HorasTrabajadas se usa en el cálculo de
    /// CostoServicio.CostoManoObra multiplicando por el coste/hora del empleado.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   NO hereda EntidadBase — tabla de asignación con datos operativos.
    ///   builder.HasKey(x => x.Id);
    ///   builder.HasIndex(x => new { x.OrdenServicioId, x.EmpleadoId }).IsUnique();
    ///   builder.HasIndex(x => x.EmpleadoId);
    /// </remarks>
    public class EmpleadoOrdenServicio
    {
        /// <summary>Identificador único del registro de asignación.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>FK hacia la orden de servicio.</summary>
        public Guid OrdenServicioId { get; set; }

        /// <summary>FK hacia el empleado asignado a la orden.</summary>
        public Guid EmpleadoId { get; set; }

        /// <summary>Rol del empleado en esta orden: Responsable, Auxiliar o Supervisor.</summary>
        public RolEmpleadoOrden Rol { get; set; } = RolEmpleadoOrden.Auxiliar;

        /// <summary>Fecha y hora UTC de entrada al servicio registrada desde la PWA.</summary>
        public DateTime? HoraEntrada { get; set; }

        /// <summary>Fecha y hora UTC de salida del servicio registrada desde la PWA.</summary>
        public DateTime? HoraSalida { get; set; }

        /// <summary>
        /// Horas trabajadas calculadas: (HoraSalida - HoraEntrada) en horas decimales.
        /// Ejemplo: 2h 30min = 2.5 horas.
        /// </summary>
        public decimal? HorasTrabajadas { get; set; }

        /// <summary>FK hacia el fichaje GPS vinculado al inicio del servicio de este empleado.</summary>
        public Guid? FichajeId { get; set; }

        /// <summary>Notas del empleado sobre su participación en el servicio.</summary>
        public string? Notas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Orden de servicio a la que está asignado el empleado.</summary>
        public virtual OrdenServicio? OrdenServicio { get; set; }

        /// <summary>Empleado asignado a la orden.</summary>
        public virtual Comercial.Empleado? Empleado { get; set; }
    }
}
