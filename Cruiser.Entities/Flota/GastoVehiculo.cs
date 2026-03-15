using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Flota
{
    /// <summary>
    /// Gasto operativo asociado a un vehículo de la flota: combustible, peajes,
    /// aparcamiento, multas, reparaciones menores, lavados y otros gastos de uso diario.
    ///
    /// Los gastos con EsAprobado=false están pendientes de validación por el
    /// responsable de flota. Los gastos de tipo Multa siempre requieren aprobación
    /// del director y se investiga si es imputable al conductor.
    ///
    /// La integración con el panel de rentabilidad suma los gastos de flota
    /// al cálculo del CostoServicio para obtener el margen real de cada orden.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Importe).HasPrecision(18, 2);
    ///   builder.HasIndex(x => new { x.VehiculoEmpresaId, x.Fecha });
    ///   builder.HasIndex(x => x.TipoGasto);
    ///   builder.HasIndex(x => x.EsAprobado);
    ///   builder.HasIndex(x => x.EmpleadoId);
    /// </remarks>
    public class GastoVehiculo : EntidadBase
    {
        /// <summary>FK hacia el vehículo al que corresponde el gasto.</summary>
        public Guid VehiculoEmpresaId { get; set; }

        /// <summary>FK hacia el empleado que incurrió en el gasto durante el uso del vehículo.</summary>
        public Guid EmpleadoId { get; set; }

        /// <summary>Tipo de gasto de vehículo.</summary>
        public TipoGastoVehiculo TipoGasto { get; set; }

        /// <summary>Fecha en que se incurrió en el gasto.</summary>
        public DateOnly Fecha { get; set; }

        /// <summary>Importe del gasto sin IVA.</summary>
        public decimal Importe { get; set; }

        /// <summary>Kilómetros del contador del vehículo en el momento del gasto.</summary>
        public int? KilometrosAlGasto { get; set; }

        /// <summary>Descripción del gasto y circunstancias en que se produjo.</summary>
        public string? Descripcion { get; set; }

        /// <summary>Ruta relativa del justificante del gasto (ticket, factura, foto de multa).</summary>
        public string? RutaJustificante { get; set; }

        /// <summary>
        /// Indica si el gasto está aprobado por el responsable de flota.
        /// Los gastos no aprobados no se incluyen en el cálculo de costes del vehículo.
        /// </summary>
        public bool EsAprobado { get; set; } = false;

        /// <summary>Motivo del rechazo del gasto cuando no se aprueba.</summary>
        public string? MotivoRechazo { get; set; }

        /// <summary>FK hacia el usuario que aprobó o rechazó el gasto.</summary>
        public Guid? AprobadoPorId { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Vehículo al que corresponde el gasto.</summary>
        public virtual VehiculoEmpresa? VehiculoEmpresa { get; set; }

        /// <summary>Empleado que incurrió en el gasto.</summary>
        public virtual Comercial.Empleado? Empleado { get; set; }
    }
}
