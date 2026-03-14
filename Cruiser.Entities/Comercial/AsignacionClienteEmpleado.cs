using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Comercial
{
    /// <summary>
    /// Asignación de un empleado a un cliente con un rol comercial o de servicio específico.
    /// Permite definir qué empleado es el gestor de cuenta, el técnico de referencia
    /// o el responsable de servicio de un cliente determinado.
    ///
    /// Las asignaciones tienen fechas de inicio y fin para gestionar cambios de gestor
    /// sin perder el historial. Solo las asignaciones sin FechaFin (o con FechaFin futura)
    /// son las vigentes.
    ///
    /// El PorcentajeComision se usa en el cálculo de liquidaciones de comisiones
    /// al completar contratos o facturas asociadas al cliente asignado.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.PorcentajeComision).HasPrecision(5, 2);
    ///   builder.Property(x => x.ObjetivoVentas).HasPrecision(18, 2);
    ///   builder.HasIndex(x => new { x.ClienteId, x.EmpleadoId, x.EsPrincipal });
    ///   builder.HasIndex(x => new { x.EmpleadoId, x.EstaActiva });
    ///   builder.HasIndex(x => x.FechaUltimaInteraccion);
    /// </remarks>
    public class AsignacionClienteEmpleado : EntidadBase
    {
        /// <summary>FK hacia el cliente asignado.</summary>
        public Guid ClienteId { get; set; }

        /// <summary>FK hacia el empleado asignado como gestor o responsable del cliente.</summary>
        public Guid EmpleadoId { get; set; }

        /// <summary>
        /// Rol que desempeña el empleado en relación a este cliente.
        /// Ejemplo: "Gestor de Cuenta", "Técnico de Servicio", "Supervisor de Calidad".
        /// </summary>
        public string Rol { get; set; } = string.Empty;

        /// <summary>
        /// Indica si esta es la asignación principal del empleado a este cliente.
        /// El empleado principal es el contacto de referencia para el cliente.
        /// </summary>
        public bool EsPrincipal { get; set; } = false;

        /// <summary>Fecha de inicio de la asignación.</summary>
        public DateOnly FechaInicio { get; set; }

        /// <summary>Fecha de fin de la asignación. Nulo si la asignación está vigente.</summary>
        public DateOnly? FechaFin { get; set; }

        /// <summary>
        /// Indica si la asignación está actualmente vigente.
        /// Calculado automáticamente: FechaFin == null || FechaFin >= hoy.
        /// </summary>
        public bool EstaActiva { get; set; } = true;

        /// <summary>
        /// Porcentaje de comisión que corresponde al empleado sobre las ventas/contratos
        /// generados con este cliente.
        /// </summary>
        public decimal PorcentajeComision { get; set; } = 0m;

        /// <summary>
        /// Objetivo de ventas o facturación mensual asignado al empleado para este cliente.
        /// Usado en los reportes de cumplimiento de objetivos.
        /// </summary>
        public decimal? ObjetivoVentas { get; set; }

        /// <summary>
        /// Prioridad de atención de este cliente para el empleado asignado.
        /// Menor número = mayor prioridad. Afecta a la ordenación en el panel del empleado.
        /// </summary>
        public int Prioridad { get; set; } = 5;

        /// <summary>
        /// Fecha y hora UTC de la última interacción comercial registrada entre
        /// el empleado y el cliente. Actualizada automáticamente al crear InteraccionComercial.
        /// </summary>
        public DateTime? FechaUltimaInteraccion { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Cliente asignado.</summary>
        public virtual Cliente? Cliente { get; set; }

        /// <summary>Empleado asignado como gestor del cliente.</summary>
        public virtual Empleado? Empleado { get; set; }
    }
}
