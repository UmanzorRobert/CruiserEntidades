using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.RRHH
{
    /// <summary>
    /// Liquidación de comisiones de un empleado por las ventas o contratos
    /// gestionados durante un período determinado.
    ///
    /// El cálculo de comisiones se realiza automáticamente por el job de Hangfire
    /// ILiquidacionComisionService.CalcularPeriodo a final de cada mes,
    /// usando las AsignacionClienteEmpleado.PorcentajeComision y los importes
    /// de contratos y facturas cobradas del período.
    ///
    /// El flujo es: Pendiente → Aprobada (por administración) → Pagada (junto con la nómina).
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Periodo).IsRequired().HasMaxLength(7);
    ///   builder.Property(x => x.ImporteBaseCalculo).HasPrecision(18, 2);
    ///   builder.Property(x => x.PorcentajeComision).HasPrecision(5, 2);
    ///   builder.Property(x => x.ImporteComision).HasPrecision(18, 2);
    ///   builder.HasIndex(x => new { x.EmpleadoId, x.Periodo }).IsUnique();
    ///   builder.HasIndex(x => x.Estado);
    /// </remarks>
    public class LiquidacionComision : EntidadBase
    {
        /// <summary>FK hacia el empleado al que corresponde esta liquidación de comisiones.</summary>
        public Guid EmpleadoId { get; set; }

        /// <summary>
        /// Período de la liquidación en formato "YYYY-MM".
        /// Ejemplo: "2026-03" para las comisiones de marzo de 2026.
        /// </summary>
        public string Periodo { get; set; } = string.Empty;

        /// <summary>
        /// Importe total de facturación o ventas sobre el que se calcula la comisión.
        /// Suma de importes de facturas cobradas en el período atribuibles al empleado.
        /// </summary>
        public decimal ImporteBaseCalculo { get; set; }

        /// <summary>Porcentaje de comisión promedio ponderado del empleado para el período.</summary>
        public decimal PorcentajeComision { get; set; }

        /// <summary>
        /// Importe de comisión calculada: ImporteBaseCalculo × PorcentajeComision / 100.
        /// </summary>
        public decimal ImporteComision { get; set; }

        /// <summary>Estado actual de la liquidación en el flujo de aprobación y pago.</summary>
        public EstadoLiquidacionComision Estado { get; set; } = EstadoLiquidacionComision.Pendiente;

        /// <summary>Número de contratos o ventas que generaron la comisión de este período.</summary>
        public int NumeroOperaciones { get; set; } = 0;

        /// <summary>Objetivo de ventas del empleado para el período.</summary>
        public decimal? ObjetivoVentas { get; set; }

        /// <summary>
        /// Porcentaje de cumplimiento del objetivo: (ImporteBaseCalculo / ObjetivoVentas) × 100.
        /// Calculado automáticamente. Usado en el dashboard de rendimiento del empleado.
        /// </summary>
        public decimal? PorcentajeCumplimientoObjetivo { get; set; }

        /// <summary>FK hacia el usuario de administración que aprobó la liquidación.</summary>
        public Guid? AprobadoPorId { get; set; }

        /// <summary>Fecha y hora UTC de la aprobación de la liquidación.</summary>
        public DateTime? FechaAprobacion { get; set; }

        /// <summary>Fecha en que la comisión fue pagada al empleado (generalmente con la nómina).</summary>
        public DateOnly? FechaPago { get; set; }

        /// <summary>Notas sobre el cálculo o la aprobación de la liquidación.</summary>
        public string? Notas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Empleado al que corresponde esta liquidación de comisiones.</summary>
        public virtual Comercial.Empleado? Empleado { get; set; }
    }
}
